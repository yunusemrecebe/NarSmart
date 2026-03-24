using NarSmart.Domain.Common;

namespace NarSmart.Domain.Entities.HotelService;

public class HotelServicePrice : BaseEntity
{
    public Guid HotelServiceId { get; private set; }
    public Guid CurrencyId { get; private set; }
    public decimal Amount { get; private set; }

    private HotelServicePrice() { }

    internal static HotelServicePrice Create(Guid hotelServiceId, Guid hotelId, Guid currencyId, decimal amount)
    {
        return new HotelServicePrice
        {
            Id = Guid.NewGuid(),
            HotelId = hotelId,
            HotelServiceId = hotelServiceId,
            CurrencyId = currencyId,
            Amount = amount
        };
    }
}
