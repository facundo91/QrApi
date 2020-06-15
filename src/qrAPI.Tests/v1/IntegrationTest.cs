using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using qrAPI.Contracts.v1.Requests;
using qrAPI.DAL.Data.EFData.Contexts;
using qrAPI.Sdk.v1;
using Refit;
using Simple.OData.Client;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace qrAPI.Tests.v1
{
    public class IntegrationTest : IDisposable
    {
        private readonly IConfigurationRoot configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly IIdentityApi _identityApi;
        protected readonly HttpClient TestClient;
        protected readonly ODataClient ODataClient;

        protected IntegrationTest()
        {
            configuration = GetTestConfiguration();
            var appFactory = GetWebApplicationFactory();
            _serviceProvider = appFactory.Services;
            using var serviceScope = _serviceProvider.CreateScope();
            serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.EnsureCreated();
            TestClient = appFactory.CreateClient();
            _identityApi = RestService.For<IIdentityApi>(TestClient);
            var relativeUri = new Uri("odata/", UriKind.Relative);
            ODataClient = new ODataClient(new ODataClientSettings(TestClient, relativeUri));
        }

        private WebApplicationFactory<Startup> GetWebApplicationFactory() =>
            IsLocalDbEnvironmentVariable()
                ? new WebApplicationFactory<Startup>()
                : new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
                {
                    builder.UseConfiguration(configuration);
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
                        services.AddDbContext<ApplicationDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDB");
                        });
                    });
                });

        private static IConfigurationRoot GetTestConfiguration()
        {
            var dir = AppContext.BaseDirectory;
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .SetBasePath(dir)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .AddEnvironmentVariables();
            var qwe = builder.Build();
            return qwe;
        }

        private bool IsLocalDbEnvironmentVariable() => configuration.GetValue<bool>("localdb");

        protected async Task RegisterAsync(string email = "test@integration.com", string password = "SomePass1234!")
        {
            var response = await _identityApi.RegisterAsync(new UserRegistrationRequest
            {
                Email = email,
                FirstName = "first Name",
                LastName = "last Name",
                Password = password
            });
            TestClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", response.Content.Token);
        }

        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.EnsureDeleted();
        }

    }
}
