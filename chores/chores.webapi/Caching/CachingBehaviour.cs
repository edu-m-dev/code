using System.Text.Json;
using chores.webapi.Handlers;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IDistributedCache _cache;

    public CachingBehavior(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Only cache queries (not commands)
        if (request is GetUserChoresQuery query)
        {
            var key = $"chores:{query.User.Identity?.Name}";
            var cached = await _cache.GetStringAsync(key, cancellationToken);

            if (cached != null)
                return JsonSerializer.Deserialize<TResponse>(cached)!;

            var response = await next();
            await _cache.SetStringAsync(key, JsonSerializer.Serialize(response), cancellationToken);
            return response;
        }

        // For commands, run handler then invalidate cache
        if (request is AddUserChoreCommand cmd)
        {
            var response = await next();
            var key = $"chores:{cmd.User.Identity?.Name}";
            await _cache.RemoveAsync(key, cancellationToken);
            return response;
        }

        // Default: just run handler
        return await next();
    }
}
