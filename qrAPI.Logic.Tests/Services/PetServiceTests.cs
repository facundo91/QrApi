//using System.Collections.Generic;
//using System.Threading.Tasks;
//using FluentAssertions;
//using Microsoft.AspNetCore.Http;
//using Moq;
//using qrAPI.Infrastructure.Extensions;
//using qrAPI.Logic.Adapters.Interfaces;
//using qrAPI.Logic.Domain;
//using qrAPI.Logic.Services.Implementations;
//using qrAPI.Logic.Services.Interfaces;
//using Xunit;

//namespace qrAPI.Logic.Tests.Services
//{
//    public class PetServiceTests : LogicTests
//    {
//        private readonly IPetService _petService;
//        private readonly Mock<IPetServiceAdapter> _serviceAdapterMock;
//        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
//        private readonly Mock<IIdentityService> _identityServiceMock;

//        public PetServiceTests()
//        {
//            _serviceAdapterMock = new Mock<IPetServiceAdapter>();
//            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
//            _identityServiceMock = new Mock<IIdentityService>();
//            _petService = new PetService(_serviceAdapterMock.Object, _httpContextAccessorMock.Object, _identityServiceMock.Object);
//        }

//        [Fact]
//        public async Task GetAllAsync_WithAPet_ReturnsThePet()
//        {
//            // Arrange
//            var pets = new List<Pet> {PampaPet};
//            _httpContextAccessorMock.Setup(x => x.HttpContext.GetUserId()).Returns(PampaPet.Owners.Id);
//            _serviceAdapterMock.Setup(x => x.GetAllByUserAsync(PampaPet.Owners.Id)).ReturnsAsync(pets);
//            // Act
//            var allPets = await _petService.GetAllAsync();
//            // Assert
//            allPets.Should().BeEquivalentTo(pets);
//        }

//        [Fact]
//        public async Task GetAllAsync_WithMoreThanAPet_ReturnsMoreThanAPet()
//        {
//            // Arrange
//            var pets = new List<Pet> { PampaPet, VulpiPet };
//            _httpContextAccessorMock.Setup(x => x.HttpContext.GetUserId()).Returns(PampaPet.Owners.Id);
//            _serviceAdapterMock.Setup(x => x.GetAllByUserAsync(PampaPet.Owners.Id)).ReturnsAsync(pets);
//            // Act
//            var allPets = await _petService.GetAllAsync();
//            // Assert
//            allPets.Should().BeEquivalentTo(pets);
//        }

//        [Fact]
//        public async Task GetAllAsync_WithoutAnyPet_ReturnsEmpty()
//        {
//            // Arrange
//            var pets = new List<Pet>();
//            _httpContextAccessorMock.Setup(x => x.HttpContext.GetUserId()).Returns(PampaPet.Owners.Id);
//            _serviceAdapterMock.Setup(x => x.GetAllByUserAsync(PampaPet.Owners.Id)).ReturnsAsync(pets);
//            // Act
//            var allPets = await _petService.GetAllAsync();
//            // Assert
//            allPets.Should().BeEquivalentTo(pets);
//        }
//    }
//}
