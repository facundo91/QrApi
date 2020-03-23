using System;
using System.IO;
using qrAPI.DAL.Dtos;
using qrAPI.DAL.Repositories;

namespace qrAPI.DAL.Data.JsonData
{
    public class JsonContext : IDataContext
    {
        public IGenericRepository<T> GetRepository<T>() where T : Dto
        {
            return DtosDictionary.TypeDictionary[typeof(T)] switch
            {
                0 => (IGenericRepository<T>)new GenericJsonRepository<QrDto>("qrs.json"),
                1 => (IGenericRepository<T>)new GenericJsonRepository<PetDto>("pets.json"),
                2 => (IGenericRepository<T>)new GenericJsonRepository<PetDto>("pets.json"),
                _ => throw new InvalidOperationException()
            };
        }
        public void HealthCheck() => _ = new FileInfo("qrs.json");
    }
}