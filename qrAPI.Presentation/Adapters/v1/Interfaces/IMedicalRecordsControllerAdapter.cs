using System;
using System.Threading.Tasks;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Logic.Domain;

namespace qrAPI.Presentation.Adapters.v1.Interfaces
{
    public interface IMedicalRecordsControllerAdapter : IControllerAdapter<MedicalRecord>
    {
        Task<T> CreateAsync<T>(Guid objToCreate, CreateMedicalRecordRequest medicalRecord);
    }
}