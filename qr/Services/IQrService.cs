using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qr.Domain;

namespace qr.Services
{
    public interface IQrService
    {
        Task<List<Qr>> GetQrsAsync();
        Task<Qr> GetQrByIdAsync(Guid qrId);
        Task<Qr> CreateQrAsync(Qr qrToCreate);
        Task<bool> UpdateQrAsync(Qr qrToUpdate);
        Task<bool> DeleteQrAsync(Guid qrId);

    }
}