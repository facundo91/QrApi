using System;
using Microsoft.AspNetCore.Identity;
using qrAPI.DAL.Dtos;

namespace qrAPI.IntegrationTests.ExampleObjects
{
    public class DtoExampleObjects
    {
        public readonly PetDto PetDto;
        public readonly UserDto UserDto;
        public readonly string Breed;
        public readonly QrDto QrDto;
        public readonly UserPetDto UserPetDto;

        public DtoExampleObjects()
        {
            Breed = "Chihuahua";
            QrDto = new QrDto
            {
                Id = Guid.NewGuid(),
                Name = "My Awesome PetDto",
                Pet = PetDto
            };
            PetDto = new PetDto
            {
                Id = Guid.NewGuid(),
                Name = "Test Name",
                Breed = Breed,
                Birthdate = DateTime.Now,
                Gender = Gender.Male,
                PictureUrl = new Uri("https://test.url/")
            };
            QrDto.Pet = PetDto;
            QrDto.PetId = PetDto.Id;
            UserDto = new UserDto
            {
                Id = Guid.NewGuid(),
                Identity = new IdentityUser("Test UserDto")
            };
            UserPetDto = new UserPetDto
            {
                Id = Guid.NewGuid(),
                PetId = PetDto.Id,
                Pet = PetDto,
                UserId = UserDto.Id,
                User = UserDto
            };
            PetDto.UserPets = new[] { UserPetDto };
            UserDto.UserPets = new[] { UserPetDto };
        }
    }
}
