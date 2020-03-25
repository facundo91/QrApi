﻿using System;
using System.Threading.Tasks;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Logic.Domain;

namespace qrAPI.Adapters
{
    public interface IMedicalRecordsControllerAdapter : IControllerAdapter<MedicalRecord>
    {
        Task<T> CreateAsync<T>(Guid objToCreate, CreateMedicalRecordRequest medicalRecord);
    }
}