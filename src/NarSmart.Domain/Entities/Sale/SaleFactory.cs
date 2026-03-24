using NarSmart.Domain.ValueObjects;

namespace NarSmart.Domain.Entities.Sale;

public static class SaleFactory
{
    public static Sale Create(
        Guid hotelId, Guid roomId, Guid salesPackageId,
        DateRange period, List<Guid> customerIds)
    {
        return Sale.CreateInternal(hotelId, roomId, salesPackageId, period, customerIds);
    }
}
