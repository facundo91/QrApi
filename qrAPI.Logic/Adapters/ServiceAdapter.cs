using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using qrAPI.DAL.Data;
using qrAPI.DAL.Dtos;
using qrAPI.DAL.Repositories;

namespace qrAPI.Logic.Adapters
{
    public class ServiceAdapter<TDto> : IServiceAdapter<TDto> where TDto : Dto
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<TDto> _repository;

        public ServiceAdapter(IMapper mapper, IDataContext dataContext)
        {
            _mapper = mapper;
            _repository = dataContext.GetRepository<TDto>();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var result = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<T>>(result);
        }

        public async Task<T> GetByIdAsync<T>(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);
            return MapToT<T>(result);
        }

        public async Task<T> CreateAsync<T>(T objToCreate)
        {
            var dto = MapToTDto(objToCreate);
            var objCreated = await _repository.InsertAsync(dto);
            return MapToT<T>(objCreated);
        }

        public async Task<bool> UpdateAsync<T>(T objToUpdate)
        {
            var dto = MapToTDto(objToUpdate);
            return await _repository.UpdateAsync(dto);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        private TDto MapToTDto(object objToMap)
            => objToMap != null ? _mapper.Map<TDto>(objToMap) : default;

        private T MapToT<T>(TDto result)
            => result != null ? _mapper.Map<T>(result) : default;

    }
}
