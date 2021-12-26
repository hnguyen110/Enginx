using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace API.Extensions.RedisDistributedCache;

public static class DistributedCacheExtensions
{
    public static async Task SetRecordAsync<T>(this IDistributedCache cache, string id, T data, TimeSpan? expiredTime,
        TimeSpan? unusedExpiredTime)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiredTime ?? TimeSpan.FromSeconds(60),
            SlidingExpiration = unusedExpiredTime
        };
        await cache.SetStringAsync(id, JsonSerializer.Serialize(data), options);
    }

    public static async Task<T?> GetRecordAsync<T>(this IDistributedCache cache, string id)
    {
        var data = await cache.GetStringAsync(id);
        return data is null ? default : JsonSerializer.Deserialize<T>(data);
    }
}