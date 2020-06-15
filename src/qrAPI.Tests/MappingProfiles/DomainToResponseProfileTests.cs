using AutoMapper;
using FluentAssertions;
using qrAPI.App.Domain.Fakers;
using qrAPI.Contracts.v1.Responses;
using qrAPI.MappingProfiles;
using System;
using Xunit;

namespace qrAPI.Tests.MappingProfiles
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
            var pet = new PetFaker().Generate();
            var petResponse = Mapper.Map<PetResponse>(pet);
            petResponse.Breed.Should().BeEquivalentTo(pet.Breed.Name);
            petResponse.PictureUrl.Should().BeEquivalentTo(pet.PictureUrl.OriginalString);
            petResponse.Should().BeEquivalentTo(pet, options =>
                options
                    .ExcludingMissingMembers()
                    .Excluding(x => x.Breed)
                    .Excluding(x => x.PictureUrl));
        }

        [Fact]
        public void AutoMapper_QrToQrResponseMappingConfiguration()
        {
            var qr = new QrFaker().Generate();
            var qrResponse = Mapper.Map<QrResponse>(qr);
            qrResponse.Should().BeEquivalentTo(qr, options =>
                options
                    .Excluding(x => x.Pet.PictureUrl)
                    .ExcludingMissingMembers());
        }

        [Fact]
        public void AutoMapper_UserToUserResponseMappingConfiguration()
        {
            var user = new UserFaker().Generate();
            var userResponse = Mapper.Map<UserResponse>(user);
            userResponse.Should().BeEquivalentTo(user, options =>
                options.ExcludingMissingMembers());
        }
    }

    public class DomainToResponseProfileFixture : IDisposable
    {
        protected readonly IMapper Mapper;

        protected DomainToResponseProfileFixture()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
                cfg.AddProfile<DomainToResponseProfile>());
            Mapper = new Mapper(mapperConfiguration);
        }

        public void Dispose()
        {
        }
    }
}
