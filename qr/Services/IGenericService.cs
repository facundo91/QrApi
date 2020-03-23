using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qrAPI.Domain;

namespace qrAPI.Services
{
    public interface IGenericService<TDomainObject> where TDomainObject : DomainObject
    {
        Task<IEnumerable<TDomainObject>> GetAllAsync();
        Task<TDomainObject> GetByIdAsync(Guid id);
        Task<TDomainObject> CreateAsync(TDomainObject obj);
        Task<bool> UpdateAsync(TDomainObject obj);
        Task<bool> DeleteAsync(Guid id);
    }
}
