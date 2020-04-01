using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Daos.CacheImplementations
{
    public class CacheRepository<T> : IRepository<T> where T : Dto
    {
        private readonly IRepository<T> _repository;

        public CacheRepository(IRepository<T> repository)
        {
            _repository = repository;
        }
        public Task<IEnumerable<T>> GetAllAsync()
        {
            //generate key
            //try to get from cache and return
            //if not found, try to get from repository
            //if found cache it
            //return
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(object id)
        {
            //generate key
            //try to get from cache and return
            //if not found, try to get from repository
            //if found cache it
            //return
            throw new NotImplementedException();
        }

        public Task<T> InsertAsync(T obj)
        {
            return _repository.InsertAsync(obj);
        }

        public Task<bool> UpdateAsync(T obj)
        {
            return _repository.UpdateAsync(obj);
        }

        public Task<bool> DeleteAsync(object id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}
