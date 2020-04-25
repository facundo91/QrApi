using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using qrAPI.Contracts.v1.Requests;
using qrAPI.DAL.Data.EFData.Contexts;
using qrAPI.Sdk.v1;
using Refit;
using Simple.OData.Client;

namespace qrAPI.IntegrationTests.v1
{
    public class IntegrationTest : IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IIdentityApi _identityApi;
        protected readonly HttpClient TestClient;
        protected readonly ODataClient ODataClient;

        protected IntegrationTest()
        {
            var appFactory = GetWebApplicationFactory();
            _serviceProvider = appFactory.Services;
            using var serviceScope = _serviceProvider.CreateScope();
            serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.EnsureCreated();
            TestClient = appFactory.CreateClient();
            _identityApi = RestService.For<IIdentityApi>(TestClient);
            var relativeUri = new Uri("odata/", UriKind.Relative);
            ODataClient = new ODataClient(new ODataClientSettings(TestClient, relativeUri));
        }

        private static WebApplicationFactory<Startup> GetWebApplicationFactory() =>
            IsLocalDbEnvironmentVariable()
                ? new WebApplicationFactory<Startup>()
                : new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
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

        private static bool IsLocalDbEnvironmentVariable()
        {
            var parsed = bool.TryParse(Environment.GetEnvironmentVariable("localdb"), out var localdbEnabled);
            return parsed && localdbEnabled;
        }

        protected async Task RegisterAsync(string email = "test@integration.com", string password = "SomePass1234!")
        {
            var response = await _identityApi.RegisterAsync(new UserRegistrationRequest
            {
                Email = email,
                FirstName = "first Name",
                LastName = "last Name",
                Password = password
            });
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Content.Token);
        }

        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.EnsureDeleted();
        }

    }
}
