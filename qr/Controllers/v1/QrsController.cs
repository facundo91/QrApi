using Microsoft.AspNetCore.Mvc;
using qrAPI.Contracts.v1;
using qrAPI.Contracts.v1.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.FeatureManagement.Mvc;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Domain;
using qrAPI.Mediators;
using qrAPI.Options;

namespace qrAPI.Controllers.v1
{
    //Should only be request and respond the corresponding version typed of object
    public class QrsController : Controller
    {
        private readonly IControllerServiceMediator<Qr> _controllerServiceMediator;

        public QrsController(IControllerServiceMediator<Qr> controllerServiceMediator)
        {
            _controllerServiceMediator = controllerServiceMediator;
        }

        [HttpGet(ApiRoutes.Qrs.GetAll)]
        public async Task<IActionResult> GetAllQrs()
        {
            var result = await _controllerServiceMediator.GetAllAsync<IEnumerable<QrResponse>>();
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Qrs.Get)]
        public async Task<IActionResult> GetQr([FromRoute] Guid qrId)
        {
            var result = await _controllerServiceMediator.GetByIdAsync<QrResponse>(qrId);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        [HttpPost(ApiRoutes.Qrs.Create)]
        public async Task<IActionResult> CreateQr([FromBody] CreateQrRequest qrRequest)
        {
            var result = await _controllerServiceMediator.CreateAsync<QrResponse>(qrRequest);
            return result != null
                ? CreatedAtAction("CreateQr", new { id = result.Id }, result)
                : (IActionResult)BadRequest();

            //var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            //var locationUri = baseUrl + "/" + ApiRoutes.Qrs.Get.Replace("{qrId}", result.Id.ToString());
            //return Created(locationUri, result);
        }

        [HttpPut(ApiRoutes.Qrs.Update)]
        [FeatureGate(FeatureFlags.EndpointFlag)]
        public async Task<IActionResult> UpdateQr([FromRoute] Guid qrId, [FromBody] UpdateQrRequest qrRequest)
        {
            var result = await _controllerServiceMediator.UpdateAsync(qrRequest);
            return result ? Ok() : (IActionResult)NotFound();
        }

        [HttpDelete(ApiRoutes.Qrs.Delete)]
        public async Task<IActionResult> DeleteQr([FromRoute] Guid qrId)
        {
            var result = await _controllerServiceMediator.DeleteAsync(qrId);
            return result ? (IActionResult)NoContent() : NotFound();
        }
    }
}