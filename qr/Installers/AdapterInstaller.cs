using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qrAPI.Adapters;
using qrAPI.Logic.Adapters;

namespace qrAPI.Installers
{
    public class AdapterInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IControllerAdapter<>), typeof(ControllerAdapter<>));
            services.AddTransient<IMedicalRecordsControllerAdapter, MedicalRecordsControllerAdapter>();

            services.AddTransient(typeof(IServiceAdapter<,>), typeof(ServiceAdapter<,>));
            services.AddTransient<IMedicalRecordServiceAdapter, MedicalRecordServiceAdapter>();
        }
    }
}