using qrAPI.DAL.Dtos;
using qrAPI.DAL.Repositories;

namespace qrAPI.DAL.Data
{
    public interface IDataContext
    {
        public IGenericRepository<QrDto> QrRepository { get; }
        void HealthCheck();
    }
}