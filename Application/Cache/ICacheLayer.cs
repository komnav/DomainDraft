
namespace Application.Cache;

public interface ICacheLayer
{
    void RemoveCache(string key);

    T? GetCache<T>(string key) where T : class;

    void SetToCache<T>(string key, T value, TimeSpan expiredTime);
    
    (bool KeyExist, T? Value) TryGet<T>(string key);
}