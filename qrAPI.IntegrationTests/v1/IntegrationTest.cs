using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using qrAPI.Contracts.v1;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Contracts.v1.Responses;
using qrAPI.DAL.Data.EFData;

namespace qrAPI.IntegrationTests.v1
{
    public class IntegrationTest : IDisposable
    {
        protected readonly HttpClient TestClient;
        private readonly IServiceProvider _serviceProvider;

        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
                        services.AddDbContext<ApplicationDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDB");
                        });
                    });
                });
            _serviceProvider = appFactory.Services;
            TestClient = appFactory.CreateClient();
        }

        protected async Task<QrResponse> CreateQrAsync(CreateQrRequest request)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Qrs.Create, request);
            return await response.Content.ReadAsAsync<QrResponse>();
        }

        protected async Task<PetResponse> CreatePetAsync(CreatePetRequest request)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Pets.Create, request);
            return await response.Content.ReadAsAsync<PetResponse>();
        }

        protected async Task<List<QrResponse>> GetAllQrs()
        {
            return (await TestClient.GetAsync(ApiRoutes.Qrs.GetAll)
                    .ConfigureAwait(false)).Content
                .ReadAsAsync<List<QrResponse>>().Result;
        }

        protected async Task<List<PetResponse>> GetAllPets()
        {
            return (await TestClient.GetAsync(ApiRoutes.Pets.GetAll)
                    .ConfigureAwait(false)).Content
                .ReadAsAsync<List<PetResponse>>().Result;
        }

        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureDeleted();
        }

    }
}
