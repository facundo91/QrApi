using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Daos.MongoImplementations
{
    public class PetMongoRepository : MongoRepository<PetDto>, IPetRepository
    {
        public PetMongoRepository(IMongoCollection<PetDto> table) : base(table)
        {
        }

        public Task<IEnumerable<PetDto>> GetAllByNameAsync(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<PetDto>> GetAllByUserIdAsync(Guid userId)
        {
            throw new System.NotImplementedException();
        }
    }
}