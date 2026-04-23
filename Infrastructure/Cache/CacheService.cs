using System.Text;
using System.Text.Json;
using Application;
using Application.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Cache;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly IAuthenticatedUser _currentUserService;
    private readonly ILogger<CacheService> _logger;

    public CacheService(
        IDistributedCache cache,
        ILogger<CacheService> logger,
        IAuthenticatedUser currentUserService)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<T?> GetAsync<T>(string cacheKey, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserService.GetCurrentUser();
        var cachedResponse =
            await _cache.GetAsync(currentUser.CompanyId + cacheKey.ToLower(), cancellationToken);

        if (cachedResponse != null)
        {
            return JsonSerializer.Deserialize<T>(Encoding.Default.GetString(cachedResponse));
        }

        return default;
    }

    public async Task SetAsync<T>(
        string cacheKey,
        T data,
        DistributedCacheEntryOptions options,
        CancellationToken cancellationToken)
    {
        var serializedData = Encoding.Default.GetBytes(JsonSerializer.Serialize(data));

        var currentUser = _currentUserService.GetCurrentUser();
        await
            _cache.SetAsync(currentUser.CompanyId + cacheKey.ToLower(), serializedData, options, cancellationToken);
        await TrackAllKeyAsync(currentUser.CompanyId + cacheKey.ToLower(), cancellationToken);
    }

    private async Task TrackAllKeyAsync(string cacheKey, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserService.GetCurrentUser();
        var keyList = await GetAllTrackedKeysAsync(cancellationToken);
        if (!keyList.Contains(currentUser.CompanyId + cacheKey))
        {
            keyList.Add(cacheKey);
            await SaveAllTrackedKeysAsync(keyList, cancellationToken);
        }
    }

    private async Task<List<string>> GetAllTrackedKeysAsync(CancellationToken cancellationToken)
    {
        var currentUser = _currentUserService.GetCurrentUser();
        var cachedKeys = await _cache.GetAsync(currentUser.CompanyId, cancellationToken);
        if (cachedKeys != null)
        {
            return JsonSerializer.Deserialize<List<string>>(Encoding.Default.GetString(cachedKeys)) ??
                   new List<string>();
        }

        return new List<string>();
    }

    private async Task SaveAllTrackedKeysAsync(List<string> keys, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserService.GetCurrentUser();
        var serializedKeys = Encoding.Default.GetBytes(JsonSerializer.Serialize(keys));
        await _cache.SetAsync(
            currentUser.CompanyId,
            serializedKeys,
            new DistributedCacheEntryOptions(),
            cancellationToken);
    }

    public async Task RemoveAllAsync(CancellationToken cancellationToken)
    {
        var keys = await GetAllTrackedKeysAsync(cancellationToken);
        foreach (var key in keys)
        {
            await _cache.RemoveAsync(key, cancellationToken);
        }

        var currentUser = _currentUserService.GetCurrentUser();
        await _cache.RemoveAsync(currentUser.CompanyId, cancellationToken);
    }
}