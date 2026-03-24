using MediatR;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Sales.DTOs;

namespace NarSmart.Application.Features.Sales.Queries.GetSales;

public record GetSalesQuery : IRequest<Result<List<SaleDto>>>;
