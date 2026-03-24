using FluentValidation;

namespace NarSmart.Application.Features.Sales.Commands.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("Room is required.");

        RuleFor(x => x.SalesPackageId)
            .NotEmpty().WithMessage("Sales package is required.");

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate).WithMessage("Start date must be before end date.");

        RuleFor(x => x.CustomerIds)
            .NotEmpty().WithMessage("At least one customer is required.");
    }
}
