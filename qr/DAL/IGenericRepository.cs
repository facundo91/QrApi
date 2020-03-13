using System.Collections.Generic;
using System.Threading.Tasks;
using qr.Dtos;

namespace qr.DAL
{
    public interface IGenericRepository<TSource, TDestination>
        where TSource : Dto
        where TDestination : class
    {
        Task<IEnumerable<TDestination>> GetAllAsync();
        Task<TDestination> GetByIdAsync(object id);
        Task<TDestination> InsertAsync(TDestination obj);
        Task<TDestination> UpdateAsync(TDestination obj);
        Task<bool> DeleteAsync(object id);
    }
}
