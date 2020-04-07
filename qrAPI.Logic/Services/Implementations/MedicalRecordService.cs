using System;
using System.Threading.Tasks;
using qrAPI.DAL.Dtos;
using qrAPI.Logic.Adapters.Interfaces;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services.Interfaces;

namespace qrAPI.Logic.Services.Implementations
{
    public class MedicalRecordService : AbstractGenericService<MedicalRecord, MedicalRecordDto> , IMedicalRecordService
    {
        private readonly IMedicalRecordServiceAdapter _serviceToDalAdapter;
        public MedicalRecordService(IMedicalRecordServiceAdapter serviceToDalAdapter) : base(serviceToDalAdapter)
        {
            _serviceToDalAdapter = serviceToDalAdapter;
        }

        public async Task<MedicalRecord> CreateAsync(Guid petId, MedicalRecord medicalRecord)
        {
            return await _serviceToDalAdapter.CreateAsync(petId, medicalRecord);
        }
    }
}