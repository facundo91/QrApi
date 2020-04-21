using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qrAPI.DAL.Dtos;

namespace qrAPI.DAL.Daos.Interfaces
{
    public interface IUserPetRepository : IRepository<UserPetDto>
    {
        Task<IEnumerable<UserPetDto>> GetAllByUserIdAsync(Guid userId);
        Task<IEnumerable<UserPetDto>> GetAllByPetIdAsync(Guid petId);
    }
}
