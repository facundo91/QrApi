using AutoBogus;
using AutoMapper;
using FluentAssertions;
using qrAPI.App.Domain;
using qrAPI.App.Domain.Fakers;
using qrAPI.DAL.Dtos;
using qrAPI.MappingProfiles;
using System;
using System.Linq;
using Xunit;

namespace qrAPI.Tests.MappingProfiles
{

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
            var user = new AutoFaker<User>().Generate();
            var userPetDto = Mapper.Map<UserPetDto>(user);
            userPetDto.User.Should().BeEquivalentTo(user, options =>
                options.ExcludingMissingMembers());
        }

        [Fact]
        public void AutoMapper_UserToUserDtoMappingConfiguration()
        {
            var user = new AutoFaker<User>().Generate();
            var userDto = Mapper.Map<UserDto>(user);
            user.Should().BeEquivalentTo(userDto, options =>
                options.ExcludingMissingMembers());
            userDto.Should().BeEquivalentTo(user, options =>
                options.ExcludingMissingMembers());
        }

        [Fact]
        public void AutoMapper_QrToQrDtoUserToUserDtoMappingConfiguration()
        {
            var qr = new QrFaker().Generate();
            var qrDto = Mapper.Map<QrDto>(qr);
            qr.Should().BeEquivalentTo(qrDto, options =>
                options.Excluding(x => x.Pet).ExcludingMissingMembers());
            qrDto.Should().BeEquivalentTo(qr, options =>
                options.Excluding(x => x.Pet).ExcludingMissingMembers());
            qr.Pet.Id.Should().Be(qrDto.Pet.Id);
        }

        [Fact]
        public void AutoMapper_PetToPetDtoMappingConfiguration()
        {
            var pet = new PetFaker().Generate();
            var petDto = Mapper.Map<PetDto>(pet);
            pet.Should().BeEquivalentTo(petDto, opt =>
                opt.Excluding(x => x.Breed).ExcludingMissingMembers());
            petDto.Breed.Should().BeEquivalentTo(pet.Breed.Name);
            petDto.Should().BeEquivalentTo(pet, opt =>
                opt.ExcludingMissingMembers());
            pet.Owners.Select(owner => owner.Id).Should().BeEquivalentTo(petDto.UserPets.Select(userPets => userPets.UserId));
            petDto.UserPets.Should().HaveCount(pet.Owners.Count);
            petDto.UserPets.FirstOrDefault()?.Pet.Should().BeEquivalentTo(pet, opt => opt.ExcludingMissingMembers());
            petDto.UserPets.FirstOrDefault()?.PetId.Should().Be(pet.Id);
        }
    }

    public class DomainToDtoProfileFixture : IDisposable
    {
        protected readonly IMapper Mapper;

        protected DomainToDtoProfileFixture()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
                cfg.AddProfile<DomainToDtoProfile>());
            Mapper = new Mapper(mapperConfiguration);
        }

        public void Dispose()
        {
        }
    }
}
