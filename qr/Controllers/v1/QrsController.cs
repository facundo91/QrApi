using MediatR;
using Microsoft.AspNetCore.Mvc;
using qrAPI.Commands.Qrs.ControllerCommands;
using qrAPI.Contracts.v1;
using qrAPI.Contracts.v1.Requests;
using qrAPI.Queries.Qrs.ControllerQueries;
using System;
using System.Threading.Tasks;
using Microsoft.FeatureManagement.Mvc;
using qrAPI.Options;

namespace qrAPI.Controllers.v1
{
    //Should only be request and respond the corresponding version typed of object
    public class QrsController : Controller
    {
        private readonly IMediator _mediator;

        public QrsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.Qrs.GetAll)]
        public async Task<IActionResult> GetAllQrs()
        {
            var query = new GetAllQrsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Qrs.Get)]
        public async Task<IActionResult> GetQr([FromRoute] Guid qrId)
        {
            var query = new GetQrByIdQuery(qrId);
            var result = await _mediator.Send(query);
            return result != null ? (IActionResult) Ok(result) : NotFound();
        }

        [HttpPost(ApiRoutes.Qrs.Create)]
        public async Task<IActionResult> CreateQr([FromBody] CreateQrRequest qrRequest)
        {
            var command = new CreateQrCommand(qrRequest);
            var result = await _mediator.Send(command);
            return result //TODO
                ? Ok()
                //? CreatedAtAction("CreateQr", new {id = result.Id}, result) 
                : (IActionResult) BadRequest();

            //var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            //var locationUri = baseUrl + "/" + ApiRoutes.Qrs.Get.Replace("{qrId}", result.Id.ToString());
            //return Created(locationUri, result);
        }

        [HttpPut(ApiRoutes.Qrs.Update)]
        [FeatureGate(FeatureFlags.EndpointFlag)]
        public async Task<IActionResult> UpdateQr([FromRoute] Guid qrId, [FromBody] UpdateQrRequest qrRequest)
        {
            var command = new UpdateQrCommand(qrId, qrRequest);
            var result = await _mediator.Send(command);
            return result ? Ok() : (IActionResult) NotFound();
        }

        [HttpDelete(ApiRoutes.Qrs.Delete)]
        public async Task<IActionResult> DeleteQr([FromRoute] Guid qrId)
        {
            var command = new DeleteQrCommand(qrId);
            var result = await _mediator.Send(command);
            return result ? (IActionResult) NoContent() : NotFound();
        }
    }
}