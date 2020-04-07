﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services.Interfaces;
using qrAPI.Presentation.Adapters.v1.Interfaces;

namespace qrAPI.Presentation.Adapters.v1.Implementations
{
    public class ControllerAdapter<TDomainObject> : IControllerAdapter<TDomainObject> where TDomainObject : DomainObject
    {
        protected readonly IGenericService<TDomainObject> _service;
        protected readonly IMapper _mapper;

        public ControllerAdapter(IMapper mapper, IGenericService<TDomainObject> service)
        {
            _mapper = mapper;
            _service = service;
        }

        public virtual async Task<T> GetAllAsync<T>()
        {
            var response = await _service.GetAllAsync();
            return _mapper.Map<T>(response);
        }

        public virtual async Task<T> GetByIdAsync<T>(Guid id)
        {
            var domainObject = await _service.GetByIdAsync(id);
            return MapToTObject<T>(domainObject);
        }

        public virtual async Task<T> CreateAsync<T>(object objToCreate)
        {
            var obj = MapToTDomainObject(objToCreate);
            var objCreated = await _service.CreateAsync(obj);
            return MapToTObject<T>(objCreated);
        }

        public virtual async Task<bool> UpdateAsync(Guid id, object objToUpdate)
        {
            var obj = MapToTDomainObject(objToUpdate);
            obj.Id = id;
            return await _service.UpdateAsync(obj);
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            return await _service.DeleteAsync(id);
        }

        protected TDomainObject MapToTDomainObject(object objToMap)
            => _mapper.Map<TDomainObject>(objToMap);

        protected T MapToTObject<T>(TDomainObject objToMap)
            => objToMap != null ? _mapper.Map<T>(objToMap) : default;

    }
}