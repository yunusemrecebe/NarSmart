using FluentValidation;

namespace NarSmart.Application.Features.Auth.Queries.GetUserHotels;

public class GetUserHotelsQueryValidator : AbstractValidator<GetUserHotelsQuery>
{
    public GetUserHotelsQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.");
    }
}
