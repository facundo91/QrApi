using System.Linq;
using FluentAssertions;
using Xunit;

namespace qrAPI.Logic.Tests.Domain
{
    public class PetTests : LogicTests
    {
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
