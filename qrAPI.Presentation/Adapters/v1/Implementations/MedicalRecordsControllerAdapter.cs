using System;
using System.Threading.Tasks;
using AutoMapper;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services.Interfaces;
using qrAPI.Presentation.Adapters.v1.Interfaces;

namespace qrAPI.Presentation.Adapters.v1.Implementations
{
    public class MedicalRecordsControllerAdapter : ControllerAdapter<MedicalRecord>, IMedicalRecordsControllerAdapter
    {
        private new readonly IMedicalRecordService _service;

        public MedicalRecordsControllerAdapter(IMapper mapper, IMedicalRecordService service) : base(mapper, service)
        {
            _service = service;
        }

        public async Task<T> CreateAsync<T>(Guid petId, CreateMedicalRecordRequest medicalRecord)
        {
            var obj = MapToTDomainObject(medicalRecord);
            var objCreated = await _service.CreateAsync(petId, obj);
            return MapToTObject<T>(objCreated);
        }
    }
}