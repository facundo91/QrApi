﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using qrAPI.Contracts.v2;
using qrAPI.Contracts.v2.Requests.Create;
using qrAPI.Contracts.v2.Requests.Update;
using qrAPI.Contracts.v2.Responses;
using qrAPI.Infrastructure.Options;
using qrAPI.Logic.Domain;
using qrAPI.Presentation.Adapters.v2;
using qrAPI.Presentation.Cache;

namespace qrAPI.Presentation.Controllers.v2
{
    //Should only be request and respond the corresponding version typed of object
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class QrsController : ControllerBase
    {
        private readonly IControllerAdapter<Qr> _controllerAdapter;

        public QrsController(IControllerAdapter<Qr> controllerAdapter)
        {
            _controllerAdapter = controllerAdapter;
        }

        [HttpGet(ApiRoutes.Qrs.GetAll)]
        [Cached(30)]
        public async Task<IActionResult> GetAllQrs()
        {
            var result = await _controllerAdapter.GetAllAsync<IEnumerable<QrResponse>>();
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Qrs.Get)]
        [FeatureGate(FeatureFlags.EndpointFlag)]
        [Authorize(Policy = "MustWorkForQRightThing")]
        [Cached(30)]
        public async Task<IActionResult> GetQr([FromRoute] Guid qrId)
        {
            var result = await _controllerAdapter.GetByIdAsync<QrResponse>(qrId);
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        [HttpPost(ApiRoutes.Qrs.Create)]
        [FeatureGate(FeatureFlags.EndpointFlag)]
        [Authorize(Policy = "MustWorkForQRightThing")]
        public async Task<IActionResult> CreateQr([FromBody] CreateQrRequest qrRequest)
        {
            var result = await _controllerAdapter.CreateAsync<QrResponse>(qrRequest);
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