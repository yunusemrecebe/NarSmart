using MediatR;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Customers.DTOs;
using NarSmart.Domain.Entities.Customer;

namespace NarSmart.Application.Features.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result<CustomerDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);

        if (customer is null)
            return Result<CustomerDto>.NotFound($"Customer with id '{request.Id}' was not found.");

        return Result<CustomerDto>.Success(customer.ToDto());
    }
}
