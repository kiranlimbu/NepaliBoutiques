using Microsoft.AspNetCore.Mvc;
using Application.Common.Exceptions;

namespace API.Middleware;

/// <summary>
/// Middleware for handling exceptions globally across the application.
/// Converts exceptions into consistent ProblemDetails responses.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the ExceptionHandlingMiddleware.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline</param>
    /// <param name="logger">Logger for recording exception details</param>
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Processes HTTP requests and handles any exceptions that occur.
    /// </summary>
    /// <param name="context">The HttpContext for the current request</param>
    /// <returns>A Task representing the asynchronous operation</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Continue processing the HTTP request
            await _next(context);
        }
        catch (Exception ex)
        {
            // Log the exception details
            _logger.LogError(ex, "An Exception occurred: {Message}", ex.Message);

            // Convert the exception into structured details
            var exceptionDetails = GetExceptionDetails(ex);

            // Create a ProblemDetails response following RFC 7807
            var problemDetails = new ProblemDetails
            {
                Status = exceptionDetails.StatusCode,
                Type = exceptionDetails.Type,
                Title = exceptionDetails.Title,
                Detail = exceptionDetails.Detail,
            };

            // Add validation errors if they exist
            if (exceptionDetails.Errors != null)
            {
                problemDetails.Extensions["errors"] = exceptionDetails.Errors;
            }

            // Set response status code and write ProblemDetails to response
            context.Response.StatusCode = exceptionDetails.StatusCode;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    /// <summary>
    /// Converts different exception types into standardized exception details.
    /// </summary>
    /// <param name="ex">The exception to convert</param>
    /// <returns>Structured exception details</returns>
    private static ExceptionDetails GetExceptionDetails(Exception ex)
    {
        return ex switch
        {
            // Handle validation exceptions with 400 Bad Request
            ValidationException validationException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "ValidationFailure",
                "Validation error",
                "One or more validation errors occurred",
                validationException.Errors),

            // Handle all other exceptions with 500 Internal Server Error
            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "ServerError",
                "Server error",
                "An unexpected error occurred",
                null
            )
        };
    }

    /// <summary>
    /// Record containing structured exception information for consistent error responses.
    /// </summary>
    /// <param name="StatusCode">HTTP status code for the response</param>
    /// <param name="Type">Error type identifier</param>
    /// <param name="Title">Brief error description</param>
    /// <param name="Detail">Detailed error message</param>
    /// <param name="Errors">Optional collection of specific error details</param>
    internal record ExceptionDetails(
        int StatusCode,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors
    );
}
