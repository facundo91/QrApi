using MongoDB.Driver;
using qr.Dtos;

namespace qr.MongoData
{
    public interface IMongoContext
    {
        IMongoCollection<QrDto> Qrs();
    }
}