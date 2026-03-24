using NarSmart.Domain.Common;

namespace NarSmart.Domain.Entities.SalesPackage;

public class SalesPackagePrice : BaseEntity
{
    public Guid SalesPackageId { get; private set; }
    public Guid CurrencyId { get; private set; }
    public decimal Amount { get; private set; }

    private SalesPackagePrice() { }

    internal static SalesPackagePrice Create(Guid salesPackageId, Guid hotelId, Guid currencyId, decimal amount)
    {
        return new SalesPackagePrice
        {
            Id = Guid.NewGuid(),
            HotelId = hotelId,
            SalesPackageId = salesPackageId,
            CurrencyId = currencyId,
            Amount = amount
        };
    }
}
