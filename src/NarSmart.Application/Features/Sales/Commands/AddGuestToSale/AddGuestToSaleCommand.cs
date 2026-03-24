using MediatR;
using NarSmart.Application.Common.Models;

namespace NarSmart.Application.Features.Sales.Commands.AddGuestToSale;

public record AddGuestToSaleCommand(
    Guid SaleId,
    string FirstName,
    string LastName,
    DateTime BirthDate,
    string? NationalId,
    string? PassportNumber) : IRequest<Result<Guid>>;
