using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using qrAPI.Contracts.v1;
using qrAPI.Contracts.v1.Requests;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Domain;
using qrAPI.Mediators;
using qrAPI.Options;

namespace qrAPI.Controllers.v1
{
    public class PetsController : Controller
    {
        private readonly IControllerServiceMediator<Pet> _controllerServiceMediator;

        public PetsController(IControllerServiceMediator<Pet> controllerServiceMediator)
        {
            _controllerServiceMediator = controllerServiceMediator;
        }

        [HttpGet(ApiRoutes.Pets.GetAll)]
        public async Task<IActionResult> GetAllPets()
        {
            var result = await _controllerServiceMediator.GetAllAsync<IEnumerable<PetResponse>>();
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Pets.Get)]
        public async Task<IActionResult> GetPet([FromRoute] Guid petId)
        {
            var result = await _controllerServiceMediator.GetByIdAsync<PetResponse>(petId);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        [HttpPost(ApiRoutes.Pets.Create)]
        public async Task<IActionResult> CreatePet([FromBody] CreatePetRequest petRequest)
        {
            var result = await _controllerServiceMediator.CreateAsync<PetResponse>(petRequest);
            return result != null
                ? CreatedAtAction("CreatePet", new { id = result.Id }, result)
                : (IActionResult)BadRequest();
        }

        [HttpPut(ApiRoutes.Pets.Update)]
        [FeatureGate(FeatureFlags.EndpointFlag)]
        public async Task<IActionResult> UpdatePet([FromRoute] Guid petId, [FromBody] UpdatePetRequest petRequest)
        {
            var result = await _controllerServiceMediator.UpdateAsync(petRequest);
            return result ? Ok() : (IActionResult)NotFound();
        }

        [HttpDelete(ApiRoutes.Pets.Delete)]
        public async Task<IActionResult> DeletePet([FromRoute] Guid petId)
        {
            var result = await _controllerServiceMediator.DeleteAsync(petId);
            return result ? (IActionResult)NoContent() : NotFound();
        }
    }
}
