using MongoDB.Driver;
using qrAPI.DAL.Options;

namespace qrAPI.DAL.Data.MongoData
{
    public class MongoContext : IDataContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(MongoOptions settings)
        {
            var client = new MongoClient(settings.DefaultConnection);
            _database = client.GetDatabase(settings.Database);
        }

        public void HealthCheck() => _database.ListCollections();
    }
}