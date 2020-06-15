using qrAPI.Contracts.v1.Fakers.Requests.Update;
using qrAPI.Contracts.v1.Requests.Update;
using Swashbuckle.AspNetCore.Filters;

namespace qrAPI.Swagger.Examples.Requests.Update
{
    public class UpdatePetRequestExampleProvider : IExamplesProvider<UpdatePetRequest>
    {
        public UpdatePetRequest GetExamples() => new UpdatePetRequestFaker().Generate();
    }
}
