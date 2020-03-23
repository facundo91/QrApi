using System.Collections.Generic;
using System.Threading.Tasks;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Repositories
{
    public interface IGenericRepository<TDto> where TDto : Dto
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(object id);
        Task<TDto> InsertAsync(TDto obj);
        Task<bool> UpdateAsync(TDto obj);
        Task<bool> DeleteAsync(object id);
    }
}