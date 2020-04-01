using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Data.EFData;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Daos.EfImplementations
{
    public class EfRepository<TDto> : IRepository<TDto> where TDto : Dto
    {
        private readonly ApplicationDbContext _context;
        protected readonly DbSet<TDto> _table;

        public EfRepository(ApplicationDbContext context)
        {
            _context = context;
            _table = context.Set<TDto>();
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<TDto> GetAsync(object id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<TDto> InsertAsync(TDto obj)
        {
            var objCreated = (await _table.AddAsync(obj)).Entity;
            var created = await Save();
            return created == 0 ? null : objCreated;
        }

        public async Task<bool> UpdateAsync(TDto obj)
        {
            var dto = await GetAsync(obj.Id);
            if (dto == null) return false;
            _context.Entry(dto).CurrentValues.SetValues(obj);
            var updated = await Save();
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var obj = await GetAsync(id);
            if (obj == null) return false;
            _table.Remove(obj);
            var deleted = await Save();
            return deleted > 0;
        }

        private async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}