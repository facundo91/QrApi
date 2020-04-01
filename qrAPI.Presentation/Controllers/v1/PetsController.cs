using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using qrAPI.Contracts.v1;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Contracts.v1.Requests.Update;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Infrastructure.Options;
using qrAPI.Logic.Domain;
using qrAPI.Presentation.Cache;
using qrAPI.Infrastructure.Constants;
using qrAPI.Presentation.Adapters.v1;

namespace qrAPI.Presentation.Controllers.v1
{
    public class PetsController : ControllerBase
    {
        private readonly IControllerAdapter<Pet> _controllerAdapter;

        public PetsController(IControllerAdapter<Pet> controllerAdapter)
        {
            _controllerAdapter = controllerAdapter;
        }

        /// <summary>
        /// Returns all the pets in the system
        /// </summary>
        /// <response code="200">Returns all the pets in the system</response>
        [HttpGet(ApiRoutes.Pets.GetAll)]
        [Cached(30)]
        public async Task<IActionResult> GetAllPets()
        {
            var result = await _controllerAdapter.GetAllAsync<IEnumerable<PetResponse>>();
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Pets.Get)]
        [Cached(600)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetPet([FromRoute] Guid petId)
        {
            var result = await _controllerAdapter.GetByIdAsync<PetResponse>(petId);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        /// <summary>
        /// Creates a pet in the system
        /// </summary>
        /// <response code="201">Creates a pet in the system</response>
        /// <response code="400">Unable to create the pet due to validation error</response>
        [HttpPost(ApiRoutes.Pets.Create)]
        public async Task<IActionResult> CreatePet([FromBody] CreatePetRequest petRequest)
        {
            var result = await _controllerAdapter.CreateAsync<PetResponse>(petRequest);
            return result != null
                ? CreatedAtAction("CreatePet", new { id = result.Id }, result)
                : (IActionResult)BadRequest();
        }

        [HttpPut(ApiRoutes.Pets.Update)]
        [FeatureGate(FeatureFlags.EndpointFlag)]
        public async Task<IActionResult> UpdatePet([FromRoute] Guid petId, [FromBody] UpdatePetRequest petRequest)
        {
            var result = await _controllerAdapter.UpdateAsync(petId, petRequest);
            return result ? Ok() : (IActionResult)NotFound();
        }

        [HttpDelete(ApiRoutes.Pets.Delete)]
        [Authorize(Policy = "MustWorkForQRightThing")]
        public async Task<IActionResult> DeletePet([FromRoute] Guid petId)
        {
            var result = await _controllerAdapter.DeleteAsync(petId);
            return result ? (IActionResult)NoContent() : NotFound();
        }
    }
}
