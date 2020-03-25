using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qrAPI.DAL.Data.EFData;
using qrAPI.HealthChecks;
using qrAPI.Options;

namespace qrAPI.Installers
{
    public class HealthChecksInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var ioCOptions = new IoCOptions();
            configuration.GetSection(nameof(IoCOptions)).Bind(ioCOptions);

            switch (ioCOptions.DalImplementation)
            {
                case "ef":
                    services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
                    break;
                default:
                    services.AddHealthChecks().AddCheck<DataBaseHealthCheck>(ioCOptions.DalImplementation);
                    break;
            }
        }
    }
}