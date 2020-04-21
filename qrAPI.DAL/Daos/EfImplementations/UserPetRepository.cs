﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Data.EFData.Contexts;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Daos.EfImplementations
{
    public class UserPetRepository : EfRepository<UserPetDto>, IUserPetRepository
    {
        public UserPetRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UserPetDto>> GetAllByUserIdAsync(Guid userId) =>
            await _table.Where(userPetDto => userPetDto.UserId == userId).Include(userPet => userPet.Pet).ToListAsync();

        public async Task<IEnumerable<UserPetDto>> GetAllByPetIdAsync(Guid petId) => 
            await _table.Where(userPetDto => userPetDto.UserId == petId).Include(userPet => userPet.User).ToListAsync();
    }
}