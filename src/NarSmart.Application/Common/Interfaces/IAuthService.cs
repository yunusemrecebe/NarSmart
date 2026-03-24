using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Auth.DTOs;

namespace NarSmart.Application.Common.Interfaces;

public interface IAuthService
{
    Task<Result<List<UserHotelDto>>> GetUserHotelsAsync(string email, CancellationToken cancellationToken = default);
    Task<Result<LoginResponseDto>> LoginAsync(string email, string password, Guid hotelId, CancellationToken cancellationToken = default);
}
