using MediatR;
using NarSmart.Application.Common.Models;

namespace NarSmart.Application.Features.Sales.Commands.CreateSale;

public record CreateSaleCommand(
    Guid RoomId,
    Guid SalesPackageId,
    DateTime StartDate,
    DateTime EndDate,
    List<Guid> CustomerIds) : IRequest<Result<Guid>>;
