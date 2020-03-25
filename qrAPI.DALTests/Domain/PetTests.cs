using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using qrAPI.DAL.Dtos;
using qrAPI.Logic.Adapters;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services;
using Xunit;

namespace qrAPI.Logic.Tests.Domain
{
    public class PetTests : LogicTests
    {
        //public PetServiceTests()
        //{
        //    _serviceAdapterMock = new Mock<IServiceAdapter<Pet, PetDto>>();
        //    _petService = new PetService(_serviceAdapterMock.Object);
        //}

        [Fact]
        public void AddNewOwner_AddsNewOwner()
        {
            // Arrange
            var oldOwnersCount = PampaPet.Owners.Count();
            // Act
            PampaPet.AddNewOwner(FacundoPerson);
            var newOwnersCount = PampaPet.Owners.Count();
            // Assert
            newOwnersCount .Should().Be(oldOwnersCount + 1);
        }

        [Fact]
        public void AddNewOwner_AddsNewPetToOwner()
        {
            // Arrange
            var oldPetsCount= FacundoPerson.Pets.Count();
            // Act
            PampaPet.AddNewOwner(FacundoPerson);
            var newOwners = FacundoPerson.Pets.Count();
            // Assert
            newOwners.Should().Be(oldPetsCount + 1);
        }
    }
}
