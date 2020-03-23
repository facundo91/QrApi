using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Repositories
{
    public class GenericMongoRepository<TDto> : IGenericRepository<TDto>
        where TDto : Dto
    {
        private readonly IMongoCollection<TDto> _table;

        public GenericMongoRepository(IMongoCollection<TDto> table)
        {
            _table = table;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            return await _table.Find(_ => true).ToListAsync();
        }

        public async Task<TDto> GetByIdAsync(object id)
        {
            return await _table
                .Find((FilterDefinition<TDto>)id).FirstOrDefaultAsync();
        }

        public async Task<TDto> InsertAsync(TDto obj)
        {
            obj.Id = Guid.NewGuid();
            await _table.InsertOneAsync(obj);
            return obj;
        }

        public async Task<bool> UpdateAsync(TDto obj)
        {
            var update = Builders<TDto>.Update.Set(s => s, obj);
            var filter = Builders<TDto>.Filter.Eq(x => x.Id, obj.Id);
            UpdateResult actionResult
                = await _table.UpdateOneAsync(filter, update);
            return actionResult.IsAcknowledged
                && actionResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var deleteResult = await _table.DeleteOneAsync(x => x.Id == (Guid)id);
            return deleteResult.DeletedCount > 0 && deleteResult.IsAcknowledged;
        }
    }
}