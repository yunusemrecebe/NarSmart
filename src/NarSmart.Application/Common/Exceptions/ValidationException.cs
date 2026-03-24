using FluentValidation.Results;

namespace NarSmart.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public List<string> Errors { get; }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : base("One or more validation errors occurred.")
    {
        Errors = failures.Select(f => f.ErrorMessage).ToList();
    }

    public ValidationException(string error)
        : base(error)
    {
        Errors = new List<string> { error };
    }
}
