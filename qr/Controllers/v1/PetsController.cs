using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using qrAPI.Commands.Pets.ControllerCommands;
using qrAPI.Contracts.v1;
using qrAPI.Contracts.v1.Requests;
using qrAPI.Options;
using qrAPI.Queries.Pets.ControllerQueries;

namespace qrAPI.Controllers.v1
{
    public class PetsController : Controller
    {
        private readonly IMediator _mediator;

        public PetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.Pets.GetAll)]
        public async Task<IActionResult> GetAllQrs()
        {
            var query = new GetAllPetsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Pets.Get)]
        public async Task<IActionResult> GetPet([FromRoute] Guid qrId)
        {
            throw new NotImplementedException();
        }

        [HttpPost(ApiRoutes.Pets.Create)]
        public async Task<IActionResult> CreatePet([FromBody] CreatePetRequest petRequest)
        {
            var command = new CreatePetCommand(petRequest);
            var result = await _mediator.Send(command);
            return result != null
                ? CreatedAtAction("CreatePet", new { id = result.Id }, result)
                : (IActionResult)BadRequest();
        }

        [HttpPut(ApiRoutes.Pets.Update)]
        [FeatureGate(FeatureFlags.EndpointFlag)]
        public async Task<IActionResult> UpdatePet([FromRoute] Guid qrId, [FromBody] UpdatePetRequest petRequest)
        {
            throw new NotImplementedException();

        }

        [HttpDelete(ApiRoutes.Pets.Delete)]
        public async Task<IActionResult> DeletePet([FromRoute] Guid qrId)
        {
            throw new NotImplementedException();

        }
    }
}
