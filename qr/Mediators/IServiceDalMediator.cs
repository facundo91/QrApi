using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qrAPI.Mediators
{
    public interface IServiceDalMediator<TDomainObject,TDto>
    {
        Task<IEnumerable<TDomainObject>> GetAllAsync();
        Task<TDomainObject> GetByIdAsync(Guid id);
        Task<TDomainObject> CreateAsync(TDomainObject objToCreate);
        Task<bool> UpdateAsync(TDomainObject objToUpdate);
        Task<bool> DeleteAsync(Guid id);
    }
}