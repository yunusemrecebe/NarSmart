using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NarSmart.Application.Common.Interfaces;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Auth.DTOs;
using NarSmart.Infrastructure.Data;
using NarSmart.Infrastructure.Identity;

namespace NarSmart.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly NarSmartDbContext _context;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        IJwtTokenService jwtTokenService,
        NarSmartDbContext context)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
        _context = context;
    }

    public async Task<Result<List<UserHotelDto>>> GetUserHotelsAsync(
        string email, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return Result<List<UserHotelDto>>.Failure("No account found with this email.");

        var hotels = await _context.UserHotels
            .IgnoreQueryFilters()
            .Where(uh => uh.UserId == user.Id && !uh.IsDeleted)
            .Join(_context.Hotels.IgnoreQueryFilters().Where(h => !h.IsDeleted),
                uh => uh.HotelId, h => h.Id,
                (uh, h) => new UserHotelDto
                {
                    HotelId = h.Id,
                    HotelName = h.Name,
                    Location = h.Location
                })
            .ToListAsync(cancellationToken);

        if (hotels.Count == 0)
            return Result<List<UserHotelDto>>.Failure("User does not have access to any hotel.");

        return Result<List<UserHotelDto>>.Success(hotels);
    }

    public async Task<Result<LoginResponseDto>> LoginAsync(
        string email, string password, Guid hotelId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return Result<LoginResponseDto>.Failure("Invalid email or password.");

        var passwordValid = await _userManager.CheckPasswordAsync(user, password);
        if (!passwordValid)
            return Result<LoginResponseDto>.Failure("Invalid email or password.");

        var hasAccess = await _context.UserHotels
            .IgnoreQueryFilters()
            .AnyAsync(uh => uh.UserId == user.Id && uh.HotelId == hotelId && !uh.IsDeleted, cancellationToken);

        if (!hasAccess)
            return Result<LoginResponseDto>.Failure("User does not have access to the selected hotel.");

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? "Receptionist";

        var token = _jwtTokenService.GenerateToken(user.Id, email, role, hotelId);

        return Result<LoginResponseDto>.Success(new LoginResponseDto
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60),
            UserId = user.Id,
            Email = email,
            Role = role,
            HotelId = hotelId
        });
    }
}
