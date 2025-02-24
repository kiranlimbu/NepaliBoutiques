using MediatR;
using Application.Abstractions;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors;

/// <summary>
/// Represents a logging behavior for MediatR pipeline that logs the execution of commands.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseCommand
{
    private readonly ILogger<TRequest> _logger;

    
    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Handles the logging of command execution within the MediatR pipeline.
    /// </summary>
    /// <param name="request">The command request being handled.</param>
    /// <param name="next">The next delegate in the pipeline.</param>
    /// <param name="cancellationToken">Token to monitor for cancellation requests.</param>
    /// <returns>The response from the next delegate in the pipeline.</returns>
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        // Get the name of the command being executed
        var name = request.GetType().Name;

        try
        {
            // Log the start of command execution
            _logger.LogInformation("Executing command {CommandName}", name);

            // Execute the next delegate in the pipeline
            var result = await next();

            // Log the successful execution of the command
            _logger.LogInformation("Command {CommandName} executed successfully", name);

            return result;
        }
        catch (Exception ex)
        {
            // Log any exception that occurs during command execution
            _logger.LogError(ex, "Command {CommandName} execution failed", name);
            throw;
        }
    }
}

