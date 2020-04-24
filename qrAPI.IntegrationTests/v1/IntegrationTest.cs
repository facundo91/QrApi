using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using qrAPI.Contracts.v1.Requests;
using qrAPI.Contracts.v1.Responses;
using qrAPI.DAL.Data.EFData.Contexts;
using qrAPI.Sdk.v1;
using Refit;
using Simple.OData.Client;

namespace qrAPI.IntegrationTests.v1
{
    public class IntegrationTest : IDisposable
    {
        protected readonly IQrsApi QrsApi;
        protected readonly IPetsApi PetsApi;
        protected readonly IBoundClient<PetResponse> PetsOdataClient;
        protected readonly IBoundClient<QrResponse> QrsOdataClient;
        private readonly IServiceProvider _serviceProvider;
        private readonly IIdentityApi _identityApi;
        private readonly HttpClient _testClient;

        protected IntegrationTest()
        {
            var appFactory = GetWebApplicationFactory();
            _serviceProvider = appFactory.Services;
            using var serviceScope = _serviceProvider.CreateScope();
            serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.EnsureCreated();
            _testClient = appFactory.CreateClient();


            _identityApi = RestService.For<IIdentityApi>(_testClient);
            QrsApi = RestService.For<IQrsApi>(_testClient);
            PetsApi = RestService.For<IPetsApi>(_testClient);


            var relativeUri = new Uri("odata/", UriKind.Relative);
            var oDataClient = new ODataClient(new ODataClientSettings(_testClient, relativeUri));
            PetsOdataClient = oDataClient.For<PetResponse>("Pets");
            QrsOdataClient = oDataClient.For<QrResponse>("Qrs");
        }

        private static WebApplicationFactory<Startup> GetWebApplicationFactory() =>
            GetLocalDbEnvironmentVariable()
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

        private static bool GetLocalDbEnvironmentVariable()
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
            _testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Content.Token);
        }

        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.EnsureDeleted();
        }

    }
}
