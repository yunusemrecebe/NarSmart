using NarSmart.Domain.Common;
using NarSmart.Domain.Enums;

namespace NarSmart.Domain.Entities.Discount;

public class Discount : BaseEntity
{
    public Guid? SalesPackageId { get; private set; }
    public Guid? HotelServiceId { get; private set; }
    public Guid? ProductId { get; private set; }
    public DiscountType DiscountType { get; private set; }
    public decimal Amount { get; private set; }
    public Guid CurrencyId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    private Discount() { }

    public static Discount Create(
        Guid hotelId,
        DiscountType discountType,
        decimal amount,
        Guid currencyId,
        DateTime startDate,
        DateTime endDate,
        Guid? salesPackageId = null,
        Guid? hotelServiceId = null,
        Guid? productId = null)
    {
        if (amount <= 0)
            throw new DomainException("Discount amount must be greater than zero.");

        if (salesPackageId is null && hotelServiceId is null && productId is null)
            throw new DomainException("Discount must target at least one of: SalesPackage, HotelService, or Product.");

        var range = new ValueObjects.DateRange(startDate, endDate);

        return new Discount
        {
            Id = Guid.NewGuid(),
            HotelId = hotelId,
            DiscountType = discountType,
            Amount = amount,
            CurrencyId = currencyId,
            StartDate = range.StartDate,
            EndDate = range.EndDate,
            SalesPackageId = salesPackageId,
            HotelServiceId = hotelServiceId,
            ProductId = productId
        };
    }
}
