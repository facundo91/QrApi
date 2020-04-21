using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using qrAPI.Contracts.v1.Fakers.Requests.Create;
using Xunit;

namespace qrAPI.IntegrationTests.v1
{
    [Collection("Sequential")]
    public class QrsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAllQrs_WithoutQrs_ReturnsEmptyResponse()
        {
            //Arrange
            //Act
            var response = await QrsApi.GetAllAsync();
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Should().BeEmpty();
        }


        [Fact]
        public async Task GetAllQrs_FromSdk()
        {
            //Arrange
            //Act
            var response = await QrsApi.GetAllAsync();
            //Assert
            response.Content.Should().BeEmpty();
        }

        [Fact]
        public async Task CreateQr_FromSdk()
        {
            //Arrange
            var createQrRequest = new CreateQrRequestFaker().Generate();
            //Act
            var response = await QrsApi.CreateAsync(createQrRequest);
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
            var response = await QrsApi.CreateAsync(createQrRequest);
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
            var qrs = await QrsOdataClient.FindEntriesAsync();
            //Assert
            qrs.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetAllQrs_WithOneQr_ReturnsQr()
        {
            //Arrange
            var createQrRequest = new CreateQrRequestFaker().Generate();
            await QrsApi.CreateAsync(createQrRequest);
            //Act
            var qrs = await QrsOdataClient.FindEntriesAsync();
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
            await QrsApi.CreateAsync(createQrRequest1);
            await QrsApi.CreateAsync(createQrRequest2);
            //Act
            var qrs = await QrsOdataClient.OrderBy(qrOrder => qrOrder.Name).FindEntriesAsync();
            //Assert
            var qrResponses = qrs.ToList();
            qrResponses.Should().HaveCount(2);
            qrResponses.ElementAt(0).Name.Should().BeEquivalentTo(createQrRequest2.Name);
            qrResponses.ElementAt(1).Name.Should().BeEquivalentTo(createQrRequest1.Name);
        }

    }
}
