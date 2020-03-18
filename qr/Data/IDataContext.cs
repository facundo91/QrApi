using qrAPI.Dtos;
using qrAPI.Repositories;

namespace qrAPI.Data
{
    public interface IDataContext
    {
        public IGenericRepository<QrDto> QrRepository { get; }
        void HealthCheck();
    }
}