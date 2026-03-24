using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Users.DTOs;

namespace NarSmart.Application.Common.Interfaces;

public interface IUserService
{
    Task<Result<List<UserDto>>> GetAllByHotelAsync(Guid hotelId, CancellationToken cancellationToken = default);
    Task<Result<UserDto>> GetByIdAsync(Guid userId, Guid hotelId, CancellationToken cancellationToken = default);
    Task<Result<Guid>> CreateAsync(Guid hotelId, string firstName, string lastName, string email,
        string password, string registrationNumber, string role,
        string? phoneNumber, string? photoUrl, DateTime? hireDate, CancellationToken cancellationToken = default);
    Task<Result<bool>> UpdateAsync(Guid userId, Guid hotelId, string firstName, string lastName,
        string registrationNumber, string? phoneNumber, string? photoUrl,
        DateTime? hireDate, CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(Guid userId, Guid hotelId, DateTime terminationDate, CancellationToken cancellationToken = default);
}
