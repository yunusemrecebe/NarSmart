using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NarSmart.Application.Common.Interfaces;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Users.DTOs;
using NarSmart.Infrastructure.Data;
using NarSmart.Infrastructure.Identity;

namespace NarSmart.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly NarSmartDbContext _context;

    public UserService(UserManager<ApplicationUser> userManager, NarSmartDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<Result<List<UserDto>>> GetAllByHotelAsync(Guid hotelId, CancellationToken cancellationToken = default)
    {
        var userIds = await _context.UserHotels
            .Where(uh => uh.HotelId == hotelId && !uh.IsDeleted)
            .Select(uh => uh.UserId)
            .ToListAsync(cancellationToken);

        var users = await _context.Users
            .Where(u => userIds.Contains(u.Id) && !u.IsDeleted)
            .ToListAsync(cancellationToken);

        var dtos = new List<UserDto>();
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            dtos.Add(MapToDto(user, roles.FirstOrDefault() ?? string.Empty));
        }

        return Result<List<UserDto>>.Success(dtos);
    }

    public async Task<Result<UserDto>> GetByIdAsync(Guid userId, Guid hotelId, CancellationToken cancellationToken = default)
    {
        var hasAccess = await _context.UserHotels
            .AnyAsync(uh => uh.UserId == userId && uh.HotelId == hotelId && !uh.IsDeleted, cancellationToken);

        if (!hasAccess)
            return Result<UserDto>.NotFound($"User with id '{userId}' was not found in this hotel.");

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted, cancellationToken);
        if (user is null)
            return Result<UserDto>.NotFound($"User with id '{userId}' was not found.");

        var roles = await _userManager.GetRolesAsync(user);
        return Result<UserDto>.Success(MapToDto(user, roles.FirstOrDefault() ?? string.Empty));
    }

    public async Task<Result<Guid>> CreateAsync(
        Guid hotelId, string firstName, string lastName, string email,
        string password, string registrationNumber, string role,
        string? phoneNumber, string? photoUrl, DateTime? hireDate,
        CancellationToken cancellationToken = default)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);
        if (existingUser is not null)
            return Result<Guid>.Failure("A user with this email already exists.");

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            RegistrationNumber = registrationNumber,
            PhoneNumber = phoneNumber,
            PhotoUrl = photoUrl,
            HotelId = hotelId,
            HireDate = hireDate,
            EmailConfirmed = true,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return Result<Guid>.Failure(result.Errors.Select(e => e.Description).ToList());

        await _userManager.AddToRoleAsync(user, role);

        var userHotel = UserHotel.Create(user.Id, hotelId);
        await _context.UserHotels.AddAsync(userHotel, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Created(user.Id);
    }

    public async Task<Result<bool>> UpdateAsync(
        Guid userId, Guid hotelId, string firstName, string lastName,
        string registrationNumber, string? phoneNumber, string? photoUrl,
        DateTime? hireDate, CancellationToken cancellationToken = default)
    {
        var hasAccess = await _context.UserHotels
            .AnyAsync(uh => uh.UserId == userId && uh.HotelId == hotelId && !uh.IsDeleted, cancellationToken);

        if (!hasAccess)
            return Result<bool>.NotFound($"User with id '{userId}' was not found in this hotel.");

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted, cancellationToken);
        if (user is null)
            return Result<bool>.NotFound($"User with id '{userId}' was not found.");

        user.FirstName = firstName;
        user.LastName = lastName;
        user.RegistrationNumber = registrationNumber;
        user.PhoneNumber = phoneNumber;
        user.PhotoUrl = photoUrl;
        user.HireDate = hireDate;
        user.UpdatedAt = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> DeleteAsync(
        Guid userId, Guid hotelId, DateTime terminationDate, CancellationToken cancellationToken = default)
    {
        var hasAccess = await _context.UserHotels
            .AnyAsync(uh => uh.UserId == userId && uh.HotelId == hotelId && !uh.IsDeleted, cancellationToken);

        if (!hasAccess)
            return Result<bool>.NotFound($"User with id '{userId}' was not found in this hotel.");

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted, cancellationToken);
        if (user is null)
            return Result<bool>.NotFound($"User with id '{userId}' was not found.");

        user.TerminationDate = terminationDate;
        user.IsActive = false;
        user.IsDeleted = true;
        user.UpdatedAt = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);

        var userHotel = await _context.UserHotels
            .FirstOrDefaultAsync(uh => uh.UserId == userId && uh.HotelId == hotelId && !uh.IsDeleted, cancellationToken);

        if (userHotel is not null)
        {
            userHotel.IsDeleted = true;
            userHotel.IsActive = false;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }

    private static UserDto MapToDto(ApplicationUser user, string role)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            RegistrationNumber = user.RegistrationNumber,
            PhoneNumber = user.PhoneNumber,
            PhotoUrl = user.PhotoUrl,
            Role = role,
            HireDate = user.HireDate,
            TerminationDate = user.TerminationDate,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt
        };
    }
}
