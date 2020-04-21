using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using qrAPI.Contracts.v1.Fakers.Requests.Create;
using qrAPI.Contracts.v1.Fakers.Requests.Update;
using qrAPI.Logic.Domain;
using qrAPI.MappingProfiles;
using Xunit;

namespace qrAPI.IntegrationTests.Mapping_Profiles
{
    public class RequestToDomainProfileTests : RequestToDomainProfileFixture
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void AutoMapper_CreatePetRequestToPetMappingConfiguration_IsValid()
        {
            var createPetRequest = new CreatePetRequestFaker().Generate();
            var pet = Mapper.Map<Pet>(createPetRequest);
            pet.Breed.Name.Should().BeEquivalentTo(createPetRequest.Breed);
            pet.PictureUrl.OriginalString.Should().BeEquivalentTo(createPetRequest.PictureUrl);
            pet.Should().BeEquivalentTo(createPetRequest, options =>
                options
                    .Excluding(x => x.Breed)
                    .Excluding(x => x.PictureUrl));
            createPetRequest.Should().BeEquivalentTo(pet, options =>
                options.ExcludingMissingMembers()
                    .Excluding(x => x.PictureUrl));
        }

        [Fact]
        public void AutoMapper_UpdatePetRequestToPetMappingConfiguration_IsValid()
        {
            var updatePetRequest = new UpdatePetRequestFaker().Generate();
            var pet = Mapper.Map<Pet>(updatePetRequest);
            pet.Breed.Name.Should().BeEquivalentTo(updatePetRequest.Breed);
            pet.Owners.Select(x => x.Id).Should().BeEquivalentTo(updatePetRequest.Owners);
            pet.PictureUrl.OriginalString.Should().BeEquivalentTo(updatePetRequest.PictureUrl);
            pet.Should().BeEquivalentTo(updatePetRequest, options =>
                options
                    .Excluding(x => x.Owners)
                    .Excluding(x => x.Breed)
                    .Excluding(x => x.PictureUrl));
            updatePetRequest.Should().BeEquivalentTo(pet, options =>
                options.ExcludingMissingMembers()
                    .Excluding(x => x.PictureUrl));
        }

        [Fact]
        public void AutoMapper_CreateQrRequestToQrMappingConfiguration_IsValid()
        {
            var createQrRequest = new CreateQrRequestFaker().Generate();
            var qr = Mapper.Map<Qr>(createQrRequest);
            qr.Name.Should().BeEquivalentTo(createQrRequest.Name);
            qr.Pet.Id.Should().Be(createQrRequest.PetId);
        }

        [Fact]
        public void AutoMapper_UpdateQrRequestToQrMappingConfiguration_IsValid()
        {
            var updateQrRequest = new UpdateQrRequestFaker().Generate();
            var qr = Mapper.Map<Qr>(updateQrRequest);
            qr.Should().BeEquivalentTo(updateQrRequest);
        }

    }

    public class RequestToDomainProfileFixture : IDisposable
    {
        protected readonly IMapper Mapper;
        protected RequestToDomainProfileFixture()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
                cfg.AddProfile<RequestToDomainProfile>());
            Mapper = new Mapper(mapperConfiguration);
        }
        public void Dispose()
        {
        }
    }
}
