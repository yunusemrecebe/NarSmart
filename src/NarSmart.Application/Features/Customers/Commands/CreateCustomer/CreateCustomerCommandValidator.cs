using FluentValidation;

namespace NarSmart.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(100);

        RuleFor(x => x.BirthDate)
            .LessThan(DateTime.UtcNow).WithMessage("Birth date must be in the past.");

        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.NationalId) || !string.IsNullOrWhiteSpace(x.PassportNumber))
            .WithMessage("At least a national ID or passport number is required.");
    }
}
