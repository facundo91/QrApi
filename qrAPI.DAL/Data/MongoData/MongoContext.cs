using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qrAPI.DAL.Dtos;
using qrAPI.DAL.Options;
using qrAPI.DAL.Repositories;

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
        public IGenericRepository<T> GetRepository<T>() where T : Dto
        {
            return DtosDictionary.TypeDictionary[typeof(T)] switch
            {
                0 => (IGenericRepository<T>)new GenericMongoRepository<QrDto>(_database.GetCollection<QrDto>("qrset")),
                1 => (IGenericRepository<T>)new GenericMongoRepository<PetDto>(_database.GetCollection<PetDto>("petset")),
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