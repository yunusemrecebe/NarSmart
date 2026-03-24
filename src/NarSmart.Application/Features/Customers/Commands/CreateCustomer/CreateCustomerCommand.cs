using MediatR;
using NarSmart.Application.Common.Models;

namespace NarSmart.Application.Features.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand(
    string FirstName,
    string LastName,
    DateTime BirthDate,
    string? NationalId,
    string? PassportNumber,
    string? PhoneNumber,
    string? Email,
    string? ProfilePhotoUrl) : IRequest<Result<Guid>>;
