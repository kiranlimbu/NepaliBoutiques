using MediatR;
using Application.Abstractions.Caching;
using Core.Abstractions;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors;

public class QueryCachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
where TRequest : ICachedQuery
where TResponse : Result
{
    private readonly ICacheService _cacheService;
    private readonly ILogger<QueryCachingBehavior<TRequest, TResponse>> _logger;

    public QueryCachingBehavior(ICacheService cacheService, ILogger<QueryCachingBehavior<TRequest, TResponse>> logger)
    {
        _cacheService = cacheService;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse? cachedResult = await _cacheService.GetAsync<TResponse>(request.CacheKey, cancellationToken);

        string name = typeof(TRequest).Name;
        if (cachedResult is not null)
        {
            _logger.LogInformation("Cache hit for {Query}", name);
            return cachedResult;
        }
        _logger.LogInformation("Cache miss for {Query}", name);

        var result = await next();
        if (result.IsSuccess)
        {
            await _cacheService.SetAsync(request.CacheKey, result, request.Expiration, cancellationToken);
        }
        
        return result;
    }
}