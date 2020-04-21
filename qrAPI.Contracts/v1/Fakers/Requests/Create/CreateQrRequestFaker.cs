using AutoBogus;
using qrAPI.Contracts.v1.Requests.Create;

namespace qrAPI.Contracts.v1.Fakers.Requests.Create
{
    public sealed class CreateQrRequestFaker : AutoFaker<CreateQrRequest>
    {
        public CreateQrRequestFaker()
        {
            RuleFor(fake => fake.Name, faker => faker.Random.Word());
        }

    }
}
