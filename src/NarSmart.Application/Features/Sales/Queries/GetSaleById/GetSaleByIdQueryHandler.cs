using MediatR;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Sales.DTOs;
using NarSmart.Domain.Entities.Sale;

namespace NarSmart.Application.Features.Sales.Queries.GetSaleById;

public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, Result<SaleDto>>
{
    private readonly ISaleRepository _saleRepository;

    public GetSaleByIdQueryHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<Result<SaleDto>> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale is null)
            return Result<SaleDto>.NotFound($"Sale with id '{request.Id}' was not found.");

        return Result<SaleDto>.Success(sale.ToDto());
    }
}
