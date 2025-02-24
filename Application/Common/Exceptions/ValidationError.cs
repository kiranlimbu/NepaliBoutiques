namespace Application.Common.Exceptions;

/// <summary>
/// Represents a validation error with the property name and error message.
/// </summary>
/// <param name="PropertyName">The name of the property that caused the validation error.</param>
/// <param name="ErrorMessage">The error message associated with the validation error.</param>
public sealed record ValidationError(string PropertyName, string ErrorMessage);
