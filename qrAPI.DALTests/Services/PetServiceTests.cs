using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using qrAPI.DAL.Dtos;
using qrAPI.Logic.Adapters;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services;
using Xunit;

namespace qrAPI.Logic.Tests.Services
{
    public class PetServiceTests : LogicTests
    {
        private IPetService _petService;
        private readonly Mock<IServiceAdapter<Pet, PetDto>> _serviceAdapterMock;

        public PetServiceTests()
        {
            _serviceAdapterMock = new Mock<IServiceAdapter<Pet, PetDto>>();
            _petService = new PetService(_serviceAdapterMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_WithAPet_ReturnsThePet()
        {
            // Arrange
            var pets = new List<Pet> {PampaPet};
            _serviceAdapterMock.Setup(x => x.GetAllAsync()).ReturnsAsync(pets);
            // Act
            var allPets = await _petService.GetAllAsync();
            // Assert
            allPets.Should().BeEquivalentTo(pets);
        }

        [Fact]
        public async Task GetAllAsync_WithMoreThanAPet_ReturnsMoreThanAPet()
        {
            // Arrange
            var pets = new List<Pet> { PampaPet, VulpiPet };
            _serviceAdapterMock.Setup(x => x.GetAllAsync()).ReturnsAsync(pets);
            // Act
            var allPets = await _petService.GetAllAsync();
            // Assert
            allPets.Should().BeEquivalentTo(pets);
        }

        [Fact]
        public async Task GetAllAsync_WithoutAnyPet_ReturnsEmpty()
        {
            // Arrange
            var pets = new List<Pet>();
            _serviceAdapterMock.Setup(x => x.GetAllAsync()).ReturnsAsync(pets);
            // Act
            var allPets = await _petService.GetAllAsync();
            // Assert
            allPets.Should().BeEquivalentTo(pets);
        }
    }
}
