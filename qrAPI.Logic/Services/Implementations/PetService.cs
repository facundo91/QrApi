using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using qrAPI.DAL.Dtos;
using qrAPI.Infrastructure.Extensions;
using qrAPI.Logic.Adapters.Interfaces;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services.Interfaces;

namespace qrAPI.Logic.Services.Implementations
{
    public class PetService : AbstractGenericService<Pet, PetDto>, IPetService 
    {
        private readonly IPetServiceAdapter _serviceToDalAdapter;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityService _identityService;

        public PetService(IPetServiceAdapter serviceToDalAdapter, IHttpContextAccessor httpContextAccessor, IIdentityService identityService) : base(serviceToDalAdapter)
        {
            _serviceToDalAdapter = serviceToDalAdapter;
            _httpContextAccessor = httpContextAccessor;
            _identityService = identityService;
        }

        public async Task<IEnumerable<Pet>> GetAllFromCurrentUserAsync()
        {
            var userId = _httpContextAccessor.HttpContext.GetUserId();
            return await _serviceToDalAdapter.GetAllByUserAsync(userId);
        }

        public async Task<Pet> CreatePetForCurrentUserAsync(Pet petToCreate)
        {
            var userId = _httpContextAccessor.HttpContext.GetUserId();
            var owner = await _identityService.GetPersonAsync(userId);
            owner.AddOwnedPet(petToCreate);
            return await CreateAsync(petToCreate);
        }
    }
}