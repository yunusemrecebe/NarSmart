using FluentAssertions;
using Moq;
using NarSmart.Application.Common.Interfaces;
using NarSmart.Application.Features.Sales.Commands.CreateSale;
using NarSmart.Domain.Common;
using NarSmart.Domain.Entities.Sale;

namespace NarSmart.Application.Tests.Sales;

public class CreateSaleCommandHandlerTests
{
    private readonly Mock<ISaleRepository> _saleRepoMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<ICurrentTenantService> _tenantMock = new();
    private readonly CreateSaleCommandHandler _handler;

    public CreateSaleCommandHandlerTests()
    {
        _tenantMock.Setup(t => t.HotelId).Returns(Guid.NewGuid());
        _handler = new CreateSaleCommandHandler(
            _saleRepoMock.Object, _unitOfWorkMock.Object, _tenantMock.Object);
    }

    [Fact]
    public async Task Handle_ValidSale_ShouldReturnSuccess()
    {
        _saleRepoMock
            .Setup(r => r.HasOverlappingReservationAsync(
                It.IsAny<Guid>(), It.IsAny<Guid>(),
                It.IsAny<DateTime>(), It.IsAny<DateTime>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var command = new CreateSaleCommand(
            Guid.NewGuid(), Guid.NewGuid(),
            DateTime.UtcNow, DateTime.UtcNow.AddDays(5),
            new List<Guid> { Guid.NewGuid() });

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        _saleRepoMock.Verify(r => r.AddAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_OverlappingReservation_ShouldReturnFailure()
    {
        _saleRepoMock
            .Setup(r => r.HasOverlappingReservationAsync(
                It.IsAny<Guid>(), It.IsAny<Guid>(),
                It.IsAny<DateTime>(), It.IsAny<DateTime>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var command = new CreateSaleCommand(
            Guid.NewGuid(), Guid.NewGuid(),
            DateTime.UtcNow, DateTime.UtcNow.AddDays(5),
            new List<Guid> { Guid.NewGuid() });

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        _saleRepoMock.Verify(r => r.AddAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
