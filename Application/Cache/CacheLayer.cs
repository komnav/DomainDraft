using Microsoft.Extensions.Caching.Memory;

namespace Application.Cache;

public class CacheLayer(IMemoryCache memoryCache) : ICacheLayer
{
    public void RemoveCache(string key)
    {
        memoryCache.Remove(key);
    }

    public T? GetCache<T>(string key) where T : class
    {
        memoryCache.TryGetValue(key, out T? value);
        return value;
    }

    public void SetToCache<T>(string key, T value, TimeSpan expiredTime) where T : class
    {
        memoryCache.Set(key, value, expiredTime);
    }
}