﻿using System;
using qrAPI.Contracts.v1.Requests;
using qrAPI.Contracts.v1.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace qrAPI.Swagger.Examples.Responses
{
    public class PetResponseExample : IExamplesProvider<PetResponse>
    {
        public PetResponse GetExamples()
        {
            return new PetResponse
            {
                Id = Guid.NewGuid(),
                Birthdate = DateTime.Now,
                Gender = Gender.Female,
                Name = "Vulpi",
                OwnerId = Guid.NewGuid(),
                PictureUrl = "https://picture.com/vulpi"
            };
        }
    }
}