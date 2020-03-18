using System.IO;
using qrAPI.Dtos;
using qrAPI.Repositories;

namespace qrAPI.Data.JsonData
{
    public class JsonContext : IDataContext
    {
        public JsonContext()
        {
            QrRepository = new GenericJsonRepository<QrDto>();
        }

        public IGenericRepository<QrDto> QrRepository { get; }
        public void HealthCheck() => _ = new FileInfo("qrs.10.json");
    }
}