using MediatR;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Customers.DTOs;
using NarSmart.Domain.Entities.Customer;

namespace NarSmart.Application.Features.Customers.Queries.GetCustomers;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, Result<List<CustomerDto>>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomersQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Result<List<CustomerDto>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllAsync(cancellationToken);
        return Result<List<CustomerDto>>.Success(customers.ToDtoList());
    }
}
