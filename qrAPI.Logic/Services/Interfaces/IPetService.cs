using System.Collections.Generic;
using System.Threading.Tasks;
using qrAPI.Logic.Domain;

namespace qrAPI.Logic.Services.Interfaces
{
    public interface IPetService : IGenericService<Pet>
    {
        Task<IEnumerable<Pet>> GetAllFromCurrentUserAsync();
        Task<Pet> CreatePetForCurrentUserAsync(Pet petToCreate);
    }
}
