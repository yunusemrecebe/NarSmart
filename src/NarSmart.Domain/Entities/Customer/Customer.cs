using NarSmart.Domain.Common;
using NarSmart.Domain.Enums;
using NarSmart.Domain.ValueObjects;

namespace NarSmart.Domain.Entities.Customer;

public class Customer : BaseEntity
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string? NationalId { get; private set; }
    public string? PassportNumber { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public bool IsAdult { get; private set; }
    public string? ProfilePhotoUrl { get; private set; }
    public CustomerType CustomerType { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    private Customer() { }

    internal static Customer CreateInternal(
        Guid hotelId, PersonName name, DateTime birthDate,
        CustomerType customerType,
        string? nationalId, string? passportNumber,
        string? phoneNumber, string? email,
        string? profilePhotoUrl,
        DateTime? startDate, DateTime? endDate)
    {
        return new Customer
        {
            Id = Guid.NewGuid(),
            HotelId = hotelId,
            FirstName = name.FirstName,
            LastName = name.LastName,
            BirthDate = birthDate,
            IsAdult = CalculateIsAdult(birthDate),
            CustomerType = customerType,
            NationalId = nationalId,
            PassportNumber = passportNumber,
            PhoneNumber = phoneNumber,
            Email = email,
            ProfilePhotoUrl = profilePhotoUrl,
            StartDate = startDate,
            EndDate = endDate
        };
    }

    public void Update(PersonName name, DateTime birthDate,
        string? nationalId, string? passportNumber,
        string? phoneNumber, string? email, string? profilePhotoUrl)
    {
        FirstName = name.FirstName;
        LastName = name.LastName;
        BirthDate = birthDate;
        IsAdult = CalculateIsAdult(birthDate);
        NationalId = nationalId;
        PassportNumber = passportNumber;
        PhoneNumber = phoneNumber;
        Email = email;
        ProfilePhotoUrl = profilePhotoUrl;
    }

    public void SetGuestDates(DateRange reservationPeriod)
    {
        if (CustomerType != CustomerType.Guest)
            throw new DomainException("Only guests can have reservation dates.");

        StartDate = reservationPeriod.StartDate;
        EndDate = reservationPeriod.EndDate;
    }

    private static bool CalculateIsAdult(DateTime birthDate)
    {
        var today = DateTime.UtcNow.Date;
        var age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age)) age--;
        return age >= 18;
    }
}
