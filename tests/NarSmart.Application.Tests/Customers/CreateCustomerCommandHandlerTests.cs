using FluentAssertions;
using Moq;
using NarSmart.Application.Common.Interfaces;
using NarSmart.Application.Features.Customers.Commands.CreateCustomer;
using NarSmart.Domain.Common;
using NarSmart.Domain.Entities.Customer;

namespace NarSmart.Application.Tests.Customers;

public class CreateCustomerCommandHandlerTests
{
    private readonly Mock<ICustomerRepository> _customerRepoMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<ICurrentTenantService> _tenantMock = new();
    private readonly CreateCustomerCommandHandler _handler;

    public CreateCustomerCommandHandlerTests()
    {
        _tenantMock.Setup(t => t.HotelId).Returns(Guid.NewGuid());
        _handler = new CreateCustomerCommandHandler(
            _customerRepoMock.Object, _unitOfWorkMock.Object, _tenantMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCustomer_ShouldReturnSuccess()
    {
        var command = new CreateCustomerCommand(
            "John", "Doe", new DateTime(1990, 1, 1),
            "12345678901", null, "555-1234", "john@example.com", null);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Data.Should().NotBeEmpty();
        _customerRepoMock.Verify(r => r.AddAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
