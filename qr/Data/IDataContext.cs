using qr.DAL;
using qr.Domain;
using qr.Dtos;

namespace qr.Data
{
    public interface IDataContext
    {
        public IGenericRepository<QrDto, Qr> qrRepository { get; }
        void HealthCheck();
    }
}