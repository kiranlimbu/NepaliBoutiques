using Application.Common.Behaviors;

namespace Application.Common.Exceptions;

/// <summary>
/// Represents an exception that is thrown when validation errors occur.
/// </summary>
public sealed class ValidationException : Exception
{
    public ValidationException(IEnumerable<ValidationError> errors)
    {
        Errors = errors;
    }

    /// <summary>
    /// Gets the collection of validation errors.
    /// </summary>
    public IEnumerable<ValidationError> Errors { get; }
}


