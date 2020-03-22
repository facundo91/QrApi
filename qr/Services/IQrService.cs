using qrAPI.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qrAPI.Services
{
    public interface IQrService
    {
        Task<IEnumerable<Qr>> GetQrsAsync();
        Task<Qr> GetQrByIdAsync(Guid qrId);
        Task<Qr> CreateQrAsync(Qr qrToCreate);
        Task<bool> UpdateQrAsync(Qr qrToUpdate);
        Task<bool> DeleteQrAsync(Guid qrId);
    }
}