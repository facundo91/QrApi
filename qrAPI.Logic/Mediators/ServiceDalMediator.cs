using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using qrAPI.DAL.Data;
using qrAPI.DAL.Dtos;
using qrAPI.DAL.Repositories;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Mediators
{
    public class ServiceDalMediator<TDomainObject, TDto> : IServiceDalMediator<TDomainObject, TDto>
        where TDomainObject : DomainObject
        where TDto : Dto
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<TDto> _repository;

        public ServiceDalMediator(IMapper mapper, IDataContext dataContext)
        {
            _mapper = mapper;
            _repository = dataContext.GetRepository<TDto>();
        }

        public async Task<IEnumerable<TDomainObject>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<TDomainObject>>(await _repository.GetAllAsync());
        }

        public async Task<TDomainObject> GetByIdAsync(Guid id)
        {
            return _mapper.Map<TDomainObject>(await _repository.GetByIdAsync(id));
        }

        public async Task<TDomainObject> CreateAsync(TDomainObject objToCreate)
        {
            var dto = _mapper.Map<TDto>(objToCreate);
            var objCreated = await _repository.InsertAsync(dto);
            return _mapper.Map<TDomainObject>(objCreated);
        }

        public async Task<bool> UpdateAsync(TDomainObject objToUpdate)
        {
            var dto = _mapper.Map<TDto>(objToUpdate);
            return await _repository.UpdateAsync(dto);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
