using FluentAssertions;
using Moq;
using NarSmart.Application.Common.Interfaces;
using NarSmart.Application.Features.Rooms.Commands.CreateRoom;
using NarSmart.Domain.Common;
using NarSmart.Domain.Entities.Room;

namespace NarSmart.Application.Tests.Rooms;

public class CreateRoomCommandHandlerTests
{
    private readonly Mock<IRoomRepository> _roomRepoMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<ICurrentTenantService> _tenantMock = new();
    private readonly CreateRoomCommandHandler _handler;

    public CreateRoomCommandHandlerTests()
    {
        _tenantMock.Setup(t => t.HotelId).Returns(Guid.NewGuid());
        _handler = new CreateRoomCommandHandler(
            _roomRepoMock.Object, _unitOfWorkMock.Object, _tenantMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRoom_ShouldReturnSuccess()
    {
        _roomRepoMock
            .Setup(r => r.ExistsByRoomNumberAsync(It.IsAny<Guid>(), "101", It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var command = new CreateRoomCommand("101", 1, 2);
        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Data.Should().NotBeEmpty();
        _roomRepoMock.Verify(r => r.AddAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_DuplicateRoomNumber_ShouldReturnFailure()
    {
        _roomRepoMock
            .Setup(r => r.ExistsByRoomNumberAsync(It.IsAny<Guid>(), "101", It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var command = new CreateRoomCommand("101", 1, 2);
        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        _roomRepoMock.Verify(r => r.AddAsync(It.IsAny<Room>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
