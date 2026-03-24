using MediatR;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Customers.DTOs;

namespace NarSmart.Application.Features.Customers.Queries.GetCustomerById;

public record GetCustomerByIdQuery(Guid Id) : IRequest<Result<CustomerDto>>;
