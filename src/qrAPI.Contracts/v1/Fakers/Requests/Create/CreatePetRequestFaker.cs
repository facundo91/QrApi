using AutoBogus;
using qrAPI.Contracts.v1.Requests;
using qrAPI.Contracts.v1.Requests.Create;

namespace qrAPI.Contracts.v1.Fakers.Requests.Create
{
    public sealed class CreatePetRequestFaker : AutoFaker<CreatePetRequest>
    {
        public CreatePetRequestFaker()
        {
            RuleFor(fake => fake.PictureUrl, faker => faker.Internet.Avatar());
            RuleFor(fake => fake.Name, faker => faker.Name.FirstName());
            RuleFor(fake => fake.Gender, faker => faker.PickRandom<Gender>());
            RuleFor(fake => fake.Birthdate, faker => faker.Date.Past(12));
        }

    }
}
