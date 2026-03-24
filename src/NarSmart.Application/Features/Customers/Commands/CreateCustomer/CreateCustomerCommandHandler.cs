using MediatR;
using NarSmart.Application.Common.Interfaces;
using NarSmart.Application.Common.Models;
using NarSmart.Domain.Common;
using NarSmart.Domain.Entities.Customer;
using NarSmart.Domain.ValueObjects;

namespace NarSmart.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<Guid>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentTenantService _tenantService;

    public CreateCustomerCommandHandler(
        ICustomerRepository customerRepository,
        IUnitOfWork unitOfWork,
        ICurrentTenantService tenantService)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _tenantService = tenantService;
    }

    public async Task<Result<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var name = new PersonName(request.FirstName, request.LastName);

        var customer = CustomerFactory.CreateCustomer(
            _tenantService.HotelId,
            name,
            request.BirthDate,
            request.NationalId,
            request.PassportNumber,
            request.PhoneNumber,
            request.Email,
            request.ProfilePhotoUrl);

        await _customerRepository.AddAsync(customer, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Created(customer.Id);
    }
}
