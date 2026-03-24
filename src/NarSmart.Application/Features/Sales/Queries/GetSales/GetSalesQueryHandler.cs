using MediatR;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Sales.DTOs;
using NarSmart.Domain.Entities.Sale;

namespace NarSmart.Application.Features.Sales.Queries.GetSales;

public class GetSalesQueryHandler : IRequestHandler<GetSalesQuery, Result<List<SaleDto>>>
{
    private readonly ISaleRepository _saleRepository;

    public GetSalesQueryHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<Result<List<SaleDto>>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
    {
        var sales = await _saleRepository.GetAllAsync(cancellationToken);
        return Result<List<SaleDto>>.Success(sales.ToDtoList());
    }
}
