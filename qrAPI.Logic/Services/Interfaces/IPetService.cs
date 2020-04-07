using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Services.Interfaces
{
    public interface IPetService : IGenericService<Pet>
    {
        new Task<IEnumerable<Pet>> GetAllAsync();
        new Task<Pet> CreateAsync(Pet petToCreate);
        new Task<Pet> GetByIdAsync(Guid id);
        new Task<bool> DeleteAsync(Guid id);
        new Task<bool> UpdateAsync(Pet petToCreate);
    }
}
