using System.IO;
using AutoMapper;
using qr.DAL;
using qr.Domain;
using qr.Dtos;

namespace qr.Data.JsonData
{
    public class JsonContext : IDataContext
    {
        public JsonContext(IMapper mapper)
        {
            qrRepository = new GenericJsonRepository<QrDto, Qr>(mapper);
        }

        public IGenericRepository<QrDto, Qr> qrRepository { get; }
        public void HealthCheck() => _ = new FileInfo("qrs.10.json");
    }
}
