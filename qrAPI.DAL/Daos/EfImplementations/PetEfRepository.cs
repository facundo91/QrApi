using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Data.EFData.Contexts;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Daos.EfImplementations
{
    public class PetEfRepository : EfRepository<PetDto>, IPetRepository
    {
        public PetEfRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<PetDto>> GetAllByNameAsync(string name)
        {
            Expression<Func<PetDto, bool>> expression = pet => pet.Name.StartsWith(name);
            return await GetAllByQueryExpressionAsync(expression);
        }

        public async Task<IEnumerable<PetDto>> GetAllByUserIdAsync(Guid userId)
        {
            Expression<Func<PetDto, bool>> expression = pet => pet.OwnerId == userId;
            return await GetAllByQueryExpressionAsync(expression);
        }
    }
}