using System;
using System.Threading.Tasks;
using FluentAssertions;
using qrAPI.Contracts.v1.Requests;
using Xunit;

namespace qrAPI.IntegrationTests
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
            var createPetRequest = new CreatePetRequest { Name = "Test Pet" , Birthdate = DateTime.Now,Gender = Gender.Female,Owner = Guid.NewGuid(), PictureUrl = ""};
            //Act
            var petCreated = await CreatePetAsync(createPetRequest);
            //Assert
            petCreated.Id.Should().NotBeEmpty();
        }
    }
}