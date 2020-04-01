using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Daos.MongoImplementations;
using qrAPI.DAL.Dtos;
using qrAPI.DAL.Options;

namespace qrAPI.DAL.Data.MongoData
{
    public class MongoContext : IDataContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IOptions<MongoOptions> settings)
        {
            var client = new MongoClient(settings.Value.DefaultConnection);
            _database = client.GetDatabase(settings.Value.Database);
        }
        public IRepository<T> GetRepository<T>() where T : Dto
        {
            return DtosDictionary.TypeDictionary[typeof(T)] switch
            {
                0 => (IRepository<T>)new MongoRepository<QrDto>(_database.GetCollection<QrDto>("qrset")),
                1 => (IRepository<T>)new MongoRepository<PetDto>(_database.GetCollection<PetDto>("petset")),
                _ => throw new InvalidOperationException()
            };
        }

        public IRefreshTokenRepository GetRefreshTokenRepository()
        {
            throw new NotImplementedException();
        }

        public void HealthCheck() => _database.ListCollections();
    }
}