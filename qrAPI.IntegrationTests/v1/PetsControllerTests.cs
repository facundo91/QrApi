using System;
using System.Threading.Tasks;
using FluentAssertions;
using qrAPI.Contracts.v1.Requests;
using qrAPI.Contracts.v1.Requests.Create;
using Xunit;

namespace qrAPI.IntegrationTests.v1
{
    public class PetsControllerTests : IntegrationTest
    {

        [Fact]
        public async Task GetAllPets_WithoutPets_ReturnsEmpty()
        {
            //Arrange
            //Act
            var getAll= await GetAllPets();
            //Assert
            getAll.Should().BeEmpty();
        }

        [Fact]
        public async Task CreatePet_AddsOnePetToDal()
        {
            //Arrange
            var createPetRequest = new CreatePetRequest { Name = "Test Pet" , Birthdate = DateTime.Now,Gender = Gender.Female,
                OwnerId = Guid.NewGuid(), PictureUrl = ""};
            //Act
            var petCreated = await CreatePetAsync(createPetRequest);
            //Assert
            petCreated.Id.Should().NotBeEmpty();
        }
    }
}