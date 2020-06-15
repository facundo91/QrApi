using qrAPI.Contracts.v1.Fakers.Requests.Create;
using qrAPI.Contracts.v1.Requests.Create;
using Swashbuckle.AspNetCore.Filters;

namespace qrAPI.Swagger.Examples.Requests.Create
{
    public class CreateQrRequestExampleProvider : IExamplesProvider<CreateQrRequest>
    {
        public CreateQrRequest GetExamples() => new CreateQrRequestFaker().Generate();
    }
}