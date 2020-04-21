using AutoBogus;
using qrAPI.Contracts.v1.Requests.Update;

namespace qrAPI.Contracts.v1.Fakers.Requests.Update
{
    public sealed class UpdateQrRequestFaker : AutoFaker<UpdateQrRequest>
    {
        public UpdateQrRequestFaker()
        {
            RuleFor(fake => fake.Name, faker => faker.Random.Word());
        }
    }
}
