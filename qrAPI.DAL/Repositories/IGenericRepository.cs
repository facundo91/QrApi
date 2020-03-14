using qrAPI.DAL.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qrAPI.DAL.Repositories
{
    public interface IGenericRepository<TDto> where TDto : Dto
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(object id);
        Task<TDto> InsertAsync(TDto obj);
        Task<TDto> UpdateAsync(TDto obj);
        Task<bool> DeleteAsync(object id);
    }
}