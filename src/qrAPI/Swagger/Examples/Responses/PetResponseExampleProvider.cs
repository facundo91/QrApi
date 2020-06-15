using qrAPI.Contracts.v1.Fakers.Responses;
using qrAPI.Contracts.v1.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace qrAPI.Swagger.Examples.Responses
{
    public class PetResponseExampleProvider : IExamplesProvider<PetResponse>
    {
        public PetResponse GetExamples() => new PetResponseFaker().Generate();
    }
}
