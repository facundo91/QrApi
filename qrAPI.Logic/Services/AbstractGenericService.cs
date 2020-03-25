using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qrAPI.DAL.Dtos;
using qrAPI.Logic.Adapters;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Services
{
    public abstract class AbstractGenericService<TDomainObject, TDto> : IGenericService<TDomainObject>
    where TDomainObject : DomainObject
    where TDto : Dto
    {

        private readonly IServiceAdapter<TDomainObject, TDto> _serviceToDalAdapter;

        protected AbstractGenericService(IServiceAdapter<TDomainObject, TDto> serviceToDalAdapter)
        {
            _serviceToDalAdapter = serviceToDalAdapter;
        }

        public virtual async Task<IEnumerable<TDomainObject>> GetAllAsync()
        {
            return await _serviceToDalAdapter.GetAllAsync();
        }

        public async Task<TDomainObject> GetByIdAsync(Guid id)
        {
            return await _serviceToDalAdapter.GetByIdAsync(id);
        }

        public async Task<TDomainObject> CreateAsync(TDomainObject objToCreate)
        {
            return await _serviceToDalAdapter.CreateAsync(objToCreate);
        }

        public async Task<bool> UpdateAsync(TDomainObject objToUpdate)
        {
            return await _serviceToDalAdapter.UpdateAsync(objToUpdate);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _serviceToDalAdapter.DeleteAsync(id);
        }
    }
}