using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using qrAPI.Contracts.v1;
using qrAPI.Contracts.v1.Requests;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Contracts.v1.Requests.Update;
using qrAPI.Contracts.v1.Responses;
using Xunit;

namespace qrAPI.IntegrationTests.v1
{
    public class PetsControllerTests : IntegrationTest
    {

        [Fact]
        public async Task GetAllPets_WithoutPets_ReturnsEmpty()
        {
            //Arrange
            await AuthenticateAsync();
            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Pets.GetAll);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.ReadAsAsync<List<PetResponse>>().Result.Should().BeEmpty();
        }

        [Fact]
        public async Task CreatePet_ReturnsCreatedPet()
        {
            //Arrange
            await AuthenticateAsync();
            var createPetRequest = new CreatePetRequest { Name = "Test Pet", Birthdate = DateTime.Now, Gender = Gender.Female, PictureUrl = "https://Test.url" };
            //Act
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Pets.Create, createPetRequest);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var returnedPost = await response.Content.ReadAsAsync<PetResponse>();

            returnedPost.Name.Should().Be(createPetRequest.Name);
            returnedPost.Birthdate.Should().Be(createPetRequest.Birthdate);
            returnedPost.Gender.Should().Be(createPetRequest.Gender);
            (new Uri(returnedPost.PictureUrl)).Should().BeEquivalentTo(new Uri(createPetRequest.PictureUrl));
        }

        [Fact]
        public async Task CreatePet_AddsOneToCurrentUserPets()
        {
            //Arrange
            await AuthenticateAsync();
            var createPetRequest = new CreatePetRequest { Name = "Test Pet", Birthdate = DateTime.Now, Gender = Gender.Female, PictureUrl = "https://Test.url" };
            //Act
            await CreatePetAsync(createPetRequest);
            var response = await TestClient.GetAsync(ApiRoutes.Pets.GetAll);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var getAllPets = response.Content.ReadAsAsync<List<PetResponse>>().Result;
            getAllPets.Should().HaveCount(1);
            getAllPets.First().Name.Should().Be(createPetRequest.Name);
            getAllPets.First().Birthdate.Should().Be(createPetRequest.Birthdate);
            getAllPets.First().Gender.Should().Be(createPetRequest.Gender);
            new Uri(getAllPets.First().PictureUrl).Should().BeEquivalentTo(new Uri(createPetRequest.PictureUrl));
        }

        [Fact]
        public async Task GetPet_ReturnsSpecificPet()
        {
            //Arrange
            await AuthenticateAsync();
            var createPetRequest = new CreatePetRequest { Name = "Test Pet", Birthdate = DateTime.Now, Gender = Gender.Female, PictureUrl = "https://Test.url" };
            //Act
            var petCreated = await CreatePetAsync(createPetRequest);
            var response = await TestClient.GetAsync(ApiRoutes.Pets.Get.Replace("{petId}", petCreated.Id.ToString()));
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var getCreatedPet = response.Content.ReadAsAsync<PetResponse>().Result;
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
            await AuthenticateAsync();
            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Pets.Get.Replace("{petId}", Guid.NewGuid().ToString()));
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeletePet_DecrementPetsByOne()
        {
            //Arrange
            await AuthenticateAsync();
            var createPetRequest = new CreatePetRequest { Name = "Test Pet", Birthdate = DateTime.Now, Gender = Gender.Female, PictureUrl = "https://Test.url" };
            //Act
            var petCreated = await CreatePetAsync(createPetRequest);
            var response = await TestClient.DeleteAsync(ApiRoutes.Pets.Delete.Replace("{petId}", petCreated.Id.ToString()));
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            var getAll = await GetAllPetsAsync();
            getAll.Should().BeEmpty();
        }

        [Fact]
        public async Task DeleteUncreatedPet_ReturnsNotFound()
        {
            //Arrange
            await AuthenticateAsync();
            //Act
            var response = await TestClient.DeleteAsync(ApiRoutes.Pets.Delete.Replace("{petId}", Guid.NewGuid().ToString()));
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task OneUserCreatesAPet_AnotherUserCannotGetThatPet()
        {
            //Arrange
            var createPetRequest = new CreatePetRequest { Name = "Test Pet", Birthdate = DateTime.Now, Gender = Gender.Female, PictureUrl = "https://Test.url" };
            await AuthenticateAsync();
            var petCreated = await CreatePetAsync(createPetRequest);
            //Act
            await AuthenticateAsync("test@mail.com", "APass123!");
            var getAllPets = await TestClient.GetAsync(ApiRoutes.Pets.GetAll);
            var getAPet = await TestClient.GetAsync(ApiRoutes.Pets.Get.Replace("{petId}", petCreated.Id.ToString()));
            //Assert
            getAllPets.StatusCode.Should().Be(HttpStatusCode.OK);
            getAllPets.Content.ReadAsAsync<List<PetResponse>>().Result.Should().BeEmpty();
            getAPet.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task OneUserCreatesAPet_AnotherUserCannotDeleteThatPet()
        {
            //Arrange
            var createPetRequest = new CreatePetRequest { Name = "Test Pet", Birthdate = DateTime.Now, Gender = Gender.Female, PictureUrl = "https://Test.url" };
            await AuthenticateAsync();
            var petCreated = await CreatePetAsync(createPetRequest);
            //Act
            await AuthenticateAsync("test@mail.com", "APass123!");
            var deleteAPet = await TestClient.DeleteAsync(ApiRoutes.Pets.Delete.Replace("{petId}", petCreated.Id.ToString()));
            //Assert
            deleteAPet.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdatePet_UpdatesPet()
        {
            //Arrange
            var createPetRequest = new CreatePetRequest
            {
                Name = "New Pet",
                Birthdate = DateTime.Now,
                Gender = Gender.Female,
                PictureUrl = "https://Test.url"
            };
            await AuthenticateAsync();
            var petCreated = await CreatePetAsync(createPetRequest);
            //Act
            var updatePetRequest = new UpdatePetRequest
            {
                Name = "Updated Pet",
                Birthdate = DateTime.Now,
                Gender = Gender.Male,
                PictureUrl = petCreated.PictureUrl, //Stays the same
                OwnerId = petCreated.Owner.GetValueOrDefault()
            };
            var response = await TestClient.PutAsJsonAsync(ApiRoutes.Pets.Update.Replace("{petId}", petCreated.Id.ToString()), updatePetRequest);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            var updatedPet = await GetPetAsync(petCreated.Id);
            updatedPet.Name.Should().Be(updatePetRequest.Name);
            updatedPet.Birthdate.Should().Be(updatePetRequest.Birthdate);
            updatedPet.Gender.Should().Be(updatePetRequest.Gender);
            new Uri(updatedPet.PictureUrl).Should().BeEquivalentTo(new Uri(updatePetRequest.PictureUrl));
            updatedPet.Owner.Should().Be(updatePetRequest.OwnerId);
        }

        [Fact]
        public async Task CreatePet_CannotBeUpdateByAnotherUser()
        {
            //Arrange
            var createPetRequest = new CreatePetRequest
            {
                Name = "New Pet",
                Birthdate = DateTime.Now,
                Gender = Gender.Female,
                PictureUrl = "https://Test.url"
            };
            await AuthenticateAsync();
            var petCreated = await CreatePetAsync(createPetRequest);
            //Act
            var updatePetRequest = new UpdatePetRequest
            {
                Name = "Updated Pet",
                Birthdate = DateTime.Now,
                Gender = Gender.Male,
                PictureUrl = petCreated.PictureUrl, //Stays the same
                OwnerId = petCreated.Owner.GetValueOrDefault()
            };
            await AuthenticateAsync("test@mail.com", "APass123!");
            var response = await TestClient.PutAsJsonAsync(ApiRoutes.Pets.Update.Replace("{petId}", petCreated.Id.ToString()), updatePetRequest);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}