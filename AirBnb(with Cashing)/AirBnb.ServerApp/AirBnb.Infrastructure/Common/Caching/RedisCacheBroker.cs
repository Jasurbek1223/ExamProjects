using AirBnb.Infrastructure.Common.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Text.Json;
using AirBnb.Persistence.Caching;

namespace AirBnb.Infrastructure.Common.Caching;

public class RedisCacheBroker(
    IDistributedCache distributedCache,
    IOptions<CacheSettings> cacheOptions) : ICacheBroker
{
    private readonly CacheSettings _cacheSettings = cacheOptions.Value;

    public async ValueTask DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        await distributedCache.RemoveAsync(key, cancellationToken);
    }

    public async ValueTask<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var value = await distributedCache.GetStringAsync(key, cancellationToken)
                    ?? throw new InvalidOperationException();

        return JsonSerializer.Deserialize<T>(value);
    }

    public async ValueTask<T?> GetOrSetAsync<T>(string key, Func<Task<T>> valueFactory,
        CancellationToken cancellationToken = default)
    {
        var stringValue = await distributedCache.GetStringAsync(key, cancellationToken);

        if (stringValue is not null)
            return JsonSerializer.Deserialize<T>(stringValue);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheSettings.AbsoluteExpirationTimeInMinutes),
            SlidingExpiration = TimeSpan.FromMinutes(_cacheSettings.SlidingExpirationTimeInMinutes)
        };

        var value = await valueFactory();
        await distributedCache.SetStringAsync(key, JsonSerializer.Serialize(value), options, cancellationToken);

        return value;
    }

    public async ValueTask SetAsync<T>(string key, T value, CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheSettings.AbsoluteExpirationTimeInMinutes),
            SlidingExpiration = TimeSpan.FromMinutes(_cacheSettings.SlidingExpirationTimeInMinutes)
        };

        await distributedCache.SetStringAsync(key, JsonSerializer.Serialize(value), options, cancellationToken);
    }
}