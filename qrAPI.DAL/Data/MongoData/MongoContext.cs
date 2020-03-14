using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qrAPI.DAL.Dtos;
using qrAPI.DAL.Repositories;
using qrAPI.Options;

namespace qrAPI.DAL.Data.MongoData
{
    public class MongoContext : IDataContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IOptions<MongoOptions> settings, IMapper mapper)
        {
            var client = new MongoClient(settings.Value.DefaultConnection);
            _database = client.GetDatabase(settings.Value.Database);
            QrRepository = new GenericMongoRepository<QrDto>(_database.GetCollection<QrDto>("qrset"));
        }

        //public MongoContext()
        //{
        //    var client = new MongoClient("mongodb://localhost:27017");
        //    _database = client.GetDatabase("qrdb");
        //}

        public IGenericRepository<QrDto> QrRepository { get; }
        public void HealthCheck() => _database.ListCollections();
    }
}