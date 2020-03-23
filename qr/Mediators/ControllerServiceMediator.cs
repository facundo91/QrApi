using System;
using System.Threading.Tasks;
using AutoMapper;
using qrAPI.Domain;
using qrAPI.Services;

namespace qrAPI.Mediators
{
    public class ControllerServiceMediator<TDomainObject> : IControllerServiceMediator<TDomainObject> 
        where TDomainObject : DomainObject
    {

        private readonly IMapper _mapper;
        private readonly IGenericService<TDomainObject> _service;

        public ControllerServiceMediator(IMapper mapper, IServiceFactory serviceFactory)
        {
            _mapper = mapper;
            _service = serviceFactory.GetService<TDomainObject>();
        }

        public async Task<T> GetAllAsync<T>()
        {
            var response = await _service.GetAllAsync();
            return _mapper.Map<T>(response);
        }

        public async Task<T> GetByIdAsync<T>(Guid id)
        {
            var domainObject = await _service.GetByIdAsync(id);
            return domainObject != null ? _mapper.Map<T>(domainObject) : default;
        }

        public async Task<T> CreateAsync<T>(object objToCreate)
        {
            var obj = _mapper.Map<TDomainObject>(objToCreate);
            var objCreated = await _service.CreateAsync(obj);
            return objCreated != null ? _mapper.Map<T>(objCreated) : default;
        }

        public async Task<bool> UpdateAsync(object objToUpdate)
        {
            var obj = _mapper.Map<TDomainObject>(objToUpdate);
            return await _service.UpdateAsync(obj);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}
