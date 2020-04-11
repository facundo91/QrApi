using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Data.MongoData;
using qrAPI.DAL.Dtos;
using qrAPI.DAL.Options;

namespace qrAPI.DAL.Daos.MongoImplementations
{
    public class MongoRepository<TDto> : IRepository<TDto> where TDto : Dto
    {
        private readonly IMongoCollection<TDto> _table;

        public MongoRepository(MongoOptions settings)
        {
            var client = new MongoClient(settings.DefaultConnection);
            _table = client.GetDatabase(settings.Database).GetCollection<TDto>(typeof(TDto).Name);
        }

        protected async Task<IEnumerable<TDto>> GetAllByQueryExpressionAsync(Expression<Func<TDto, bool>> expression)
        {
            return (await _table.FindAsync(expression)).Current;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            return await _table.Find(_ => true).ToListAsync();
        }

        public async Task<TDto> GetAsync(object id)
        {
            return (await _table.FindAsync(x => x.Id == (Guid) id))
                .Current.FirstOrDefault();
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