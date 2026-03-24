using NarSmart.Domain.Common;

namespace NarSmart.Domain.Entities.SalesPackage;

public class SalesPackageHotelService : BaseEntity
{
    public Guid SalesPackageId { get; private set; }
    public Guid HotelServiceId { get; private set; }

    private SalesPackageHotelService() { }

    internal static SalesPackageHotelService Create(Guid salesPackageId, Guid hotelId, Guid hotelServiceId)
    {
        return new SalesPackageHotelService
        {
            Id = Guid.NewGuid(),
            HotelId = hotelId,
            SalesPackageId = salesPackageId,
            HotelServiceId = hotelServiceId
        };
    }
}
