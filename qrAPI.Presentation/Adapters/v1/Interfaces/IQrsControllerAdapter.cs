using System;
using System.Threading.Tasks;
using qrAPI.Logic.Domain;

namespace qrAPI.Presentation.Adapters.v1.Interfaces
{
    public interface IQrsControllerAdapter : IControllerAdapter<Qr>
    {
        Task ScanQr(Guid qrId);
    }
}
