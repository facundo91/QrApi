using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using qr.Domain;
using qr.Dtos;
using qr.MongoData;

namespace qr.Services
{
    public class MongoQrService : IQrService
    {
        private readonly IMongoContext _mongoContext;
        private readonly IMapper _mapper;

        public MongoQrService(IMongoContext mongoContext, IMapper mapper)
        {
            _mongoContext = mongoContext;
            _mapper = mapper;
        }

        public async Task<List<Qr>> GetQrsAsync()
        {
            var qrs = await _mongoContext.Qrs().Find(_ => true).ToListAsync();
            return _mapper.Map<List<Qr>>(qrs);
        }

        public async Task<Qr> GetQrByIdAsync(Guid qrId)
        {
            var qrDto = await _mongoContext.Qrs()
                .Find(Qr => Qr.Id == qrId)
                .FirstOrDefaultAsync();
            return _mapper.Map<Qr>(qrDto);
        }

        public async Task<Qr> CreateQrAsync(Qr qrToCreate)
        {
            var qrDto = _mapper.Map<QrDto>(qrToCreate);
            qrDto.Id = Guid.NewGuid();
            await _mongoContext.Qrs().InsertOneAsync(qrDto);
            return _mapper.Map<Qr>(qrDto);
        }

        public async Task<bool> UpdateQrAsync(Qr qrToUpdate)
        {
            var qrDto = _mapper.Map<QrDto>(qrToUpdate);
            var update = Builders<QrDto>.Update.Set(s => s, qrDto);
            var filter = Builders<QrDto>.Filter.Eq(x => x.Id, qrDto.Id);
            UpdateResult actionResult
                = await _mongoContext.Qrs().UpdateOneAsync(filter, update);
            return actionResult.IsAcknowledged
                   && actionResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteQrAsync(Guid qrId)
        {
            var deleteResult = await _mongoContext.Qrs().DeleteOneAsync(x => x.Id == qrId);
            return deleteResult.DeletedCount > 0 && deleteResult.IsAcknowledged;
        }
    }
}
