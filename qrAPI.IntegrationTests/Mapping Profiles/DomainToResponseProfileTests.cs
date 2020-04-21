using System;
using AutoMapper;
using FluentAssertions;
using qrAPI.Contracts.v1.Responses;
using qrAPI.IntegrationTests.ExampleObjects;
using qrAPI.Logic.Domain;
using qrAPI.MappingProfiles;
using Xunit;

namespace qrAPI.IntegrationTests.Mapping_Profiles
{
    public class DomainToResponseProfileTests : DomainToResponseProfileFixture
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void AutoMapper_PetToPetResponseMappingConfiguration()
        {
            var petResponse = Mapper.Map<PetResponse>(Pet);
            petResponse.Breed.Should().BeEquivalentTo(Pet.Breed.Name);
            petResponse.PictureUrl.Should().BeEquivalentTo(Pet.PictureUrl.OriginalString);
            petResponse.Should().BeEquivalentTo(Pet, options =>
                options
                    .ExcludingMissingMembers()
                    .Excluding(x => x.Breed)
                    .Excluding(x => x.PictureUrl));
        }

        [Fact]
        public void AutoMapper_QrToQrResponseMappingConfiguration()
        {
            var qrResponse = Mapper.Map<QrResponse>(Qr);
            qrResponse.Should().BeEquivalentTo(Qr, options =>
                options
                    .Excluding(x => x.Pet.PictureUrl)
                    .ExcludingMissingMembers());
        }

        [Fact]
        public void AutoMapper_UserToUserResponseMappingConfiguration()
        {
            var userResponse = Mapper.Map<UserResponse>(User);
            userResponse.Should().BeEquivalentTo(User, options =>
                options.ExcludingMissingMembers());
        }
    }

    public class DomainToResponseProfileFixture : IDisposable
    {
        protected readonly IMapper Mapper;
        protected readonly Pet Pet;
        protected readonly User User;
        protected readonly Qr Qr;

        protected DomainToResponseProfileFixture()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
                cfg.AddProfile<DomainToResponseProfile>());
            Mapper = new Mapper(mapperConfiguration); 
            var domainExampleObjects = new DomainExampleObjects();
            Pet = domainExampleObjects.PetExample;
            User = domainExampleObjects.UserExample;
            Qr = domainExampleObjects.QrExample;
        }

        public void Dispose()
        {
        }
    }
}
