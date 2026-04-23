
namespace Application.Cache;

public interface ICacheLayer
{
    void ResetCache();

    T? GetCache<T>(string key) where T : class;

    void SetToCache<T>(string key, T value, TimeSpan expiredTime);
}