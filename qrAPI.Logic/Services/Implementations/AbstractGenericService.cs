using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Dtos;
using qrAPI.Infrastructure.Adapters;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services.Interfaces;

namespace qrAPI.Logic.Services.Implementations
{
    public abstract class AbstractGenericService<TDomainObject, TDto> : IGenericService<TDomainObject>
    where TDomainObject : DomainObject
    where TDto : Dto
    {

        protected readonly IMapperAdapter MapperAdapter;
        private readonly IRepository<TDto> _repository;

        protected AbstractGenericService(IMapperAdapter mapperAdapter, IRepository<TDto> repository)
        {
            MapperAdapter = mapperAdapter;
            _repository = repository;
        }

        public virtual async Task<IEnumerable<TDomainObject>> GetAllAsync() =>
            await MapperAdapter.DoMapAsync<IEnumerable<TDomainObject>>(async () => await _repository.GetAllAsync());

        public virtual async Task<TDomainObject> GetByIdAsync(Guid id) =>
            await MapperAdapter.DoMapAsync<TDomainObject>(async () => await _repository.GetAsync(id));

        public virtual async Task<TDomainObject> CreateAsync(TDomainObject objToCreate) => 
            await MapperAdapter.MapDoMapAsync<TDto, TDomainObject>(objToCreate, async mapped => await _repository.InsertAsync(mapped));

        public virtual async Task<bool> UpdateAsync(Guid id, TDomainObject objToUpdate)
        {
            objToUpdate.Id = id;
            return await MapperAdapter.MapDoAsync<TDto, bool>(objToUpdate, async mapped => await _repository.UpdateAsync(mapped));
        }

        public virtual async Task<bool> DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    }
}