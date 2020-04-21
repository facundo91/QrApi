
namespace qrAPI.IntegrationTests.Mapping_Profiles
{
    using ExampleObjects;
    using System;
    using System.Linq;
    using AutoMapper;
    using FluentAssertions;
    using DAL.Dtos;
    using Logic.Domain;
    using MappingProfiles;
    using Xunit;
    public class DomainToDtoProfileTests : DomainToDtoProfileFixture
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void AutoMapper_UserToUserPetDtoMappingConfiguration()
        {
            var userPetDto = Mapper.Map<UserPetDto>(User);
            userPetDto.User.Should().BeEquivalentTo(User, options =>
                options.ExcludingMissingMembers());
        }

        [Fact]
        public void AutoMapper_UserToUserDtoMappingConfiguration()
        {
            var userDto = Mapper.Map<UserDto>(User);
            User.Should().BeEquivalentTo(userDto, options =>
                options.ExcludingMissingMembers());
            userDto.Should().BeEquivalentTo(User, options =>
                options.ExcludingMissingMembers());
        }

        [Fact]
        public void AutoMapper_QrToQrDtoUserToUserDtoMappingConfiguration()
        {
            var qrDto = Mapper.Map<QrDto>(Qr);
            Qr.Should().BeEquivalentTo(qrDto, options =>
                options.Excluding(x => x.Pet).ExcludingMissingMembers());
            qrDto.Should().BeEquivalentTo(Qr, options =>
                options.Excluding(x => x.Pet).ExcludingMissingMembers());
            Qr.Pet.Id.Should().Be(qrDto.Pet.Id);
        }

        [Fact]
        public void AutoMapper_PetToPetDtoMappingConfiguration()
        {
            var petDto = Mapper.Map<PetDto>(Pet);
            Pet.Should().BeEquivalentTo(petDto, opt =>
                opt.Excluding(x => x.Breed).ExcludingMissingMembers());
            petDto.Breed.Should().BeEquivalentTo(Pet.Breed.Name);
            petDto.Should().BeEquivalentTo(Pet, opt =>
                opt.ExcludingMissingMembers());
            Pet.Owners.Select(owner => owner.Id).Should().BeEquivalentTo(petDto.UserPets.Select(userPets => userPets.UserId));
            petDto.UserPets.Should().HaveCount(1);
            petDto.UserPets.FirstOrDefault()?.Pet.Should().BeEquivalentTo(Pet, opt => opt.ExcludingMissingMembers());
            petDto.UserPets.FirstOrDefault()?.PetId.Should().Be(Pet.Id);
        }
    }

    public class DomainToDtoProfileFixture : IDisposable
    {
        protected readonly IMapper Mapper;
        protected readonly Pet Pet;
        protected readonly User User;
        protected readonly Qr Qr;

        protected DomainToDtoProfileFixture()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
                cfg.AddProfile<DomainToDtoProfile>());
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
