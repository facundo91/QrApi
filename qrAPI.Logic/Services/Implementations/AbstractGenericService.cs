using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Dtos;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services.Interfaces;

namespace qrAPI.Logic.Services.Implementations
{
    public abstract class AbstractGenericService<TDomainObject, TDto> : IGenericService<TDomainObject>
    where TDomainObject : DomainObject
    where TDto : Dto
    {
        protected readonly IMapper Mapper;
        private readonly IRepository<TDto> _repository;

        protected AbstractGenericService(IMapper mapper, IRepository<TDto> repository)
        {
            Mapper = mapper;
            _repository = repository;
        }

        public virtual async Task<IEnumerable<TDomainObject>> GetAllAsync() => 
            Mapper.Map<IEnumerable<TDomainObject>>(await _repository.GetAllAsync());

        public virtual async Task<TDomainObject> GetByIdAsync(Guid id) =>
            Mapper.Map<TDomainObject>(await _repository.GetAllAsync());

        public virtual async Task<TDomainObject> CreateAsync(TDomainObject objToCreate) => 
            Mapper.Map<TDomainObject>(await _repository.InsertAsync(Mapper.Map<TDto>(objToCreate)));

        public virtual async Task<bool> UpdateAsync(Guid id, TDomainObject objToUpdate) => 
            await _repository.UpdateAsync(Mapper.Map<TDto>(objToUpdate.Id = id));

        public virtual async Task<bool> DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    }
}