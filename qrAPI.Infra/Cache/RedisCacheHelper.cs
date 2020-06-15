using Microsoft.Extensions.Caching.Distributed;
using qrAPI.Infra.Settings;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace qrAPI.Infra.Cache
{
    public class RedisCacheHelper : ICacheHelper
    {
        private readonly IDistributedCache _distributedCache;
        private readonly MemoryCacheSettings _memoryCacheSettings;

        public RedisCacheHelper(IDistributedCache distributedCache, MemoryCacheSettings memoryCacheSettings)
        {
            _distributedCache = distributedCache;
            _memoryCacheSettings = memoryCacheSettings;
        }

        public async Task<T> GetCachedAsync<T>(string cacheKey)
        {
            var cachedResponse = await _distributedCache.GetStringAsync(cacheKey);
            return string.IsNullOrEmpty(cachedResponse) ? default : JsonSerializer.Deserialize<T>(cachedResponse);
        }

        public async Task<string> GetCachedAsync(string cacheKey)
        {
            var cachedResponse = await _distributedCache.GetStringAsync(cacheKey);
            return string.IsNullOrEmpty(cachedResponse) ? default : cachedResponse;
        }

        public async Task CacheAsync<T>(string cacheKey, T response, TimeSpan? timeToLive)
        {
            if (response == null) return;
            var serializedResponse = JsonSerializer.Serialize(response);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive ?? _memoryCacheSettings.AbsoluteExpirationHours,
                SlidingExpiration = timeToLive ?? _memoryCacheSettings.SlidingExpiration,
            };
            await _distributedCache.SetStringAsync(cacheKey, serializedResponse, options);
        }
    }
}