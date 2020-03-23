using qrAPI.DAL.Dtos;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Mediators;

namespace qrAPI.Logic.Services
{
    public class QrService : AbstractGenericService<Qr, QrDto>, IQrService<Qr>
    {
        public QrService(IServiceDalMediator<Qr, QrDto> serviceDalMediator) : base(serviceDalMediator)
        {
        }
    }
}