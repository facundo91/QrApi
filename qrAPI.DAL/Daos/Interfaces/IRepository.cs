using System.Collections.Generic;
using System.Threading.Tasks;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Daos.Interfaces
{
    public interface IRepository<TDto> where TDto : Dto
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetAsync(object id);
        Task<TDto> InsertAsync(TDto obj);
        Task<bool> UpdateAsync(TDto obj);
        Task<bool> DeleteAsync(object id);
    }
}