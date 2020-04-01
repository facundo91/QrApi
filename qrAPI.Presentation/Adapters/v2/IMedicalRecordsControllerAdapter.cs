using System;
using System.Threading.Tasks;
using qrAPI.Contracts.v2.Requests.Create;
using qrAPI.Logic.Domain;

namespace qrAPI.Presentation.Adapters.v2
{
    public interface IMedicalRecordsControllerAdapter : IControllerAdapter<MedicalRecord>
    {
        Task<T> CreateAsync<T>(Guid objToCreate, CreateMedicalRecordRequest medicalRecord);
    }
}