using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using qrAPI.Contracts.v1;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Contracts.v1.Responses;
using Xunit;

namespace qrAPI.IntegrationTests
{
    public class QrsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutQrs_ReturnsEmptyResponse()
        {
            //Arrange

            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Qrs.GetAll);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<QrResponse>>()).Should().BeEmpty();
        }

        [Fact]
        public async Task CreateQr_AddsOneItemToTable()
        {
            //Arrange
            var getAllBefore = (await GetAllQrs()).Count;
            var createQrRequest = new CreateQrRequest { Name = "Test QR" };
            //Act
            await CreateQrAsync(createQrRequest);
            var getAllAfter = (await GetAllQrs()).Count;
            //Assert
            (getAllAfter - getAllBefore).Should().Be(1);
        }

        [Fact]
        public async Task CreateQr_ReturnsQrResponse()
        {
            //Arrange
            const string qrName = "Test QR";
            var createQrRequest = new CreateQrRequest { Name = qrName };
            //Act
            var qrResponse = await CreateQrAsync(createQrRequest);
            //Assert
            qrResponse.Name.Should().Be(qrName);
        }

    }
}
