using FluentAssertions;
using NarSmart.Domain.Common;
using NarSmart.Domain.Entities.Customer;
using NarSmart.Domain.Enums;
using NarSmart.Domain.ValueObjects;

namespace NarSmart.Domain.Tests.Factories;

public class CustomerFactoryTests
{
    private readonly Guid _hotelId = Guid.NewGuid();
    private readonly PersonName _validName = new("John", "Doe");
    private readonly DateTime _birthDate = new(1990, 1, 1);

    [Fact]
    public void CreateCustomer_WithValidData_ShouldSucceed()
    {
        var customer = CustomerFactory.CreateCustomer(
            _hotelId, _validName, _birthDate,
            "12345678901", null, null, null, null);

        customer.CustomerType.Should().Be(CustomerType.Customer);
        customer.FirstName.Should().Be("John");
        customer.LastName.Should().Be("Doe");
        customer.NationalId.Should().Be("12345678901");
        customer.IsAdult.Should().BeTrue();
    }

    [Fact]
    public void CreateCustomer_WithoutNationalIdOrPassport_ShouldThrow()
    {
        var act = () => CustomerFactory.CreateCustomer(
            _hotelId, _validName, _birthDate,
            null, null, null, null, null);

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void CreateGuest_WithValidData_ShouldSucceed()
    {
        var period = new DateRange(DateTime.UtcNow, DateTime.UtcNow.AddDays(5));
        var guest = CustomerFactory.CreateGuest(
            _hotelId, _validName, _birthDate, period, "12345678901");

        guest.CustomerType.Should().Be(CustomerType.Guest);
        guest.StartDate.Should().Be(period.StartDate);
        guest.EndDate.Should().Be(period.EndDate);
    }

    [Fact]
    public void CreateCustomer_Underage_ShouldSetIsAdultFalse()
    {
        var minorBirthDate = DateTime.UtcNow.AddYears(-10);
        var customer = CustomerFactory.CreateCustomer(
            _hotelId, _validName, minorBirthDate,
            "12345678901", null, null, null, null);

        customer.IsAdult.Should().BeFalse();
    }
}
