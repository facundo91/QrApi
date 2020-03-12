using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using qr.Domain;
using qr.Dtos;
using qr.EFData;

namespace qr.Services
{
    public class QrService : IQrService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public QrService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _dbContext = applicationDbContext;
            _mapper = mapper;
        }
        public async Task<List<Qr>> GetQrsAsync()
        {
            var qrDtoList = await _dbContext.Qrs.ToListAsync();
            return _mapper.Map<List<Qr>>(qrDtoList);
        }

        public async Task<Qr> GetQrByIdAsync(Guid qrId)
        {
            var qrDto = await _dbContext.Qrs.SingleOrDefaultAsync(x => x.Id == qrId);
            return _mapper.Map<Qr>(qrDto);
        }

        public async Task<Qr> CreateQrAsync(Qr qrToCreate)
        {
            qrToCreate.Id = Guid.NewGuid();
            var qrDto = _mapper.Map<QrDto>(qrToCreate);
            _dbContext.Qrs.Add(qrDto);
            var created = await _dbContext.SaveChangesAsync();
            return created > 0 ? _mapper.Map<Qr>(qrDto) : null;
        }

        public async Task<bool> UpdateQrAsync(Qr qrToUpdate)
        {
            _dbContext.Qrs.Update(_mapper.Map<QrDto>(qrToUpdate));
            var updated = await _dbContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteQrAsync(Guid qrId)
        {
            var qrDto = await _dbContext.Qrs.SingleOrDefaultAsync(x => x.Id == qrId);
            if (qrDto == null) return false;
            _dbContext.Qrs.Remove(qrDto);
            var deleted = await _dbContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}