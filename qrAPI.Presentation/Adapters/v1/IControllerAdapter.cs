using System;
using System.Threading.Tasks;
using qrAPI.Logic.Domain;

namespace qrAPI.Presentation.Adapters.v1
{
    public interface IControllerAdapter<TDomainObject> where TDomainObject : DomainObject
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetByIdAsync<T>(Guid id);
        Task<T> CreateAsync<T>(object objToCreate);
        Task<bool> UpdateAsync(Guid id, object objToUpdate);
        Task<bool> DeleteAsync(Guid id);
    }
}