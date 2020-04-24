using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qrAPI.Infrastructure.Adapters;

namespace qrAPI.Installers
{
    public class AdapterInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMapperAdapter, MapperAdapter>();
        }

    }
}