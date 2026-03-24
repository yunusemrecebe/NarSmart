using NarSmart.Domain.Common;

namespace NarSmart.Domain.Entities.Product;

public class ProductPrice : BaseEntity
{
    public Guid ProductId { get; private set; }
    public Guid CurrencyId { get; private set; }
    public decimal Amount { get; private set; }

    private ProductPrice() { }

    internal static ProductPrice Create(Guid productId, Guid hotelId, Guid currencyId, decimal amount)
    {
        return new ProductPrice
        {
            Id = Guid.NewGuid(),
            HotelId = hotelId,
            ProductId = productId,
            CurrencyId = currencyId,
            Amount = amount
        };
    }
}
