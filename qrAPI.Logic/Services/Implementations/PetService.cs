using System;
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

        public new async Task<IEnumerable<Pet>> GetAllAsync()
        {
            var userId = _httpContextAccessor.HttpContext.GetUserId();
            return await _serviceToDalAdapter.GetAllByUserAsync(userId);
        }

        public new async Task<Pet> CreateAsync(Pet petToCreate)
        {
            var userId = _httpContextAccessor.HttpContext.GetUserId();
            var owner = await _identityService.GetPersonAsync(userId);
            owner.AddOwnedPet(petToCreate);
            return await base.CreateAsync(petToCreate);
        }

        public new async Task<Pet> GetByIdAsync(Guid id)
        {
            var pet = await base.GetByIdAsync(id);
            var userId = _httpContextAccessor.HttpContext.GetUserId();
            return pet?.Owner?.Id == userId ? pet : null;
        }

        public new async Task<bool> DeleteAsync(Guid id)
        {
            var pet = await GetByIdAsync(id);
            return pet != null && await base.DeleteAsync(id);
        }

        public new async Task<bool> UpdateAsync(Pet petToCreate)
        {
            var pet = await GetByIdAsync(petToCreate.Id);
            return pet != null && await base.UpdateAsync(petToCreate);
        }
    }
}