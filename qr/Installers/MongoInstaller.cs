using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qr.MongoData;
using qr.Options;
using qr.Services;

namespace qr.Installers
{
    public class MongoInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // Example #3: Suboptions
            // Bind options using a sub-section of the appsettings.json file.
            services.Configure<MongoOptions>(configuration.GetSection(nameof(MongoOptions)));
            services.AddScoped<IMongoContext, MongoContext>();
            services.AddScoped<IQrService, MongoQrService>();
        }
    }
}
