namespace AirBnb.Persistance.Caching;

public interface ICacheBroker
{
    ValueTask<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);

    ValueTask<T?> GetOrSetAsync<T>(string key, Func<Task<T>> valueFactory,
        CancellationToken cancellationToken = default);

    ValueTask SetAsync<T>(string key, T value, CancellationToken cancellationToken = default);

    ValueTask DeleteAsync(string key, CancellationToken cancellationToken = default);
}