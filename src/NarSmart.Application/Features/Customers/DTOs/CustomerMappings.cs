using NarSmart.Domain.Entities.Customer;

namespace NarSmart.Application.Features.Customers.DTOs;

public static class CustomerMappings
{
    public static CustomerDto ToDto(this Customer customer)
    {
        return new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            NationalId = customer.NationalId,
            PassportNumber = customer.PassportNumber,
            PhoneNumber = customer.PhoneNumber,
            Email = customer.Email,
            BirthDate = customer.BirthDate,
            IsAdult = customer.IsAdult,
            ProfilePhotoUrl = customer.ProfilePhotoUrl,
            CustomerType = customer.CustomerType,
            StartDate = customer.StartDate,
            EndDate = customer.EndDate,
            IsActive = customer.IsActive
        };
    }

    public static List<CustomerDto> ToDtoList(this IEnumerable<Customer> customers)
        => customers.Select(c => c.ToDto()).ToList();
}
