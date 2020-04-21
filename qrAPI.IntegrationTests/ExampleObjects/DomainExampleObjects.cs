namespace qrAPI.IntegrationTests.ExampleObjects
{
    using System;
    using Microsoft.AspNetCore.Identity;
    using Logic.Domain;
    public class DomainExampleObjects
    {
        public readonly Breed BreedExample;
        public readonly Qr QrExample;
        public readonly Pet PetExample;
        public readonly User UserExample;

        public DomainExampleObjects()
        {
            BreedExample = new Breed
            {
                Id = Guid.NewGuid(),
                Name = "Chihuahua"
            };
            QrExample = new Qr
            {
                Id = Guid.NewGuid(),
                Name = "My Awesome Pet",
                Pet = PetExample
            };
            PetExample = new Pet
            {
                Name = "Pampa",
                Birthdate = DateTime.Now,
                Breed = BreedExample,
                Gender = Gender.Female,
                Id = Guid.NewGuid(),
                PictureUrl = new Uri("https://test.url/"),
                Qr = QrExample
            };
            QrExample.Pet = PetExample;
            UserExample = new User
            {
                Id = Guid.NewGuid(),
                Identity = new IdentityUser("Test User")
            };
            PetExample.AddNewOwner(UserExample);
        }
    }
}
