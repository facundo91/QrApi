using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qr.DAL;
using qr.Domain;
using qr.Dtos;
using qr.Options;

namespace qr.Data.MongoData
{
    public class MongoContext : IDataContext
    {
        private readonly IMongoDatabase _database;
        public MongoContext(IOptions<MongoOptions> settings, IMapper mapper)
        {
            var client = new MongoClient(settings.Value.DefaultConnection);
            _database = client.GetDatabase(settings.Value.Database);
            qrRepository = new GenericMongoRepository<QrDto, Qr>(_database.GetCollection<QrDto>("qrset"), mapper);
        }

        //public MongoContext()
        //{
        //    var client = new MongoClient("mongodb://localhost:27017");
        //    _database = client.GetDatabase("qrdb");
        //}

        public IGenericRepository<QrDto, Qr> qrRepository { get; }
        public void HealthCheck() => _database.ListCollections();
    }
}
