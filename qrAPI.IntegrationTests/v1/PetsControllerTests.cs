using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using qrAPI.Contracts.v1.Fakers.Requests.Create;
using qrAPI.Contracts.v1.Requests;
using qrAPI.Contracts.v1.Requests.Update;
using Xunit;

namespace qrAPI.IntegrationTests.v1
{
    [Collection("Sequential")]
    public class PetsControllerTests : IntegrationTest
    {

        [Fact]
        public async Task GetAllPets_WithoutPets_ReturnsEmptyResponse()
        {
            //Arrange
            await RegisterAsync();
            //Act
            var response = await PetsApi.GetAllAsync();
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().BeEmpty();
        }


        [Fact]
        public async Task GetAllPets_Odata_WithOnePet_ReturnsPet()
        {
            //Arrange
            await RegisterAsync();
            var createPetRequest = new CreatePetRequestFaker().Generate();
            await PetsApi.CreateAsync(createPetRequest);
            //Act
            var pets = await PetsOdataClient
                .Select(petSelect => new { petSelect.Name, petSelect.Birthdate }).FindEntriesAsync();
            //Assert
            var petResponses = pets.ToList();
            petResponses.Should().HaveCount(1);
            petResponses.First().Name.Should().BeEquivalentTo(createPetRequest.Name);
            petResponses.First().Birthdate.Should().Be(createPetRequest.Birthdate);
            petResponses.First().PictureUrl.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task CreatePet_ReturnsCreatedPet()
        {
            //Arrange
            await RegisterAsync();
            var createPetRequest = new CreatePetRequestFaker().Generate();
            //Act
            var response = await PetsApi.CreateAsync(createPetRequest);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var returnedPost = response.Content;

            returnedPost.Name.Should().Be(createPetRequest.Name);
            returnedPost.Birthdate.Should().Be(createPetRequest.Birthdate);
            returnedPost.Gender.Should().Be(createPetRequest.Gender);
            new Uri(returnedPost.PictureUrl).Should().BeEquivalentTo(new Uri(createPetRequest.PictureUrl));
        }

        [Fact]
        public async Task CreatePet_AddsOneToCurrentUserPets()
        {
            //Arrange
            await RegisterAsync();
            var createPetRequest = new CreatePetRequestFaker().Generate();
            //Act
            await PetsApi.CreateAsync(createPetRequest);
            var response = await PetsApi.GetAllAsync();
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var getAllPets = response.Content;
            var petResponses = getAllPets.ToList();
            petResponses.Should().HaveCount(1);
            petResponses.First().Name.Should().Be(createPetRequest.Name);
            petResponses.First().Birthdate.Should().Be(createPetRequest.Birthdate);
            petResponses.First().Gender.Should().Be(createPetRequest.Gender);
            new Uri(petResponses.First().PictureUrl).Should().BeEquivalentTo(new Uri(createPetRequest.PictureUrl));
        }

        [Fact]
        public async Task GetPet_ReturnsSpecificPet()
        {
            //Arrange
            await RegisterAsync();
            var createPetRequest = new CreatePetRequestFaker().Generate();
            //Act
            var petCreated = (await PetsApi.CreateAsync(createPetRequest)).Content;
            var response = await PetsApi.GetAsync(petCreated.Id);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var getCreatedPet = response.Content;
            getCreatedPet.Id.Should().Be(petCreated.Id);
            getCreatedPet.Name.Should().Be(petCreated.Name).And.Be(createPetRequest.Name);
            getCreatedPet.Birthdate.Should().Be(petCreated.Birthdate).And.Be(createPetRequest.Birthdate);
            getCreatedPet.Gender.Should().Be(petCreated.Gender).And.Be(createPetRequest.Gender);
            new Uri(getCreatedPet.PictureUrl).Should().BeEquivalentTo(new Uri(petCreated.PictureUrl));
            new Uri(getCreatedPet.PictureUrl).Should().BeEquivalentTo(new Uri(createPetRequest.PictureUrl));
        }

        [Fact]
        public async Task GetPet_WithoutAny_ReturnsNotFound()
        {
            //Arrange
            await RegisterAsync();
            //Act
            var response = await PetsApi.GetAsync(Guid.NewGuid());
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeletePet_DecrementPetsByOne()
        {
            //Arrange
            await RegisterAsync();
            var createPetRequest = new CreatePetRequestFaker().Generate();
            //Act
            var petCreated = (await PetsApi.CreateAsync(createPetRequest)).Content;
            var response = await PetsApi.DeleteAsync(petCreated.Id);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            var getAll = (await PetsApi.GetAllAsync()).Content;
            getAll.Should().BeEmpty();
        }

        [Fact]
        public async Task DeleteUncreatedPet_ReturnsNotFound()
        {
            //Arrange
            await RegisterAsync();
            //Act
            var response = await PetsApi.DeleteAsync(Guid.NewGuid());
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task OneUserCreatesAPet_AnotherUserCannotGetThatPet()
        {
            //Arrange
            await RegisterAsync();
            var createPetRequest = new CreatePetRequestFaker().Generate();
            var petCreated = (await PetsApi.CreateAsync(createPetRequest)).Content;
            //Act
            await RegisterAsync(new Faker().Internet.Email(), "T3stPa$$");
            var getAllPets = await PetsApi.GetAllAsync();
            var getAPet = await PetsApi.GetAsync(petCreated.Id);
            //Assert
            getAllPets.StatusCode.Should().Be(HttpStatusCode.OK);
            getAllPets.Content.Should().BeEmpty();
            getAPet.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task OneUserCreatesAPet_AnotherUserCannotDeleteThatPet()
        {
            //Arrange
            await RegisterAsync();
            var createPetRequest = new CreatePetRequestFaker().Generate();
            var petCreated = (await PetsApi.CreateAsync(createPetRequest)).Content;
            //Act
            await RegisterAsync(new Faker().Internet.Email(), "T3stPa$$");
            var deleteAPet = await PetsApi.DeleteAsync(petCreated.Id);
            //Assert
            deleteAPet.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdatePet_UpdatesPet()
        {
            //Arrange
            await RegisterAsync();
            var createPetRequest = new CreatePetRequestFaker().Generate();
            var petCreated = (await PetsApi.CreateAsync(createPetRequest)).Content;
            //Act
            var updatePetRequest = new UpdatePetRequest
            {
                Name = "Updated Pet",
                Birthdate = DateTime.Now,
                Gender = Gender.Male,
                PictureUrl = petCreated.PictureUrl, //Stays the same
                Owners = petCreated.Owners.Select(x=>x.Id).ToArray()
            };
            var response = await PetsApi.UpdateAsync(petCreated.Id, updatePetRequest);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            var updatedPet = (await PetsApi.GetAsync(petCreated.Id)).Content;
            updatedPet.Name.Should().Be(updatePetRequest.Name);
            updatedPet.Birthdate.Should().Be(updatePetRequest.Birthdate);
            updatedPet.Gender.Should().Be(updatePetRequest.Gender);
            new Uri(updatedPet.PictureUrl).Should().BeEquivalentTo(new Uri(updatePetRequest.PictureUrl));
            updatedPet.Owners.Select(x=>x.Id).ToArray().Should().BeEquivalentTo(updatePetRequest.Owners);
        }

        [Fact]
        public async Task CreatePet_CannotBeUpdateByAnotherUser()
        {
            //Arrange
            await RegisterAsync();
            var createPetRequest = new CreatePetRequestFaker().Generate();
            var petCreated = (await PetsApi.CreateAsync(createPetRequest)).Content;
            //Act
            var updatePetRequest = new UpdatePetRequest
            {
                Name = "Updated Pet",
                Birthdate = DateTime.Now,
                Gender = Gender.Male,
                PictureUrl = petCreated.PictureUrl, //Stays the same
                Owners = petCreated.Owners.Select(x => x.Id).ToArray()
            };
            await RegisterAsync("test@mail.com", "APass123!");
            var response = await PetsApi.UpdateAsync(petCreated.Id, updatePetRequest);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}