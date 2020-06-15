using AutoBogus;
using qrAPI.Contracts.v1.Requests;
using qrAPI.Contracts.v1.Requests.Update;

namespace qrAPI.Contracts.v1.Fakers.Requests.Update
{
    public sealed class UpdatePetRequestFaker : AutoFaker<UpdatePetRequest>
    {
        public UpdatePetRequestFaker()
        {
            RuleFor(fake => fake.PictureUrl, faker => faker.Internet.Avatar());
            RuleFor(fake => fake.Name, faker => faker.Name.FirstName());
            RuleFor(fake => fake.Gender, faker => faker.PickRandom<Gender>());
            RuleFor(fake => fake.Birthdate, faker => faker.Date.Past(12));
        }

    }
}
