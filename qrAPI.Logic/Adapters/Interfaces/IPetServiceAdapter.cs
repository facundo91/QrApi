using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qrAPI.DAL.Dtos;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Adapters.Interfaces
{
    public interface IPetServiceAdapter : IServiceAdapter<PetDto>
    {
        Task<IEnumerable<Pet>> GetAllByUserAsync(Guid userId);
    }
}
