using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services.Interfaces;
using qrAPI.Presentation.Adapters.v1.Interfaces;

namespace qrAPI.Presentation.Adapters.v1.Implementations
{
    public class PetsControllerAdapter : ControllerAdapter<Pet>,  IPetsControllerAdapter
    {
        private new readonly IPetService _service;

        public PetsControllerAdapter(IMapper mapper, IPetService service) : base(mapper, service)
        {
            _service = service;
        }

        public async Task<IEnumerable<PetResponse>> GetAllFromCurrentUserAsync()
        {
            var pets = await _service.GetAllFromCurrentUserAsync();
            return _mapper.Map<IEnumerable<PetResponse>>(pets);
        }

        public async Task<PetResponse> CreateForCurrentUserAsync(CreatePetRequest petToCreate)
        {
            var pet = _mapper.Map<Pet>(petToCreate);
            var petCreated = await _service.CreatePetForCurrentUserAsync(pet);
            return _mapper.Map<PetResponse>(petCreated);
        }
    }
}
