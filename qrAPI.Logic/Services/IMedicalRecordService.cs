using System;
using System.Threading.Tasks;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Services
{
    public interface IMedicalRecordService : IGenericService<MedicalRecord>
    {
        Task<MedicalRecord> CreateAsync(Guid petId, MedicalRecord medicalRecord);
    }
}