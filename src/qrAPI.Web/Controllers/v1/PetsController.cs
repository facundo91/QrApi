﻿using AutoMapper;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using qrAPI.App.Domain;
using qrAPI.App.Services.Interfaces;
using qrAPI.Contracts.v1;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Contracts.v1.Requests.Update;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Infra.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static qrAPI.Contracts.ApiVersions;

namespace qrAPI.Web.Controllers.v1
{
    [Produces("application/json")]
    [ApiVersion(V1Tag)]
    [ODataRoutePrefix("Pets")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PetsController : ODataController
    {
        private readonly IMapper _mapper;
        private readonly IGenericService<Pet> _petService;

        public PetsController(IMapper mapper, IGenericService<Pet> petService)
        {
            _mapper = mapper;
            _petService = petService;
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
        public async Task<IActionResult> GetAllPets()
        {
            var result = _mapper.Map<IEnumerable<PetResponse>>(await _petService.GetAllAsync());
            return Ok(result);
        }

        /// <summary>
        /// Get the Pet with the specific Id
        /// </summary>
        /// <param name="petId">Id of the Pet</param>
        /// <returns>The pet if found</returns>
        /// <response code="200">Pet found and retrieved</response>
        /// <response code="404">Pet not found</response>
        [ProducesResponseType(typeof(PetResponse), Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        [HttpGet(ApiRoutes.Pets.Get)]
        [EnableQuery]
        [ResponseCache(VaryByQueryKeys = new[] { "*" }, Duration = 30)]
        [ODataRoute("{petId}")]
        public async Task<IActionResult> GetPet([FromRoute] Guid petId)
        {
            var result = _mapper.Map<PetResponse>(await _petService.GetByIdAsync(petId));
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        /// <summary>
        /// Creates a pet in the system
        /// </summary>
        /// <response code="201">Creates a pet in the system</response>
        /// <response code="400">Unable to create the pet due to validation error</response>
        [ProducesResponseType(typeof(PetResponse), Status201Created)]
        [ProducesResponseType(Status400BadRequest)]
        [HttpPost(ApiRoutes.Pets.Create)]
        public async Task<IActionResult> CreatePet([FromBody] CreatePetRequest petRequest)
        {
            var result = _mapper.Map<PetResponse>(await _petService.CreateAsync(_mapper.Map<Pet>(petRequest)));
            return result != null
                ? CreatedAtAction("CreatePet", new { id = result.Id }, result)
                : (IActionResult)BadRequest();
        }

        /// <summary>
        /// Updates a pet in the system
        /// </summary>
        /// <response code="204">Pet successfully updated</response>
        /// <response code="404">Either Pet wasn't find, or you don't have access to updated it</response>
        [HttpPut(ApiRoutes.Pets.Update)]
        [ProducesResponseType(Status404NotFound)]
        [FeatureGate(FeatureFlags.EndpointFlag)]
        public async Task<IActionResult> UpdatePet([FromRoute] Guid petId, [FromBody] UpdatePetRequest petRequest)
        {
            var result = await _petService.UpdateAsync(petId, _mapper.Map<Pet>(petRequest));
            return result ? NoContent() : (IActionResult)NotFound();
        }

        /// <summary>
        /// Deletes a pet in the system
        /// </summary>
        /// <response code="204">Pet successfully deleted</response>
        /// <response code="404">Either Pet wasn't find, or you don't have access to delete it</response>
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(Status404NotFound)]
        [HttpDelete(ApiRoutes.Pets.Delete)]
        public async Task<IActionResult> DeletePet([FromRoute] Guid petId)
        {
            var result = await _petService.DeleteAsync(petId);
            return result ? (IActionResult)NoContent() : NotFound();
        }
    }
}
