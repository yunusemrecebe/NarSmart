using MediatR;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Sales.DTOs;

namespace NarSmart.Application.Features.Sales.Queries.GetSaleById;

public record GetSaleByIdQuery(Guid Id) : IRequest<Result<SaleDto>>;
