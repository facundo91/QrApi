using Microsoft.EntityFrameworkCore;
using qrAPI.DAL.Data.EFData;
using qrAPI.DAL.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qrAPI.DAL.Repositories
{
    public class GenericEfRepository<TDto> : IGenericRepository<TDto>
        where TDto : Dto
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TDto> _table;

        public GenericEfRepository(ApplicationDbContext context)
        {
            _context = context;
            _table = _context.Set<TDto>();
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<TDto> GetByIdAsync(object id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<TDto> InsertAsync(TDto obj)
        {
            obj.Id = Guid.NewGuid();
            await _table.AddAsync(obj);
            return obj;
        }

        public Task<TDto> UpdateAsync(TDto obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            return Task.FromResult(obj);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            TDto existing = await _table.FindAsync(id);
            if (existing == null) return false;
            _table.Remove(existing);
            return true;
        }

        private void Save()
        {
            _context.SaveChanges();
        }
    }
}