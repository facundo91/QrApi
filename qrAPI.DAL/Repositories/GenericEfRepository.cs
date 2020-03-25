using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using qrAPI.DAL.Data.EFData;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Repositories
{
    public class GenericEfRepository<TDto> : IGenericRepository<TDto>
        where TDto : Dto
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TDto> _table;

        public GenericEfRepository(ApplicationDbContext context, DbSet<TDto> table)
        {
            _context = context;
            _table = table;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var table = await _table.ToListAsync();
            Console.WriteLine("s");
            return table;
            //return await _table.ToListAsync();
        }

        public async Task<TDto> GetByIdAsync(object id)
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
            var dto = await GetByIdAsync(obj.Id);
            if (dto == null) return false;
            _context.Entry(dto).CurrentValues.SetValues(obj);
            var updated = await Save();
            return updated > 0;
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var obj = await GetByIdAsync(id);
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