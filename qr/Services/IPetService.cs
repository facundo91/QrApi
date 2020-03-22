using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qrAPI.Domain;

namespace qrAPI.Services
{
    public interface IPetService
    {
        Task<IEnumerable<Pet>> GetPetsAsync();
        Task<Pet> GetPetByIdAsync(Guid petId);
        Task<Pet> CreatePetAsync(Pet petToCreate);
        Task<bool> UpdatePetAsync(Pet petToUpdate);
        Task<bool> DeletePetAsync(Guid petId);
    }
}
