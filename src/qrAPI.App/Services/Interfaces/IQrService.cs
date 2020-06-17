using qrAPI.App.Domain;
using System;
using System.Threading.Tasks;

namespace qrAPI.App.Services.Interfaces
{
    public interface IQrService : IGenericService<Qr>
    {
        /// <summary>
        /// Scans a code.
        /// </summary>
        /// <param name="qrId">Code Id</param>
        Task ScanQr(Guid qrId);
    }
}