using System;
using System.Threading.Tasks;

namespace qrAPI.Infrastructure.Cache
{
    public interface ICacheHelper
    {
        Task<T> GetCachedAsync<T>(string cacheKey);
        Task<string> GetCachedAsync(string cacheKey);
        Task CacheAsync<T>(string cacheKey, T response, TimeSpan? timeToLive);
    }
}
