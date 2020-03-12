using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qr.Dtos;
using qr.Options;

namespace qr.MongoData
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoContext(IOptions<MongoOptions> settings)
        {
            var client = new MongoClient(settings.Value.DefaultConnection);
            _database = client.GetDatabase(settings.Value.Database);
        }

        //public MongoContext()
        //{
        //    var client = new MongoClient("mongodb://localhost:27017");
        //    _database = client.GetDatabase("qrdb");
        //}

        public IMongoCollection<QrDto> Qrs() => _database.GetCollection<QrDto>("qrset");
    }
}
