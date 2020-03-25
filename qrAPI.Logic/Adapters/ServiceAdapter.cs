using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using qrAPI.DAL.Data;
using qrAPI.DAL.Dtos;
using qrAPI.DAL.Repositories;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Adapters
{
    public class ServiceAdapter<TDomainObject, TDto> : IServiceAdapter<TDomainObject, TDto>
        where TDomainObject : DomainObject
        where TDto : Dto
    {
        private readonly IMapper _mapper;
        private protected readonly IGenericRepository<TDto> Repository;

        // ReSharper disable once MemberCanBeProtected.Global
        public ServiceAdapter(IMapper mapper, IDataContext dataContext)
        {
            _mapper = mapper;
            Repository = dataContext.GetRepository<TDto>();
        }

        public async Task<IEnumerable<TDomainObject>> GetAllAsync()
        {
            var result = await Repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDomainObject>>(result);
        }

        public async Task<TDomainObject> GetByIdAsync(Guid id)
        {
            var result = await Repository.GetByIdAsync(id);
            return MapToTDomainObject(result);
        }

        public async Task<TDomainObject> CreateAsync(TDomainObject objToCreate)
        {
            var dto = MapToTDto(objToCreate);
            var objCreated = await Repository.InsertAsync(dto);
            return MapToTDomainObject(objCreated);
        }

        public async Task<bool> UpdateAsync(TDomainObject objToUpdate)
        {
            var dto = MapToTDto(objToUpdate);
            return await Repository.UpdateAsync(dto);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await Repository.DeleteAsync(id);
        }

        protected TDomainObject MapToTDomainObject(TDto objToMap)
            => _mapper.Map<TDomainObject>(objToMap);

        protected TDto MapToTDto(TDomainObject objToMap)
            => objToMap != null ? _mapper.Map<TDto>(objToMap) : default;
    }
}
