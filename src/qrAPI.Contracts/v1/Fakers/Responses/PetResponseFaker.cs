using AutoBogus;
using qrAPI.Contracts.v1.Responses;

namespace qrAPI.Contracts.v1.Fakers.Responses
{
    public sealed class PetResponseFaker : AutoFaker<PetResponse>
    {
        public PetResponseFaker()
        {
            RuleFor(fake => fake.PictureUrl, faker => faker.Internet.Avatar());
            RuleFor(fake => fake.Name, faker => faker.Name.FirstName());
            RuleFor(fake => fake.Birthdate, faker => faker.Date.Past(12));
        }

    }
}