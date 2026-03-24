using NarSmart.Domain.Common;
using NarSmart.Domain.Enums;
using NarSmart.Domain.ValueObjects;

namespace NarSmart.Domain.Entities.Customer;

public static class CustomerFactory
{
    public static Customer CreateCustomer(
        Guid hotelId, PersonName name, DateTime birthDate,
        string? nationalId, string? passportNumber,
        string? phoneNumber, string? email, string? profilePhotoUrl)
    {
        if (string.IsNullOrWhiteSpace(nationalId) && string.IsNullOrWhiteSpace(passportNumber))
            throw new DomainException("Customer must have at least a national ID or passport number.");

        return Customer.CreateInternal(
            hotelId, name, birthDate,
            CustomerType.Customer,
            nationalId, passportNumber,
            phoneNumber, email, profilePhotoUrl,
            startDate: null, endDate: null);
    }

    public static Customer CreateGuest(
        Guid hotelId, PersonName name, DateTime birthDate,
        DateRange reservationPeriod,
        string? nationalId = null, string? passportNumber = null)
    {
        return Customer.CreateInternal(
            hotelId, name, birthDate,
            CustomerType.Guest,
            nationalId, passportNumber,
            phoneNumber: null, email: null, profilePhotoUrl: null,
            startDate: reservationPeriod.StartDate,
            endDate: reservationPeriod.EndDate);
    }
}
