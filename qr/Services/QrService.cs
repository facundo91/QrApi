using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qr.DAL;
using qr.Data;
using qr.Domain;
using qr.Dtos;

namespace qr.Services
{
    public class QrService : IQrService
    {
        private readonly IGenericRepository<QrDto, Qr> _qrRepository;

        public QrService(IDataContext dataContext)
        {
            _qrRepository = dataContext.qrRepository;
        }

        public Task<IEnumerable<Qr>> GetQrsAsync() => 
            _qrRepository.GetAllAsync();

        public Task<Qr> GetQrByIdAsync(Guid qrId) =>
            _qrRepository.GetByIdAsync(qrId);

        public Task<Qr> CreateQrAsync(Qr qrToCreate) =>
            _qrRepository.InsertAsync(qrToCreate);

        public Task<Qr> UpdateQrAsync(Qr qrToUpdate) =>
            _qrRepository.UpdateAsync(qrToUpdate);

        public Task<bool> DeleteQrAsync(Guid qrId) =>
            _qrRepository.DeleteAsync(qrId);
    }
}