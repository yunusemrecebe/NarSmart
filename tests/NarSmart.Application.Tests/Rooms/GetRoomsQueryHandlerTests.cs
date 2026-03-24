using FluentAssertions;
using Moq;
using NarSmart.Application.Features.Rooms.Queries.GetRooms;
using NarSmart.Domain.Entities.Room;

namespace NarSmart.Application.Tests.Rooms;

public class GetRoomsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnAllRooms()
    {
        var hotelId = Guid.NewGuid();
        var rooms = new List<Room>
        {
            Room.Create(hotelId, "101", 1, 2),
            Room.Create(hotelId, "102", 1, 3)
        };

        var repoMock = new Mock<IRoomRepository>();
        repoMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(rooms);

        var handler = new GetRoomsQueryHandler(repoMock.Object);
        var result = await handler.Handle(new GetRoomsQuery(), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Data.Should().HaveCount(2);
    }
}
