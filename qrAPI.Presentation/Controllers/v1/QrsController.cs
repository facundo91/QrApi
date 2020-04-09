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
using static qrAPI.Contracts.ApiVersions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace qrAPI.Presentation.Controllers.v1
{
    //Should only be request and respond the corresponding version typed of object
    [Produces("application/json")]
    [ApiVersion(V1Tag)]
    [ODataRoutePrefix("Qrs")]
    public class QrsController : ODataController
    {
        private readonly IQrsControllerAdapter _controllerAdapter;

        public QrsController(IQrsControllerAdapter controllerAdapter)
        {
            _controllerAdapter = controllerAdapter;
        }

        [ODataRoute]
        [ProducesResponseType(typeof(IEnumerable<QrResponse>), Status200OK)]
        [HttpGet(ApiRoutes.Qrs.GetAll)]
        [ResponseCache(VaryByQueryKeys = new[] { "*" }, Duration = 30)]
        [EnableQuery]
        public async Task<IActionResult> GetAllQrs()
        {
            var result = await _controllerAdapter.GetAllAsync<IEnumerable<QrResponse>>();
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Qrs.Get)]
        [ODataRoute("{qrId}")]
        [EnableQuery]
        [ProducesResponseType(typeof(QrResponse), Status200OK)]
        [FeatureGate(FeatureFlags.EndpointFlag)]
        [ResponseCache(VaryByQueryKeys = new[] { "*" }, Duration = 30)]
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