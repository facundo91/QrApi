using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qrAPI.DAL.Dtos;

namespace qrAPI.Logic.Adapters
{
    public interface IServiceAdapter<TDto> where TDto : Dto
    {
        Task<IEnumerable<T>> GetAllAsync<T>();
        Task<T> GetByIdAsync<T>(Guid id);
        Task<T> CreateAsync<T>(T objToCreate);
        Task<bool> UpdateAsync<T>(T objToUpdate);
        Task<bool> DeleteAsync(Guid id);
    }
}