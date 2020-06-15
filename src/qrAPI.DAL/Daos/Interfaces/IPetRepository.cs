using qrAPI.DAL.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qrAPI.DAL.Daos.Interfaces
{
    public interface IPetRepository : IRepository<PetDto>
    {
        Task<IEnumerable<PetDto>> GetAllByNameAsync(string name);
        Task<IEnumerable<PetDto>> GetAllByUserIdAsync(Guid userId);
    }
}
