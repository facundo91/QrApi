using qrAPI.Dtos;
using qrAPI.Repositories;

namespace qrAPI.Data
{
    public interface IDataContext
    {
        public IGenericRepository<QrDto> QrRepository { get; }
        public IGenericRepository<PetDto> PetRepository { get; }
        void HealthCheck();
    }
}