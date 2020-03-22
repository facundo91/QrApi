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
            QrRepository = new GenericMongoRepository<QrDto>(_database.GetCollection<QrDto>("qrset"));
            PetRepository = new GenericMongoRepository<PetDto>(_database.GetCollection<PetDto>("petset"));
        }

        public IGenericRepository<QrDto> QrRepository { get; }
        public IGenericRepository<PetDto> PetRepository { get; }
        public void HealthCheck() => _database.ListCollections();
    }
}