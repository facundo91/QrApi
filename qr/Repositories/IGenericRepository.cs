using System.Collections.Generic;
using System.Threading.Tasks;
using qrAPI.Dtos;

namespace qrAPI.Repositories
{
    public interface IGenericRepository<TDto> where TDto : Dto
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(object id);
        Task<bool> InsertAsync(TDto obj);
        Task<bool> UpdateAsync(TDto obj);
        Task<bool> DeleteAsync(object id);
    }
}