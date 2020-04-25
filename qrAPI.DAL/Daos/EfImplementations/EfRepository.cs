using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Data.EFData.Contexts;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Daos.EfImplementations
{
    public class EfRepository<TDto> : IRepository<TDto> where TDto : Dto
    {
        private readonly ApplicationDbContext _context;
        protected DbSet<TDto> Table => _context.Set<TDto>();

        public EfRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        protected async Task<IEnumerable<TDto>> GetAllByQueryExpressionAsync(Expression<Func<TDto, bool>> expression) =>
            await Table.Where(expression).ToListAsync();

        protected async Task<TDto> GetAsyncIncludeProperty<TDto2>(object id, Expression<Func<TDto, TDto2>> navigationPropertyPath) =>
            await Table.Where(dto => dto.Id == (Guid)id)
                .Include(navigationPropertyPath).SingleOrDefaultAsync();

        private async Task<List<TDto>> GetAllAsyncIncludeProperty<TDto2>(Guid id, Expression<Func<TDto, TDto2>> navigationPropertyPath) where TDto2 : Dto =>
            await Table.Where(userPetDto => userPetDto.Id == id).Include(navigationPropertyPath).ToListAsync();

        public virtual async Task<IEnumerable<TDto>> GetAllAsync() => await Table.ToListAsync();

        public virtual async Task<TDto> GetAsync(object id) => await Table.FindAsync(id);

        public virtual async Task<TDto> InsertAsync(TDto obj)
        {
            var objCreated = (await Table.AddAsync(obj)).Entity;
            var created = await Save();
            return created == 0 ? null : objCreated;
        }

        public virtual async Task<bool> UpdateAsync(TDto obj)
        {
            var dto = await GetAsync(obj.Id);
            if (dto == null) return false;
            _context.Entry(dto).CurrentValues.SetValues(obj);
            var updated = await Save();
            return updated > 0;
        }

        public virtual async Task<bool> DeleteAsync(object id)
        {
            var obj = await GetAsync(id);
            if (obj == null) return false;
            Table.Remove(obj);
            var deleted = await Save();
            return deleted > 0;
        }

        private async Task<int> Save() => await _context.SaveChangesAsync();
    }
}