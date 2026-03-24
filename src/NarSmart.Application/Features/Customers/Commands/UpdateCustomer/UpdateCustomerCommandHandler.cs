using MediatR;
using NarSmart.Application.Common.Models;
using NarSmart.Domain.Common;
using NarSmart.Domain.Entities.Customer;
using NarSmart.Domain.ValueObjects;

namespace NarSmart.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result<bool>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);

        if (customer is null)
            return Result<bool>.NotFound($"Customer with id '{request.Id}' was not found.");

        var name = new PersonName(request.FirstName, request.LastName);

        customer.Update(name, request.BirthDate,
            request.NationalId, request.PassportNumber,
            request.PhoneNumber, request.Email, request.ProfilePhotoUrl);

        _customerRepository.Update(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}
