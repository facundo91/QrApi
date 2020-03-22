using System.IO;
using qrAPI.Dtos;
using qrAPI.Repositories;

namespace qrAPI.Data.JsonData
{
    public class JsonContext : IDataContext
    {
        public JsonContext()
        {
            QrRepository = new GenericJsonRepository<QrDto>("qrs.json");
            PetRepository = new GenericJsonRepository<PetDto>("pets.json");
        }

        public IGenericRepository<QrDto> QrRepository { get; }
        public IGenericRepository<PetDto> PetRepository { get; }
        public void HealthCheck() => _ = new FileInfo("qrs.json");
    }
}