using FluentValidation;

namespace NarSmart.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("User id is required.");

        RuleFor(x => x.TerminationDate)
            .NotEmpty().WithMessage("Termination date is required.");
    }
}
