using FluentAssertions;
using qrAPI.Contracts.v1.Fakers.Requests.Create;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Sdk.v1;
using Refit;
using Simple.OData.Client;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace qrAPI.Tests.v1
{
    [Collection("Sequential")]
    public class QrsControllerTests : IntegrationTest
    {
        private readonly IQrsApi _qrsApi;
        private readonly IBoundClient<QrResponse> _qrsOdataClient;

        public QrsControllerTests()
        {
            _qrsApi = RestService.For<IQrsApi>(TestClient);
            _qrsOdataClient = ODataClient.For<QrResponse>("Qrs");
        }

        [Fact]
        public async Task GetAllQrs_WithoutQrs_ReturnsEmptyResponse()
        {
            //Arrange
            //Act
            var response = await _qrsApi.GetAllAsync();
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().BeEmpty();
        }


        [Fact]
        public async Task GetAllQrs_FromSdk()
        {
            //Arrange
            //Act
            var response = await _qrsApi.GetAllAsync();
            //Assert
            response.Content.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateQr_FromSdk()
        {
            //Arrange
            var createQrRequest = new CreateQrRequestFaker().Generate();
            //Act
            var response = await _qrsApi.CreateAsync(createQrRequest);
            //Assert
            response.Content.Name.Should().BeEquivalentTo(createQrRequest.Name);
            response.Content.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreateQr_AddsOneItem()
        {
            //Arrange
            var createQrRequest = new CreateQrRequestFaker().Generate();
            //Act
            var response = await _qrsApi.CreateAsync(createQrRequest);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var qrCreated = response.Content;
            qrCreated.Name.Should().Be(createQrRequest.Name);
            qrCreated.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetAllQrs_WithoutAny_ReturnsEmpty()
        {
            //Arrange
            //Act
            var qrs = await _qrsOdataClient.FindEntriesAsync();
            //Assert
            qrs.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetAllQrs_WithOneQr_ReturnsQr()
        {
            //Arrange
            var createQrRequest = new CreateQrRequestFaker().Generate();
            await _qrsApi.CreateAsync(createQrRequest);
            //Act
            var qrs = await _qrsOdataClient.FindEntriesAsync();
            //Assert
            var qrResponses = qrs.ToList();
            qrResponses.Should().HaveCount(1);
            qrResponses.First().Name.Should().BeEquivalentTo(createQrRequest.Name);
        }

        [Fact]
        public async Task GetAllQrs_WithTwoQrs_OrderByName_ReturnsQrsOrderedByName()
        {
            //Arrange
            var createQrRequest1 = new CreateQrRequestFaker().Generate();
            createQrRequest1.Name = "Second";
            var createQrRequest2 = new CreateQrRequestFaker().Generate();
            createQrRequest2.Name = "First";
            await _qrsApi.CreateAsync(createQrRequest1);
            await _qrsApi.CreateAsync(createQrRequest2);
            //Act
            var qrs = await _qrsOdataClient.OrderBy(qrOrder => qrOrder.Name).FindEntriesAsync();
            //Assert
            var qrResponses = qrs.ToList();
            qrResponses.Should().HaveCount(2);
            qrResponses.ElementAt(0).Name.Should().BeEquivalentTo(createQrRequest2.Name);
            qrResponses.ElementAt(1).Name.Should().BeEquivalentTo(createQrRequest1.Name);
        }

    }
}
