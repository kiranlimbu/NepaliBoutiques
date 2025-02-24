using MediatR;
using FluentValidation;
using Application.Abstractions; 
using Application.Common.Exceptions;

namespace Application.Common.Behaviors;

/// <summary>
/// Represents a validation behavior for MediatR pipeline that validates requests using FluentValidation.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;


    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <summary>
    /// Handles the validation of the request within the MediatR pipeline.
    /// </summary>
    /// <param name="request">The request being handled.</param>
    /// <param name="next">The next delegate in the pipeline.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>The response from the next delegate in the pipeline.</returns>
    /// <exception cref="ValidationException">Thrown when validation errors are found.</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // If there are no validators, proceed to the next delegate in the pipeline
        if (!_validators.Any())
        {
            return await next();
        }

        // Create a validation context for the request
        var context = new ValidationContext<TRequest>(request);

        // Validate the request using all available validators
        var validationErrors = _validators
            // Execute validation for each validator and get the validation result
            .Select(validator => validator.Validate(context))
            // Filter out the validation results that have errors
            .Where(result => result.Errors.Count > 0)
            // Flatten the list of validation errors
            .SelectMany(result => result.Errors)
            // Map the validation errors to the ValidationError type
            .Select(error => new ValidationError(error.PropertyName, error.ErrorMessage))
            // Convert the IEnumerable<ValidationError> to a List<ValidationError>
            .ToList();

        // If validation errors are found, throw a ValidationException
        if (validationErrors.Count != 0)
        {
            throw new Exceptions.ValidationException(validationErrors);
        }

        // Proceed to the next delegate in the pipeline
        return await next();
    }
}




