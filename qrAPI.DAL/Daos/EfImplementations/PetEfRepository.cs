using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Data.EFData.Contexts;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Daos.EfImplementations
{
    public class PetEfRepository : EfRepository<PetDto>, IPetRepository
    {
        private readonly IUserPetRepository _userPetRepository;

        public PetEfRepository(ApplicationDbContext context, IUserPetRepository userPetRepository) : base(context)
        {
            _userPetRepository = userPetRepository;
        }

        public async Task<IEnumerable<PetDto>> GetAllByNameAsync(string name) =>
            await GetAllByQueryExpressionAsync(pet => pet.Name.StartsWith(name));

        public async Task<IEnumerable<PetDto>> GetAllByUserIdAsync(Guid userId) =>
            (await _userPetRepository.GetAllByUserIdAsync(userId)).Select(userPetDto => userPetDto.Pet);

        public override async Task<PetDto> GetAsync(object id) => await _table.Where(dto => dto.Id == (Guid)id)
        .Include(pet => pet.UserPets).ThenInclude(userPets => userPets.User).SingleOrDefaultAsync();
    }
}