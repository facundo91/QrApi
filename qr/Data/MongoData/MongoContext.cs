using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qrAPI.Dtos;
using qrAPI.Options;
using qrAPI.Repositories;

namespace qrAPI.Data.MongoData
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
        public void HealthCheck() => _database.ListCollections();
    }
}