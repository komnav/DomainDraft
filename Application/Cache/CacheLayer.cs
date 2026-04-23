using Microsoft.Extensions.Caching.Memory;

namespace Application.Cache;

public class CacheLayer(IMemoryCache memoryCache) : ICacheLayer
{
    private List<string> _keys = [];

    public void ResetCache()
    {
        foreach (var key in _keys)
        {
            memoryCache.Remove(key);
        }

        _keys.Clear();
    }

    public T? GetCache<T>(string key) where T : class
    {
        memoryCache.TryGetValue(key, out T? value);
        return value;
    }

    public void SetToCache<T>(string key, T value, TimeSpan expiredTime)
    {
        memoryCache.Set(key, value, expiredTime);
        _keys.Add(key);
    }
}