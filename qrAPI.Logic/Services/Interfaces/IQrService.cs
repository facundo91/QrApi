using System;
using System.Threading.Tasks;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Services.Interfaces
{
    public interface IQrService : IGenericService<Qr>
    {
        Task ScanQr(Guid qrId);
    }
}