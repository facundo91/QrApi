using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Dtos;
using qrAPI.Logic.Adapters.Interfaces;

namespace qrAPI.Logic.Adapters.Implementations
{
    public class ServiceAdapter<TDto> : IServiceAdapter<TDto> where TDto : Dto
    {
        protected readonly IMapper _mapper;
        protected readonly IRepository<TDto> _repository;

        public ServiceAdapter(IMapper mapper, IRepository<TDto> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var result = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<T>>(result);
        }

        public virtual async Task<T> GetByIdAsync<T>(Guid id)
        {
            var result = await _repository.GetAsync(id);
            return MapToT<T>(result);
        }

        public virtual async Task<T> CreateAsync<T>(T objToCreate)
        {
            var dto = MapToTDto(objToCreate);
            var objCreated = await _repository.InsertAsync(dto);
            return MapToT<T>(objCreated);
        }

        public virtual async Task<bool> UpdateAsync<T>(T objToUpdate)
        {
            var dto = MapToTDto(objToUpdate);
            return await _repository.UpdateAsync(dto);
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        private TDto MapToTDto(object objToMap)
            => objToMap != null ? _mapper.Map<TDto>(objToMap) : default;
        protected T MapToT<T>(TDto result)
            => result != null ? _mapper.Map<T>(result) : default;

    }
}
