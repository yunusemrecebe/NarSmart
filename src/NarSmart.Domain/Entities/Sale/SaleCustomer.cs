using NarSmart.Domain.Common;

namespace NarSmart.Domain.Entities.Sale;

public class SaleCustomer : BaseEntity
{
    public Guid SaleId { get; private set; }
    public Guid CustomerId { get; private set; }

    private SaleCustomer() { }

    internal static SaleCustomer Create(Guid saleId, Guid hotelId, Guid customerId)
    {
        return new SaleCustomer
        {
            Id = Guid.NewGuid(),
            HotelId = hotelId,
            SaleId = saleId,
            CustomerId = customerId
        };
    }
}
