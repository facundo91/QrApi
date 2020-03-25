﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using qrAPI.Adapters;
using qrAPI.Cache;
using qrAPI.Contracts.v1;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Logic.Domain;

namespace qrAPI.Controllers.v1
{
    public class MedicalRecordsController : Controller
    {
        private readonly IMedicalRecordsControllerAdapter _medicalRecordsControllerAdapter;

        public MedicalRecordsController(IMedicalRecordsControllerAdapter medicalRecordsControllerAdapter)
        {
            _medicalRecordsControllerAdapter = medicalRecordsControllerAdapter;
        }

        [HttpGet(ApiRoutes.MedicalRecords.GetAll)]
        [Cached(600)]
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