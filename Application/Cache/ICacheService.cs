using Microsoft.Extensions.Caching.Distributed;

namespace Application.Cache;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string cacheKey, CancellationToken cancellationToken);
    Task SetAsync<T>(string cacheKey, T data, DistributedCacheEntryOptions options, CancellationToken cancellationToken);
    Task RemoveAllAsync(CancellationToken cancellationToken);
}