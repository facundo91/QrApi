using qrAPI.Contracts.v1.Fakers.Requests.Create;
using qrAPI.Contracts.v1.Requests.Create;
using Swashbuckle.AspNetCore.Filters;

namespace qrAPI.Swagger.Examples.Requests.Create
{
    public class CreatePetRequestExampleProvider : IExamplesProvider<CreatePetRequest>
    {
        public CreatePetRequest GetExamples() => new CreatePetRequestFaker().Generate();
    }
}