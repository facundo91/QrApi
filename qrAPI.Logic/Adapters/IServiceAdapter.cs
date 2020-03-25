using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qrAPI.Logic.Adapters
{
    public interface IServiceAdapter<TDomainObject,TDto>
    {
        Task<IEnumerable<TDomainObject>> GetAllAsync();
        Task<TDomainObject> GetByIdAsync(Guid id);
        Task<TDomainObject> CreateAsync(TDomainObject objToCreate);
        Task<bool> UpdateAsync(TDomainObject objToUpdate);
        Task<bool> DeleteAsync(Guid id);
    }
}