using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Dtos;
using qrAPI.Infrastructure.Cache;
using qrAPI.Infrastructure.Settings;

namespace qrAPI.DAL.Daos.CacheImplementations
{
    public class PetCacheRepository : CacheRepository<PetDto>, IPetRepository
    {
        private readonly IPetRepository _repository;

        public PetCacheRepository(IPetRepository repository, MemoryCacheSettings memoryCacheOptions, ICacheHelper cacheHelper) 
            : base(repository, memoryCacheOptions, cacheHelper)
        {
            _repository = repository;
        }


        public async Task<IEnumerable<PetDto>> GetAllByNameAsync(string name)
        {
            return await GetAllAndCacheAsync(async () => await _repository.GetAllByNameAsync(name),
                () => GenerateCacheKeyFromQuery($"|name-{name}"));
        }

        public async Task<IEnumerable<PetDto>> GetAllByUserIdAsync(Guid userId)
        {
            return await GetAllAndCacheAsync(async () => await _repository.GetAllByUserIdAsync(userId),
                () => GenerateCacheKeyFromQuery($"|userId-{userId}"));
        }
    }
}
