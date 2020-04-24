using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Dtos;
using qrAPI.Infrastructure.Adapters;
using qrAPI.Infrastructure.Extensions;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services.Interfaces;

namespace qrAPI.Logic.Services.Implementations
{
    public class PetService : AbstractGenericService<Pet, PetDto>
    {
        private readonly IPetRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityService _identityService;

        public PetService(
            IMapperAdapter mapperAdapter, 
            IPetRepository repository, 
            IHttpContextAccessor httpContextAccessor, 
            IIdentityService identityService) 
            : base(mapperAdapter, repository)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _identityService = identityService;
        }

        public override async Task<IEnumerable<Pet>> GetAllAsync()
        {
            var userId = _httpContextAccessor.HttpContext.GetUserId();
            return await MapperAdapter.DoMapAsync<IEnumerable<Pet>>(async () => await _repository.GetAllByUserIdAsync(userId));
        }

        public override async Task<Pet> CreateAsync(Pet petToCreate)
        {
            var userId = _httpContextAccessor.HttpContext.GetUserId();
            var owner = await _identityService.GetPersonAsync(userId);
            owner.AddOwnedPet(petToCreate);
            return await base.CreateAsync(petToCreate);
        }

        public override async Task<Pet> GetByIdAsync(Guid id)
        {
            var pet = await base.GetByIdAsync(id);
            if (pet == null) return null;
            var userId = _httpContextAccessor.HttpContext.GetUserId();
            return pet.Owners.Any(owner => owner.Id == userId) ? pet : null;
        }

        public override async Task<bool> DeleteAsync(Guid id)
        {
            var pet = await GetByIdAsync(id);
            return pet != null && await base.DeleteAsync(id);
        }

        public override async Task<bool> UpdateAsync(Guid id, Pet petToCreate)
        {
            var pet = await GetByIdAsync(id);
            return pet != null && await base.UpdateAsync(id, petToCreate);
        }
    }
}