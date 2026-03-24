using MediatR;
using NarSmart.Application.Common.Models;

namespace NarSmart.Application.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string RegistrationNumber,
    string? PhoneNumber,
    string? PhotoUrl,
    DateTime? HireDate) : IRequest<Result<bool>>;
