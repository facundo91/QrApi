using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.FeatureFilters;

namespace qrAPI.Installers
{
    public class FeatureManagementInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddFeatureManagement(configuration.GetSection("FeatureManagement"))
                .AddFeatureFilter<PercentageFilter>();
        }
    }
}
