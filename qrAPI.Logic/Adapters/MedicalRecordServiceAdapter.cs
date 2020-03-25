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
    public class MedicalRecordServiceAdapter : IMedicalRecordServiceAdapter
    {
        private readonly IMapper _mapper;
        private readonly IServiceAdapter<MedicalRecordDto> _serviceAdapter;
        private readonly IGenericRepository<MedicalRecordDto> _repository;

        public MedicalRecordServiceAdapter(IMapper mapper, IDataContext dataContext, IServiceAdapter<MedicalRecordDto> serviceAdapter)
        {
            _mapper = mapper;
            _serviceAdapter = serviceAdapter;
            _repository = dataContext.GetRepository<MedicalRecordDto>();
        }

        public async Task<MedicalRecord> CreateAsync(Guid petId, MedicalRecord medicalRecord)
        {
            var dto = MapToTDto(medicalRecord);
            dto.PetId = petId;
            var objCreated = await _repository.InsertAsync(dto);
            return MapToT<MedicalRecord>(objCreated);
        }

        public Task<IEnumerable<T>> GetAllAsync<T>()
            => _serviceAdapter.GetAllAsync<T>();

        public Task<T> GetByIdAsync<T>(Guid id)
            => _serviceAdapter.GetByIdAsync<T>(id);

        public Task<T> CreateAsync<T>(T objToCreate)
            => _serviceAdapter.CreateAsync(objToCreate);

        public Task<bool> UpdateAsync<T>(T objToUpdate)
            => _serviceAdapter.UpdateAsync(objToUpdate);

        public Task<bool> DeleteAsync(Guid id)
            => _serviceAdapter.DeleteAsync(id);

        private MedicalRecordDto MapToTDto(object objToMap)
            => objToMap != null ? _mapper.Map<MedicalRecordDto>(objToMap) : default;

        private T MapToT<T>(MedicalRecordDto result)
            => result != null ? _mapper.Map<T>(result) : default;
    }
}