using System.Collections.Generic;
using System.Threading.Tasks;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Logic.Domain;

namespace qrAPI.Presentation.Adapters.v1.Interfaces
{
    public interface IPetsControllerAdapter : IControllerAdapter<Pet>
    {
        Task<IEnumerable<PetResponse>> GetAllFromCurrentUserAsync();
        Task<PetResponse> CreateForCurrentUserAsync(CreatePetRequest petToCreate);
    }
}