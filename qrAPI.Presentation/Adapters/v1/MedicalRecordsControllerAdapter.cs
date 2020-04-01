using System;
using System.Threading.Tasks;
using AutoMapper;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Factory;
using qrAPI.Logic.Services;

namespace qrAPI.Presentation.Adapters.v1
{
    public class MedicalRecordsControllerAdapter : ControllerAdapter<MedicalRecord>, IMedicalRecordsControllerAdapter
    {
        private readonly IMedicalRecordService _service;

        public MedicalRecordsControllerAdapter(IMapper mapper, IServiceFactory serviceFactory) : base(mapper)
        {
            _service = serviceFactory.GetMedicalRecordService();
        }

        public async Task<T> CreateAsync<T>(Guid petId, CreateMedicalRecordRequest medicalRecord)
        {
            var obj = MapToTDomainObject(medicalRecord);
            var objCreated = await _service.CreateAsync(petId, obj);
            return MapToTObject<T>(objCreated);
        }
    }
}