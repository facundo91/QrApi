﻿using System;
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
using qrAPI.Infrastructure.Adapters;
using qrAPI.Infrastructure.Options;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services.Interfaces;
using static qrAPI.Contracts.ApiVersions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace qrAPI.Presentation.Controllers.v1
{
    [Produces("application/json")]
    [ApiVersion(V1Tag)]
    [ODataRoutePrefix("Qrs")]
    public class QrsController : ODataController
    {
        private readonly IMapperAdapter _mapperAdapter;
        private readonly IQrService _qrService;

        public QrsController(IMapperAdapter mapperAdapter, IQrService qrService)
        {
            _mapperAdapter = mapperAdapter;
            _qrService = qrService;
        }

        [ODataRoute]
        [ProducesResponseType(typeof(IEnumerable<QrResponse>), Status200OK)]
        [HttpGet(ApiRoutes.Qrs.GetAll)]
        [ResponseCache(VaryByQueryKeys = new[] { "*" }, Duration = 30)]
        [EnableQuery]
        public async Task<IActionResult> GetAllQrs()
        {
            var result = await _mapperAdapter.DoMapAsync<IEnumerable<QrResponse>>(async () => await _qrService.GetAllAsync());
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
            var result = await _mapperAdapter.DoMapAsync<QrResponse>(async () => await _qrService.GetByIdAsync(qrId));
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        [HttpPost(ApiRoutes.Qrs.Scan)]
        public async Task<IActionResult> ScanQr([FromRoute] Guid qrId)
        {
            await _qrService.ScanQr(qrId);
            return Ok();
        }

        [HttpPost(ApiRoutes.Qrs.Create)]
        [FeatureGate(FeatureFlags.EndpointFlag)]
        public async Task<IActionResult> CreateQr([FromBody] CreateQrRequest qrRequest)
        {
            var result = await _mapperAdapter.MapDoMapAsync<Qr, QrResponse>(qrRequest, async mapped => await _qrService.CreateAsync(mapped));
            return result != null
                ? CreatedAtAction("CreateQr", new { id = result.Id }, result)
                : (IActionResult)BadRequest();
        }

        [HttpPut(ApiRoutes.Qrs.Update)]
        [FeatureGate(FeatureFlags.EndpointFlag)]
        public async Task<IActionResult> UpdateQr([FromRoute] Guid qrId, [FromBody] UpdateQrRequest qrRequest)
        {
            var result = await _mapperAdapter.MapDoAsync(qrRequest, async mapped => await _qrService.UpdateAsync(qrId, (Qr)mapped));
            return result ? Ok() : (IActionResult)NotFound();
        }

        [HttpDelete(ApiRoutes.Qrs.Delete)]
        public async Task<IActionResult> DeleteQr([FromRoute] Guid qrId)
        {
            var result = await _qrService.DeleteAsync(qrId);
            return result ? (IActionResult)NoContent() : NotFound();
        }
    }
}