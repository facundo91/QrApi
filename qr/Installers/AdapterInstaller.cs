using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qrAPI.Logic.Adapters;

namespace qrAPI.Installers
{
    public class AdapterInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(Presentation.Adapters.v1.IControllerAdapter<>), typeof(Presentation.Adapters.v1.ControllerAdapter<>));
            services.AddTransient<Presentation.Adapters.v1.IMedicalRecordsControllerAdapter, Presentation.Adapters.v1.MedicalRecordsControllerAdapter>();

            services.AddTransient(typeof(Presentation.Adapters.v2.IControllerAdapter<>), typeof(Presentation.Adapters.v2.ControllerAdapter<>));
            services.AddTransient<Presentation.Adapters.v2.IMedicalRecordsControllerAdapter, Presentation.Adapters.v2.MedicalRecordsControllerAdapter>();

            services.AddTransient(typeof(IServiceAdapter<>), typeof(ServiceAdapter<>));
            services.AddTransient<IMedicalRecordServiceAdapter, MedicalRecordServiceAdapter>();
        }
    }
}