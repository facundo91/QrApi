using System;
using System.Threading.Tasks;
using qrAPI.DAL.Dtos;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Adapters
{
    public interface IMedicalRecordServiceAdapter : IServiceAdapter<MedicalRecord, MedicalRecordDto>
    {
        Task<MedicalRecord> CreateAsync(Guid petId, MedicalRecord medicalRecord);
    }
}