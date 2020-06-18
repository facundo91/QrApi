using AutoMapper;
using Microsoft.Extensions.Logging;
using qrAPI.App.Domain;
using qrAPI.App.Services.Interfaces;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qrAPI.App.Services.Implementations
{
    public abstract class AbstractGenericService<TDomainObject, TDto> : IGenericService<TDomainObject>
    where TDomainObject : DomainObject
    where TDto : Dto
    {
        protected readonly IMapper Mapper;
        private readonly IRepository<TDto> _repository;
        private readonly ILogger<AbstractGenericService<TDomainObject, TDto>> _logger;

        protected AbstractGenericService(
            IMapper mapper,
            IRepository<TDto> repository,
            ILogger<AbstractGenericService<TDomainObject, TDto>> logger)
        {
            Mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public virtual async Task<IEnumerable<TDomainObject>> GetAllAsync() =>
            Mapper.Map<IEnumerable<TDomainObject>>(await _repository.GetAllAsync());

        public async virtual Task<TDomainObject> GetByIdAsync(Guid id) =>
            Mapper.Map<TDomainObject>(await _repository.GetAsync(id));

        public virtual async Task<TDomainObject> CreateAsync(TDomainObject objToCreate) =>
            Mapper.Map<TDomainObject>(await _repository.InsertAsync(Mapper.Map<TDto>(objToCreate)));

        public async virtual Task<bool> UpdateAsync(Guid id, TDomainObject objToUpdate)
        {
            objToUpdate.Id = id;
            return await _repository.UpdateAsync(Mapper.Map<TDto>(objToUpdate));
        }

        public async virtual Task<bool> DeleteAsync(Guid id) =>
            await _repository.DeleteAsync(id);
    }
}