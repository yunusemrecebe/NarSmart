using MediatR;
using NarSmart.Application.Common.Models;

namespace NarSmart.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string RegistrationNumber,
    string Role,
    string? PhoneNumber,
    string? PhotoUrl,
    DateTime? HireDate) : IRequest<Result<Guid>>;
