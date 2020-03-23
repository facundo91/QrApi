using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Repositories
{
    public class GenericJsonRepository<TDto> : IGenericRepository<TDto>
        where TDto : Dto
    {
        private readonly List<TDto> _table = new List<TDto>();

        public GenericJsonRepository(string path)
        {
            LoadJson(path);
            _table.ForEach(x => x.Id = Guid.NewGuid());
        }

        private IEnumerable<TDto> GetAll() => _table;

        private TDto GetById(object id) => _table.FirstOrDefault(x => x.Id == (Guid) id);

        private TDto Insert(TDto obj)
        {
            obj.Id = Guid.NewGuid();
            _table.Add(obj);
            return obj;
        }

        private bool Update(TDto obj)
        {
            var foundObject = _table.FirstOrDefault(x => x.Id == obj.Id);
            if (foundObject == null) return false;
            foundObject = obj;
            return true;
        }

        private bool Delete(object id)
        {
            var firstOrDefault = _table.FirstOrDefault(x => x.Id == (Guid) id);
            return _table.Remove(firstOrDefault);
        }

        private void LoadJson(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                List<TDto> items = JsonConvert.DeserializeObject<List<TDto>>(json);
                _table.AddRange(items);
            }
        }

        public Task<IEnumerable<TDto>> GetAllAsync() =>
            Task.FromResult(GetAll());

        public Task<TDto> GetByIdAsync(object id) =>
            Task.FromResult(GetById(id));

        public Task<TDto> InsertAsync(TDto obj) =>
            Task.FromResult(Insert(obj));

        public Task<bool> UpdateAsync(TDto obj) =>
            Task.FromResult(Update(obj));

        public Task<bool> DeleteAsync(object id) =>
            Task.FromResult(Delete(id));
    }
}