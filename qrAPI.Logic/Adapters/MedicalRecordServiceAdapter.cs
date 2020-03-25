using System;
using System.Threading.Tasks;
using AutoMapper;
using qrAPI.DAL.Data;
using qrAPI.DAL.Dtos;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Adapters
{
    public class MedicalRecordServiceAdapter : ServiceAdapter<MedicalRecord, MedicalRecordDto>, IMedicalRecordServiceAdapter
    {
        public MedicalRecordServiceAdapter(IMapper mapper, IDataContext dataContext) : base(mapper, dataContext)
        {
        }

        public async Task<MedicalRecord> CreateAsync(Guid petId, MedicalRecord medicalRecord)
        {
            var dto = MapToTDto(medicalRecord);
            dto.PetId = petId;
            var objCreated = await Repository.InsertAsync(dto);
            return MapToTDomainObject(objCreated);
        }
    }
}