using System;
using System.Threading.Tasks;
using qrAPI.Logic.Domain;

namespace qrAPI.Mediators
{
    public interface IControllerServiceMediator<TDomainObject> where TDomainObject : DomainObject
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetByIdAsync<T>(Guid id);
        Task<T> CreateAsync<T>(object objToCreate);
        Task<bool> UpdateAsync(object objToUpdate);
        Task<bool> DeleteAsync(Guid id);
    }
}