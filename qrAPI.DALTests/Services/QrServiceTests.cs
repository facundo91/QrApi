using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using qrAPI.Domain;
using qrAPI.Services;
using Xunit;

namespace qrAPI.UnitTests.Services
{
    public class QrServiceTests
    {

        private readonly Mock<IMediator> _mediatorMock;
        private readonly IQrService _qrService;
        public QrServiceTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _qrService = new QrService(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllQrsAsync_WithoutQrs_ReturnsEmptyList()
        {
            //Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetQrsAsyncQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Qr>());
            //Act
            //Assert
            (await _qrService.GetQrsAsync()).Should().BeEmpty();
        }

        [Fact]
        public async Task CreateQrsAsync_WithoutQrs_ReturnsEmptyList()
        {
            //Arrange
            var qr = new Qr { Name = "Test Qr" };
            _mediatorMock.Setup(m => 
                    m.Send(It.IsAny<CreateQrAsyncCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Qr());
            //Act
            //Assert
            (await _qrService.CreateQrAsync(qr)).Should().Be(true);
        }

    }
}
