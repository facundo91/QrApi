using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Dtos;
using qrAPI.Infrastructure.Cache;
using qrAPI.Infrastructure.Settings;

namespace qrAPI.DAL.Daos.CacheImplementations
{
    public class CacheRepository<T> : IRepository<T> where T : Dto
    {
        private readonly IRepository<T> _repository;
        private readonly MemoryCacheSettings _memoryCacheOptions;
        private readonly ICacheHelper _cacheHelper;

        public CacheRepository(IRepository<T> repository, MemoryCacheSettings memoryCacheOptions, ICacheHelper cacheHelper)
        {
            _repository = repository;
            _memoryCacheOptions = memoryCacheOptions;
            _cacheHelper = cacheHelper;
        }

        public async Task<IEnumerable<T>> GetAllAsync() => 
            await GetAllAndCacheAsync(async () => await _repository.GetAllAsync(), GenerateCacheKeyFromQuery);

        public async Task<T> GetAsync(object id) =>
            await GetAndCacheAsync(async () => await _repository.GetAsync(id),
                () => GenerateCacheKeyFromQuery((Guid)id));

        public Task<T> InsertAsync(T obj) => _repository.InsertAsync(obj);

        public Task<bool> UpdateAsync(T obj) => _repository.UpdateAsync(obj);

        public Task<bool> DeleteAsync(object id) => _repository.DeleteAsync(id);

        protected async Task<IEnumerable<T>> GetAllAndCacheAsync(Func<Task<IEnumerable<T>>> function, Func<string> keyGeneratorFunction)
        {
            if (!_memoryCacheOptions.Enabled) return await function.Invoke();
            var cacheKey = keyGeneratorFunction.Invoke();
            var cachedResponse = await _cacheHelper.GetCachedAsync<IEnumerable<T>>(cacheKey);
            if (cachedResponse != null) return cachedResponse;
            var repositoryResponse = await function.Invoke();
            await _cacheHelper.CacheAsync(cacheKey, repositoryResponse,null);
            return repositoryResponse;
        }

        private async Task<T> GetAndCacheAsync(Func<Task<T>> function, Func<string> keyGeneratorFunction)
        {
            if (!_memoryCacheOptions.Enabled) return await function.Invoke();
            var cacheKey = keyGeneratorFunction.Invoke();
            var cachedResponse = await _cacheHelper.GetCachedAsync<T>(cacheKey);
            if (cachedResponse != null) return cachedResponse;
            var repositoryResponse = await function.Invoke();
            await _cacheHelper.CacheAsync(cacheKey, repositoryResponse, null);
            return repositoryResponse;
        }

        private static string GenerateCacheKeyFromQuery() => typeof(T).Name;

        private static string GenerateCacheKeyFromQuery(Guid id) => $"{typeof(T).Name}-{id}";

        protected static string GenerateCacheKeyFromQuery(string key) => $"{typeof(T).Name}-{key}";
    }
}
