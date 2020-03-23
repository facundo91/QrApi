using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qrAPI.Logic.Mediators;
using qrAPI.Logic.Services;
using qrAPI.Mediators;
using ServiceFactory = qrAPI.Logic.Services.ServiceFactory;

namespace qrAPI.Installers
{
    public class MediatorInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IControllerServiceMediator<>), typeof(ControllerServiceMediator<>));
            services.AddScoped<IServiceFactory, ServiceFactory>();
            services.AddScoped(typeof(IServiceDalMediator<,>), typeof(ServiceDalMediator<,>));
        }
    }
}