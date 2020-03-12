using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qr.EFData;

namespace qr.Installers
{
    public class HealthChecksInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();
        }
    }
}
