using qrAPI.DAL.Dtos;
using qrAPI.Logic.Adapters.Interfaces;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services.Interfaces;

namespace qrAPI.Logic.Services.Implementations
{
    public class QrService : AbstractGenericService<Qr, QrDto>, IQrService
    {
        public QrService(IQrServiceAdapter serviceToDalAdapter) : base(serviceToDalAdapter)
        {
        }
    }
}