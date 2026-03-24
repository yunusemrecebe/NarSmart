using NarSmart.Domain.Common;
using NarSmart.Domain.ValueObjects;

namespace NarSmart.Domain.Entities.Sale;

public class Sale : BaseEntity
{
    public Guid RoomId { get; private set; }
    public Guid SalesPackageId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    private readonly List<SaleCustomer> _saleCustomers = new();
    public IReadOnlyCollection<SaleCustomer> SaleCustomers => _saleCustomers.AsReadOnly();

    private Sale() { }

    internal static Sale CreateInternal(
        Guid hotelId, Guid roomId, Guid salesPackageId,
        DateRange period, List<Guid> customerIds)
    {
        if (customerIds.Count == 0)
            throw new DomainException("Sale must have at least one customer.");

        var sale = new Sale
        {
            Id = Guid.NewGuid(),
            HotelId = hotelId,
            RoomId = roomId,
            SalesPackageId = salesPackageId,
            StartDate = period.StartDate,
            EndDate = period.EndDate
        };

        foreach (var customerId in customerIds)
        {
            sale._saleCustomers.Add(SaleCustomer.Create(sale.Id, hotelId, customerId));
        }

        return sale;
    }

    public void AddCustomer(Guid customerId)
    {
        if (_saleCustomers.Any(sc => sc.CustomerId == customerId))
            throw new DomainException("Customer is already added to this sale.");

        _saleCustomers.Add(SaleCustomer.Create(Id, HotelId, customerId));
    }

    public DateRange GetPeriod() => new(StartDate, EndDate);
}
