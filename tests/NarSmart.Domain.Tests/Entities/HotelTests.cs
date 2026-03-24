using FluentAssertions;
using NarSmart.Domain.Common;
using NarSmart.Domain.Entities.Hotel;

namespace NarSmart.Domain.Tests.Entities;

public class HotelTests
{
    [Fact]
    public void Create_WithValidData_ShouldSucceed()
    {
        var hotel = Hotel.Create("Test Hotel", "Istanbul", "Europe/Istanbul", DateTime.UtcNow);

        hotel.Name.Should().Be("Test Hotel");
        hotel.Location.Should().Be("Istanbul");
        hotel.TimeZoneId.Should().Be("Europe/Istanbul");
        hotel.Id.Should().NotBeEmpty();
    }

    [Theory]
    [InlineData("", "Istanbul", "Europe/Istanbul")]
    [InlineData("Test Hotel", "", "Europe/Istanbul")]
    [InlineData("Test Hotel", "Istanbul", "")]
    public void Create_WithInvalidData_ShouldThrow(string name, string location, string tz)
    {
        var act = () => Hotel.Create(name, location, tz, DateTime.UtcNow);

        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Create_WithoutEndDate_ShouldSetTo9999()
    {
        var hotel = Hotel.Create("Test Hotel", "Istanbul", "Europe/Istanbul", DateTime.UtcNow);

        hotel.EndDate.Year.Should().Be(9999);
    }
}
