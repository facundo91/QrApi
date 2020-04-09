using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using qrAPI.Contracts.v1;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Contracts.v1.Requests.Update;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Infrastructure.Options;
using qrAPI.Presentation.Adapters.v1.Interfaces;
using qrAPI.Presentation.Cache;

namespace qrAPI.Presentation.Controllers.v1
{
    //Should only be request and respond the corresponding version typed of object
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(IgnoreApi = false)]
    //[ODataRoutePrefix("Qrs")]
    public class QrsController : ControllerBase
    {
        private readonly IQrsControllerAdapter _controllerAdapter;

        public QrsController(IQrsControllerAdapter controllerAdapter)
        {
            _controllerAdapter = controllerAdapter;
        }

        [HttpGet(ApiRoutes.Qrs.GetAll)]
        [Cached(30)]
        [EnableQuery]
        public async Task<IActionResult> GetAllQrs()
        {
            var result = await _controllerAdapter.GetAllAsync<IEnumerable<QrResponse>>();
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Qrs.Get)]
        [FeatureGate(FeatureFlags.EndpointFlag)]
        [Cached(30)]
        public async Task<IActionResult> GetQr([FromRoute] Guid qrId)
        {
            var result = await _controllerAdapter.GetByIdAsync<QrResponse>(qrId);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        [HttpPost(ApiRoutes.Qrs.Create)]
        [FeatureGate(FeatureFlags.EndpointFlag)]
        public async Task<IActionResult> CreateQr([FromBody] CreateQrRequest qrRequest)
        {
            var result = await _controllerAdapter.CreateAsync<QrResponse>(qrRequest);
            return result != null
                ? CreatedAtAction("CreateQr", new { id = result.Id }, result)
                : (IActionResult)BadRequest();
        }

        [HttpPut(ApiRoutes.Qrs.Update)]
        [FeatureGate(FeatureFlags.EndpointFlag)]
        public async Task<IActionResult> UpdateQr([FromRoute] Guid qrId, [FromBody] UpdateQrRequest qrRequest)
        {
            var result = await _controllerAdapter.UpdateAsync(qrId, qrRequest);
            return result ? Ok() : (IActionResult)NotFound();
        }

        [HttpDelete(ApiRoutes.Qrs.Delete)]
        public async Task<IActionResult> DeleteQr([FromRoute] Guid qrId)
        {
            var result = await _controllerAdapter.DeleteAsync(qrId);
            return result ? (IActionResult)NoContent() : NotFound();
        }
    }
}