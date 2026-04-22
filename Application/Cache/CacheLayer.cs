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

    public void SetToCache<T>(string key, T value, TimeSpan expiredTime)
    {
        memoryCache.Set(key, value, expiredTime);
    }
    
    public (bool KeyExist, T? Value) TryGet<T>(string key)
    {
         memoryCache.TryGetValue(key, out T? valueFromCache);

        return (true, valueFromCache);
    }
}