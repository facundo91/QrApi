using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qrAPI.DAL.Dtos;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Mediators;

namespace qrAPI.Logic.Services
{
    public class AbstractGenericService<TDomainObject, TDto> : IGenericService<TDomainObject>
    where TDomainObject : DomainObject
    where TDto : Dto
    {

        private readonly IServiceDalMediator<TDomainObject, TDto> _serviceDalMediator;

        protected AbstractGenericService(IServiceDalMediator<TDomainObject, TDto> serviceDalMediator)
        {
            _serviceDalMediator = serviceDalMediator;
        }
        public virtual async Task<IEnumerable<TDomainObject>> GetAllAsync()
        {
            return await _serviceDalMediator.GetAllAsync();
        }

        public async Task<TDomainObject> GetByIdAsync(Guid id)
        {
            return await _serviceDalMediator.GetByIdAsync(id);
        }

        public async Task<TDomainObject> CreateAsync(TDomainObject objToCreate)
        {
            return await _serviceDalMediator.CreateAsync(objToCreate);
        }

        public async Task<bool> UpdateAsync(TDomainObject objToUpdate)
        {
            return await _serviceDalMediator.UpdateAsync(objToUpdate);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _serviceDalMediator.DeleteAsync(id);
        }
    }
}