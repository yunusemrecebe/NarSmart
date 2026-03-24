using NarSmart.Domain.Entities.Sale;

namespace NarSmart.Application.Features.Sales.DTOs;

public static class SaleMappings
{
    public static SaleDto ToDto(this Sale sale)
    {
        return new SaleDto
        {
            Id = sale.Id,
            RoomId = sale.RoomId,
            SalesPackageId = sale.SalesPackageId,
            StartDate = sale.StartDate,
            EndDate = sale.EndDate,
            IsActive = sale.IsActive,
            Customers = sale.SaleCustomers
                .Select(sc => new SaleCustomerDto { CustomerId = sc.CustomerId })
                .ToList()
        };
    }

    public static List<SaleDto> ToDtoList(this IEnumerable<Sale> sales)
        => sales.Select(s => s.ToDto()).ToList();
}
