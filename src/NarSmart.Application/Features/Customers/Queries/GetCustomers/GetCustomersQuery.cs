using MediatR;
using NarSmart.Application.Common.Models;
using NarSmart.Application.Features.Customers.DTOs;

namespace NarSmart.Application.Features.Customers.Queries.GetCustomers;

public record GetCustomersQuery : IRequest<Result<List<CustomerDto>>>;
