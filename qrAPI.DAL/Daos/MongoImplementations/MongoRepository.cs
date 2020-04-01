using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Daos.MongoImplementations
{
    public class MongoRepository<TDto> : IRepository<TDto> where TDto : Dto
    {
        private readonly IMongoCollection<TDto> _table;

        public MongoRepository(IMongoCollection<TDto> table)
        {
            _table = table;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            return await _table.Find(_ => true).ToListAsync();
        }

        public async Task<TDto> GetAsync(object id)
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