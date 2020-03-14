using AutoMapper;
using qrAPI.DAL.Dtos;
using qrAPI.DAL.Repositories;
using System.IO;

namespace qrAPI.DAL.Data.JsonData
{
    public class JsonContext : IDataContext
    {
        public JsonContext(IMapper mapper)
        {
            QrRepository = new GenericJsonRepository<QrDto>();
        }

        public IGenericRepository<QrDto> QrRepository { get; }
        public void HealthCheck() => _ = new FileInfo("qrs.10.json");
    }
}