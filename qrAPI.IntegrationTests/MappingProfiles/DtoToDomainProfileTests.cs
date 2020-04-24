using System;
using System.Linq;
using AutoBogus;
using AutoMapper;
using FluentAssertions;
using qrAPI.DAL.Dtos;
using qrAPI.DAL.Dtos.Fakers;
using qrAPI.Logic.Domain;
using qrAPI.MappingProfiles;
using Xunit;

namespace qrAPI.IntegrationTests.MappingProfiles
{
    public class DtoToDomainProfileTests : DtoToDomainProfileFixture
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void AutoMapper_UserDtoToUserMappingConfiguration()
        {
            var user = Mapper.Map<User>(UserDto);

            user.Should().BeEquivalentTo(UserDto, opt => opt.ExcludingMissingMembers());
            UserDto.Should().BeEquivalentTo(user, opt => opt.ExcludingMissingMembers());
            user.Pets.Select(x=>x.Id).Should().BeEquivalentTo(UserDto.UserPets.Select(x => x.PetId));
        }

        [Fact]
        public void AutoMapper_QrDtoToQrMappingConfiguration()
        {
            var qr = Mapper.Map<Qr>(QrDto);

            qr.Should().BeEquivalentTo(QrDto, opt => opt.Excluding(x=>x.Pet).ExcludingMissingMembers());
            QrDto.Should().BeEquivalentTo(qr, opt => opt.ExcludingMissingMembers());
            qr.Pet.Id.Should().Be(QrDto.PetId);
            QrDto.Pet.Id.Should().Be(qr.Pet.Id);
        }

        [Fact]
        public void AutoMapper_PetDtoToPetMappingConfiguration()
        {
            var pet = Mapper.Map<Pet>(PetDto);

            pet.Should().BeEquivalentTo(PetDto,opt => opt.Excluding(x => x.Breed).ExcludingMissingMembers());
            pet.Owners.Select(x => x.Id).Should().BeEquivalentTo(PetDto.UserPets.Select(x => x.UserId));
            PetDto.Should().BeEquivalentTo(pet, opt => opt.ExcludingMissingMembers());
            pet.Breed.Name.Should().BeEquivalentTo(PetDto.Breed);
        }
    }

    public class DtoToDomainProfileFixture :  IDisposable
    {
        protected readonly IMapper Mapper;
        protected PetDto PetDto;
        protected UserDto UserDto;
        protected QrDto QrDto;
        protected UserPetDto UserPetDto;

        protected DtoToDomainProfileFixture()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
                cfg.AddProfile<DtoToDomainProfile>());
            Mapper = new Mapper(mapperConfiguration);
            QrDto = new QrDtoFaker().Generate();
            QrDto.Pet = PetDto;
            PetDto = new PetDtoFaker().Generate();
            QrDto.Pet = PetDto;
            QrDto.PetId = PetDto.Id;
            UserDto = new AutoFaker<UserDto>().Generate();
            UserPetDto = new UserPetDtoFaker(PetDto, UserDto).Generate();
            PetDto.UserPets = new[] { UserPetDto };
            UserDto.UserPets = new[] { UserPetDto };
        }
        
        public void Dispose()
        {
        }
    }
}
