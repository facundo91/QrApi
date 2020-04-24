using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Services.Interfaces
{
    public interface IGenericService<TDomainObject> where TDomainObject : DomainObject
    {
        Task<IEnumerable<TDomainObject>> GetAllAsync();
        Task<TDomainObject> GetByIdAsync(Guid id);
        Task<TDomainObject> CreateAsync(TDomainObject obj);
        Task<bool> UpdateAsync(Guid id, TDomainObject objToUpdate);
        Task<bool> DeleteAsync(Guid id);
    }
}
