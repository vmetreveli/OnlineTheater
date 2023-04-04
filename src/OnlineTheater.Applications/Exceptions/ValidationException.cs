using FluentValidation.Results;

namespace OnlineTheater.Applications.Exceptions;

public sealed class ValidationException : Exception
{
    public ValidationException(IEnumerable<ValidationFailure> failures)
        : base("One or more validation failures has occurred.") =>
        Errors = failures
            .Distinct()
            .Select(failure => Error.Failure(failure.ErrorCode, failure.ErrorMessage))
            .ToArray();

    public IReadOnlyCollection<Error> Errors { get; }
}