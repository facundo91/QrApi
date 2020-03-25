using System.Linq;
using FluentAssertions;
using Xunit;

namespace qrAPI.Logic.Tests.Domain
{
    public class PersonTests : LogicTests
    {
        [Fact]
        public void AddNewPet_AddsNewPet()
        {
            // Arrange
            var oldPetsCount = FacundoPerson.Pets.Count();
            // Act
            FacundoPerson.AddOwnedPet(PampaPet);
            var newOwners = FacundoPerson.Pets.Count();
            // Assert
            newOwners.Should().Be(oldPetsCount + 1);
        }

        [Fact]
        public void AddNewPet_AddsNewOwnerToPet()
        {
            // Arrange
            var oldOwnersCount = PampaPet.Owners.Count();
            // Act
            FacundoPerson.AddOwnedPet(PampaPet);
            var newOwnersCount = PampaPet.Owners.Count();
            // Assert
            newOwnersCount.Should().Be(oldOwnersCount + 1);
        }
    }
}
