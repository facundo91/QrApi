using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using qr.Dtos;

namespace qr.DAL
{
    public class GenericJsonRepository<TSource, TDestination> : IGenericRepository<TSource, TDestination>
        where TSource : Dto
        where TDestination : class
    {
        private readonly List<TSource> _table = new List<TSource>();
        private readonly IMapper _mapper;

        public GenericJsonRepository(IMapper mapper)
        {
            LoadJson();
            _mapper = mapper;
            _table.ForEach(x => x.Id = Guid.NewGuid());
        }

        private IEnumerable<TDestination> GetAll() =>
            _mapper.Map<IEnumerable<TDestination>>(_table);

        private TDestination GetById(object id) =>
            _mapper.Map<TDestination>(_table.FirstOrDefault(x => x.Id == (Guid)id));

        private TDestination Insert(TDestination obj)
        {
            var objDto = _mapper.Map<TSource>(obj);
            objDto.Id = Guid.NewGuid();
            _table.Add(objDto);
            return _mapper.Map<TDestination>(objDto);
        }

        private TDestination Update(TDestination obj)
        {
            var objDto = _mapper.Map<TSource>(obj);
            var foundObject = _table.FirstOrDefault(x => x.Id == objDto.Id);
            if (foundObject != null)
                foundObject = objDto;
            return _mapper.Map<TDestination>(obj);
        }

        private bool Delete(object id)
        {
            var firstOrDefault = _table.FirstOrDefault(x => x.Id == (Guid)id);
            return _table.Remove(firstOrDefault);
        }

        private void LoadJson()
        {
            using (StreamReader r = new StreamReader("qrs.10.json"))
            {
                string json = r.ReadToEnd();
                List<TSource> items = JsonConvert.DeserializeObject<List<TSource>>(json);
                _table.AddRange(items);
            }
        }

        public Task<IEnumerable<TDestination>> GetAllAsync() =>
            Task.FromResult(GetAll());

        public Task<TDestination> GetByIdAsync(object id) =>
            Task.FromResult(GetById(id));

        public Task<TDestination> InsertAsync(TDestination obj) =>
            Task.FromResult(Insert(obj));

        public Task<TDestination> UpdateAsync(TDestination obj) =>
            Task.FromResult(Update(obj));

        public Task<bool> DeleteAsync(object id) =>
            Task.FromResult(Delete(id));
    }
}
