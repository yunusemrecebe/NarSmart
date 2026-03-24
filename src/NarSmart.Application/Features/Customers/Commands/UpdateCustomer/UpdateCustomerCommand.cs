using MediatR;
using NarSmart.Application.Common.Models;

namespace NarSmart.Application.Features.Customers.Commands.UpdateCustomer;

public record UpdateCustomerCommand(
    Guid Id,
    string FirstName,
    string LastName,
    DateTime BirthDate,
    string? NationalId,
    string? PassportNumber,
    string? PhoneNumber,
    string? Email,
    string? ProfilePhotoUrl) : IRequest<Result<bool>>;
