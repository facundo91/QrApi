using System;
using System.IO;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Daos.JsonImplementations;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Data.JsonData
{
    public class JsonContext : IDataContext
    {
        public IRepository<T> GetRepository<T>() where T : Dto
        {
            return DtosDictionary.TypeDictionary[typeof(T)] switch
            {
                0 => (IRepository<T>)new JsonRepository<QrDto>("qrs.json"),
                1 => (IRepository<T>)new JsonRepository<PetDto>("pets.json"),
                _ => throw new InvalidOperationException()
            };
        }

        public IRefreshTokenRepository GetRefreshTokenRepository()
        {
            throw new NotImplementedException();
        }

        public void HealthCheck() => _ = new FileInfo("qrs.json");
    }
}