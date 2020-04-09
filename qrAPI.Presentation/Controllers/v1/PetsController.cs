using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using qrAPI.Contracts.v1;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Contracts.v1.Requests.Update;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Infrastructure.Options;
using qrAPI.Presentation.Adapters.v1.Interfaces;
using static Microsoft.AspNetCore.Http.StatusCodes;


namespace qrAPI.Presentation.Controllers.v1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [ODataRoutePrefix("Pets")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PetsController : ODataController
    {
        private readonly IPetsControllerAdapter _controllerAdapter;

        public PetsController(IPetsControllerAdapter controllerAdapter)
        {
            _controllerAdapter = controllerAdapter;
        }

        /// <summary>
        /// Returns all the pets in the system
        /// </summary>
        /// <response code="200">Returns all the pets in the system</response>
        [ODataRoute]
        [ProducesResponseType(typeof(IEnumerable<PetResponse>), Status200OK)]
        [HttpGet(ApiRoutes.Pets.GetAll)]
        [EnableQuery]
        [ResponseCache(VaryByQueryKeys = new[] { "*" }, Duration = 30)]
        //[Cached(30)]
        public async Task<IActionResult> GetAllPets()
        {
            var result = await _controllerAdapter.GetAllAsync<IEnumerable<PetResponse>>();
            return Ok(result);
        }

        [ProducesResponseType(typeof(PetResponse), Status200OK)]
        [HttpGet(ApiRoutes.Pets.Get)]
        //[Cached(600)]
        [ODataRoute("{petId}")]
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
        [ProducesResponseType(typeof(PetResponse), Status201Created)]
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
            return result ? NoContent() : (IActionResult)NotFound();
        }

        [HttpDelete(ApiRoutes.Pets.Delete)]
        public async Task<IActionResult> DeletePet([FromRoute] Guid petId)
        {
            var result = await _controllerAdapter.DeleteAsync(petId);
            return result ? (IActionResult)NoContent() : NotFound();
        }
    }
}
