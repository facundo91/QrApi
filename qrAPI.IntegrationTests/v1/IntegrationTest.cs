using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using qrAPI.Contracts.v1;
using qrAPI.Contracts.v1.Requests;
using qrAPI.Contracts.v1.Requests.Create;
using qrAPI.Contracts.v1.Responses;
using qrAPI.DAL.Data.EFData.Contexts;

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
        protected async Task<PetResponse> CreatePetAsync(CreatePetRequest request)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Pets.Create, request);
            return await response.Content.ReadAsAsync<PetResponse>();
        }

        protected async Task<List<PetResponse>> GetAllPetsAsync() =>
            (await TestClient.GetAsync(ApiRoutes.Pets.GetAll))
            .Content.ReadAsAsync<List<PetResponse>>().Result;

        protected async Task<PetResponse> GetPetAsync(Guid id)
        {
            var response = (await TestClient.GetAsync(ApiRoutes.Pets.Get.Replace("{petId}", id.ToString())));
            return await response.Content.ReadAsAsync<PetResponse>();
        }

        protected async Task AuthenticateAsync(string email = "test@integration.com", string password = "SomePass1234!")
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(email, password));
        }

        private async Task<string> GetJwtAsync(string email, string password)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Identity.Register, new UserRegistrationRequest
            {
                Email = email,
                Password = password
            });

            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }

        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureDeleted();
        }

    }
}
