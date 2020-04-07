using System;
using System.Threading.Tasks;
using AutoMapper;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Dtos;
using qrAPI.Logic.Adapters.Interfaces;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Adapters.Implementations
{
    public class MedicalRecordServiceAdapter : ServiceAdapter<MedicalRecordDto>, IMedicalRecordServiceAdapter
    {
        private readonly IMapper _mapper;
        private readonly IRepository<MedicalRecordDto> _repository;

        public MedicalRecordServiceAdapter(IMapper mapper, IRepository<MedicalRecordDto> repository) 
            : base(mapper, repository)
        {
        }

        public async Task<MedicalRecord> CreateAsync(Guid petId, MedicalRecord medicalRecord)
        {
            var dto = MapToTDto(medicalRecord);
            dto.PetId = petId;
            var objCreated = await _repository.InsertAsync(dto);
            return MapToT<MedicalRecord>(objCreated);
        }

        private MedicalRecordDto MapToTDto(object objToMap)
            => objToMap != null ? _mapper.Map<MedicalRecordDto>(objToMap) : default;

        private T MapToT<T>(MedicalRecordDto result)
            => result != null ? _mapper.Map<T>(result) : default;

    }
}