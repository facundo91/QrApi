using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using qrAPI.Contracts.v1;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Contracts.v1.Requests.Update;
using qrAPI.Contracts.v1.Responses;
using Xunit;

namespace qrAPI.IntegrationTests.v1
{
    public class QrsControllerTests : QrsControllerFixture
    {
        [Fact]
        public async Task GetAllQrs_WithoutQrs_ReturnsEmptyResponse()
        {
            //Arrange
            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Qrs.GetAll);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<QrResponse>>()).Should().BeEmpty();
        }

        [Fact]
        public async Task CreateQr_AddsOneItem()
        {
            //Arrange
            var createQrRequest = new CreateQrRequest { Name = "Test QR" };
            //Act
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Qrs.Create, createQrRequest);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var qrCreated = await response.Content.ReadAsAsync<QrResponse>();
            qrCreated.Name.Should().Be(createQrRequest.Name);
            qrCreated.Id.Should().NotBeEmpty();
        }

    }

    public abstract class  QrsControllerFixture : IntegrationTest
    {
        protected async Task<QrResponse> CreateQrAsync(CreateQrRequest request)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Qrs.Create, request);
            return await response.Content.ReadAsAsync<QrResponse>();
        }

        protected async Task<List<QrResponse>> GetAllQrsAsync() =>
            (await TestClient.GetAsync(ApiRoutes.Qrs.GetAll))
            .Content.ReadAsAsync<List<QrResponse>>().Result;

        protected async Task<QrResponse> GetQrAsync(Guid id) =>
            (await TestClient.GetAsync(ApiRoutes.Qrs.Get.Replace("{qrId}",id.ToString())))
            .Content.ReadAsAsync<QrResponse>().Result;

        protected async Task<bool> UpdateQrAsync(Guid id,UpdateQrRequest updateQrRequest) =>
            (await TestClient.PutAsJsonAsync(ApiRoutes.Qrs.Update.Replace("{qrId}", id.ToString()), updateQrRequest))
            .Content.ReadAsAsync<bool>().Result;

        protected async Task<bool> DeleteQrAsync(Guid id) =>
            (await TestClient.DeleteAsync(ApiRoutes.Qrs.Delete.Replace("{qrId}", id.ToString())))
            .Content.ReadAsAsync<bool>().Result;
    }
}
