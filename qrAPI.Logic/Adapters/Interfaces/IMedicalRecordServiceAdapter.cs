using System;
using System.Threading.Tasks;
using qrAPI.DAL.Dtos;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Adapters.Interfaces
{
    public interface IMedicalRecordServiceAdapter : IServiceAdapter<MedicalRecordDto>
    {
        Task<MedicalRecord> CreateAsync(Guid petId, MedicalRecord medicalRecord);
    }
}