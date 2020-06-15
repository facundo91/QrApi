using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Dtos;
using qrAPI.DAL.Options;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace qrAPI.DAL.Daos.MongoImplementations
{
    public class PetMongoRepository : MongoRepository<PetDto>, IPetRepository
    {
        public PetMongoRepository(MongoOptions settings) : base(settings)
        {
        }

        public async Task<IEnumerable<PetDto>> GetAllByNameAsync(string name)
        {
            Expression<Func<PetDto, bool>> expression = pet => pet.Name.StartsWith(name);
            return await GetAllByQueryExpressionAsync(expression);
        }

        public async Task<IEnumerable<PetDto>> GetAllByUserIdAsync(Guid userId)
        {
            return await Task.Run(() => new List<PetDto>());
            //Expression<Func<PetDto, bool>> expression = pet => pet.OwnerId == userId;
            //return await GetAllByQueryExpressionAsync(expression);
        }


    }
}