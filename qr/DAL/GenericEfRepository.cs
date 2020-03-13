using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using qr.Data.EFData;
using qr.Dtos;

namespace qr.DAL
{
    public class GenericEfRepository<TSource, TDestination> : IGenericRepository<TSource, TDestination> 
        where TSource : Dto
        where TDestination : class
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly DbSet<TSource> _table;

        public GenericEfRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _table = _context.Set<TSource>();
        }

        public async Task<IEnumerable<TDestination>> GetAllAsync()
        {
            var foundObjects = await _table.ToListAsync();
            return _mapper.Map<IEnumerable<TDestination>>(foundObjects);
        }

        public async Task<TDestination> GetByIdAsync(object id)
        {
            var foundObject = await _table.FindAsync(id);
            return _mapper.Map<TSource, TDestination>(foundObject);
        }

        public async Task<TDestination> InsertAsync(TDestination obj)
        {
            var objDto = _mapper.Map<TSource>(obj);
            objDto.Id = Guid.NewGuid();
            await _table.AddAsync(_mapper.Map<TSource>(objDto));
            return _mapper.Map<TDestination>(objDto);
        }

        public Task<TDestination> UpdateAsync(TDestination obj)
        {
            var objDto = _mapper.Map<TSource>(obj);
            _table.Attach(objDto);
            _context.Entry(objDto).State = EntityState.Modified;
            return Task.FromResult(obj);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            TSource existing = await _table.FindAsync(id);
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
