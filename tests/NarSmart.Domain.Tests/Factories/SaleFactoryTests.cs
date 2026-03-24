using FluentAssertions;
using NarSmart.Domain.Common;
using NarSmart.Domain.Entities.Sale;
using NarSmart.Domain.ValueObjects;

namespace NarSmart.Domain.Tests.Factories;

public class SaleFactoryTests
{
    private readonly Guid _hotelId = Guid.NewGuid();
    private readonly Guid _roomId = Guid.NewGuid();
    private readonly Guid _packageId = Guid.NewGuid();

    [Fact]
    public void Create_WithValidData_ShouldSucceed()
    {
        var period = new DateRange(DateTime.UtcNow, DateTime.UtcNow.AddDays(5));
        var customerIds = new List<Guid> { Guid.NewGuid() };

        var sale = SaleFactory.Create(_hotelId, _roomId, _packageId, period, customerIds);

        sale.RoomId.Should().Be(_roomId);
        sale.SalesPackageId.Should().Be(_packageId);
        sale.SaleCustomers.Should().HaveCount(1);
    }

    [Fact]
    public void Create_WithoutCustomers_ShouldThrow()
    {
        var period = new DateRange(DateTime.UtcNow, DateTime.UtcNow.AddDays(5));

        var act = () => SaleFactory.Create(_hotelId, _roomId, _packageId, period, new List<Guid>());

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void AddCustomer_ShouldIncreaseSaleCustomers()
    {
        var period = new DateRange(DateTime.UtcNow, DateTime.UtcNow.AddDays(5));
        var sale = SaleFactory.Create(_hotelId, _roomId, _packageId, period, new List<Guid> { Guid.NewGuid() });

        sale.AddCustomer(Guid.NewGuid());

        sale.SaleCustomers.Should().HaveCount(2);
    }
}
