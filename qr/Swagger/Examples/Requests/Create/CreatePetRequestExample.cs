using System;
using qrAPI.Contracts.v1.Requests;
using qrAPI.Contracts.v1.Requests.Create;
using Swashbuckle.AspNetCore.Filters;

namespace qrAPI.Swagger.Examples.Requests.Create
{
    public class CreatePetRequestExample : IExamplesProvider<CreatePetRequest>
    {
        public CreatePetRequest GetExamples()
        {
            return new CreatePetRequest
            {
                Birthdate = DateTime.Now,
                Gender = Gender.Male,
                Name = "Pampa",
                OwnerId = Guid.NewGuid(),
                PictureUrl = "https://picture.com/pampa"
            };
        }
    }
}
