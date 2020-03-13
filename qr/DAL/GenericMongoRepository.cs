using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using qr.Dtos;

namespace qr.DAL
{
    public class GenericMongoRepository<TSource, TDestination> : IGenericRepository<TSource, TDestination> 
        where TSource : Dto 
        where TDestination : class
    {
        private readonly IMongoCollection<TSource> _table;
        private readonly IMapper _mapper;

        public GenericMongoRepository(IMongoCollection<TSource> table, IMapper mapper)
        {
            _table = table;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<TDestination>> GetAllAsync()
        {
            var qrs = await _table.Find(_ => true).ToListAsync();
            return _mapper.Map<IEnumerable<TDestination>>(qrs);
        }

        public async Task<TDestination> GetByIdAsync(object id)
        {
            var foundObject = await _table.FindAsync<TSource>((FilterDefinition<TSource>) id);
            return _mapper.Map<TDestination>(foundObject);
        }

        public async Task<TDestination> InsertAsync(TDestination obj)
        {
            var objDto = _mapper.Map<TSource>(obj);
            objDto.Id = Guid.NewGuid();
            await _table.InsertOneAsync(objDto);
            return _mapper.Map<TDestination>(objDto);
        }

        public async Task<TDestination> UpdateAsync(TDestination obj)
        {
            var objDto = _mapper.Map<TSource>(obj);
            var update = Builders<TSource>.Update.Set(s => s, objDto);
            var filter = Builders<TSource>.Filter.Eq(x => x.Id, objDto.Id);
            UpdateResult actionResult
                = await _table.UpdateOneAsync(filter, update);
            if (actionResult.IsAcknowledged
                && actionResult.ModifiedCount > 0)
                return await Task.FromResult(obj);
            return null;
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var deleteResult = await _table.DeleteOneAsync(x => x.Id == (Guid)id);
            return deleteResult.DeletedCount > 0 && deleteResult.IsAcknowledged;
        }

    }
}