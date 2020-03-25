using qrAPI.DAL.Dtos;
using qrAPI.Logic.Adapters;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Services
{
    public class QrService : AbstractGenericService<Qr, QrDto>, IQrService
    {
        public QrService(IServiceAdapter<QrDto> serviceToDalAdapter) : base(serviceToDalAdapter)
        {
        }
    }
}