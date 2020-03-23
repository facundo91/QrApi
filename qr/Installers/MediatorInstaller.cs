using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qrAPI.Mediators;
using qrAPI.Services;
using ServiceFactory = qrAPI.Services.ServiceFactory;

namespace qrAPI.Installers
{
    public class MediatorInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMediatR(typeof(Startup).Assembly);

            services.AddScoped(typeof(IControllerServiceMediator<>), typeof(ControllerServiceMediator<>));
            services.AddScoped<IServiceFactory, ServiceFactory>();
            services.AddScoped(typeof(IServiceDalMediator<,>), typeof(ServiceDalMediator<,>));
        }
    }
}