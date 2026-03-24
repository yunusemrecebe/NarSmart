using FluentValidation;

namespace NarSmart.Application.Features.Sales.Commands.AddGuestToSale;

public class AddGuestToSaleCommandValidator : AbstractValidator<AddGuestToSaleCommand>
{
    public AddGuestToSaleCommandValidator()
    {
        RuleFor(x => x.SaleId)
            .NotEmpty().WithMessage("Sale id is required.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(100);

        RuleFor(x => x.BirthDate)
            .LessThan(DateTime.UtcNow).WithMessage("Birth date must be in the past.");
    }
}
