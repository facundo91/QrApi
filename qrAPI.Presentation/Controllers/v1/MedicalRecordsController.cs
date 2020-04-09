﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using qrAPI.Contracts.v1;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Logic.Domain;
using qrAPI.Presentation.Adapters.v1.Interfaces;
using static qrAPI.Contracts.ApiVersions;
using qrAPI.Presentation.Cache;

namespace qrAPI.Presentation.Controllers.v1
{
    [Produces("application/json")]
    [ODataRoutePrefix("MedicalRecords")]
    [ApiVersion(V1Tag)]
    public class MedicalRecordsController : ODataController
    {
        private readonly IMedicalRecordsControllerAdapter _medicalRecordsControllerAdapter;

        public MedicalRecordsController(IMedicalRecordsControllerAdapter medicalRecordsControllerAdapter)
        {
            _medicalRecordsControllerAdapter = medicalRecordsControllerAdapter;
        }

        [HttpGet(ApiRoutes.MedicalRecords.GetAll)]
        [ODataRoute]
        [EnableQuery]
        [Cached(60)]
        public async Task<IActionResult> GetAllMedicalRecords()
        {
            var result = await _medicalRecordsControllerAdapter.GetAllAsync<IEnumerable<MedicalRecord>>();
            return Ok(result);
        }

        /// <summary>
        /// Creates a medical record in the system
        /// </summary>
        /// <response code="201">Creates a medical record in the system</response>
        /// <response code="400">Unable to create the medical record due to validation error</response>
        [HttpPost(ApiRoutes.MedicalRecords.Create)]
        public async Task<IActionResult> CreateMedicalRecord([FromRoute] Guid petId, [FromBody] CreateMedicalRecordRequest medicalRecord)
        {
            var result = await _medicalRecordsControllerAdapter.CreateAsync<MedicalRecordResponse>(petId, medicalRecord);
            return result != null
                ? CreatedAtAction("CreateMedicalRecord", new { id = result.Id }, result)
                : (IActionResult)BadRequest();
        }
    }
}
