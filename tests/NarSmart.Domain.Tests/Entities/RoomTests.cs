using FluentAssertions;
using NarSmart.Domain.Common;
using NarSmart.Domain.Entities.Room;

namespace NarSmart.Domain.Tests.Entities;

public class RoomTests
{
    [Fact]
    public void Create_WithValidData_ShouldSucceed()
    {
        var hotelId = Guid.NewGuid();
        var room = Room.Create(hotelId, "101", 1, 2);

        room.RoomNumber.Should().Be("101");
        room.FloorNumber.Should().Be(1);
        room.BedCount.Should().Be(2);
        room.HotelId.Should().Be(hotelId);
    }

    [Theory]
    [InlineData("", 1, 2)]
    [InlineData("101", 1, 0)]
    public void Create_WithInvalidData_ShouldThrow(string roomNumber, int floor, int beds)
    {
        var act = () => Room.Create(Guid.NewGuid(), roomNumber, floor, beds);

        act.Should().Throw<DomainException>();
    }
}
