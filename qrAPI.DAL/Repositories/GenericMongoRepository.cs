using MongoDB.Driver;
using qrAPI.DAL.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var foundObject = await _table.FindAsync<TDto>((FilterDefinition<TDto>) id);
            return null; //TODO
        }

        public async Task<TDto> InsertAsync(TDto obj)
        {
            obj.Id = Guid.NewGuid();
            await _table.InsertOneAsync(obj);
            return obj;
        }

        public async Task<TDto> UpdateAsync(TDto obj)
        {
            var update = Builders<TDto>.Update.Set(s => s, obj);
            var filter = Builders<TDto>.Filter.Eq(x => x.Id, obj.Id);
            UpdateResult actionResult
                = await _table.UpdateOneAsync(filter, update);
            if (actionResult.IsAcknowledged
                && actionResult.ModifiedCount > 0)
                return await Task.FromResult(obj);
            return null;
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var deleteResult = await _table.DeleteOneAsync(x => x.Id == (Guid) id);
            return deleteResult.DeletedCount > 0 && deleteResult.IsAcknowledged;
        }
    }
}