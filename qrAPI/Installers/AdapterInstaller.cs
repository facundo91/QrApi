using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qrAPI.Logic.Adapters.Implementations;
using qrAPI.Logic.Adapters.Interfaces;
using qrAPI.Presentation.Adapters.v1.Implementations;
using qrAPI.Presentation.Adapters.v1.Interfaces;

namespace qrAPI.Installers
{
    public class AdapterInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            AddControllerAdapters(services);
            AddServiceAdapters(services);
        }

        private static void AddControllerAdapters(IServiceCollection services)
        {
            services.AddTransient<IMedicalRecordsControllerAdapter, MedicalRecordsControllerAdapter>();
            services.AddTransient<IPetsControllerAdapter, PetsControllerAdapter>();
            services.AddTransient<IQrsControllerAdapter, QrsControllerAdapter>();
        }

        private static void AddServiceAdapters(IServiceCollection services)
        {
            services.AddTransient<IMedicalRecordServiceAdapter, MedicalRecordServiceAdapter>();
            services.AddTransient<IPetServiceAdapter, PetServiceAdapter>();
            services.AddTransient<IQrServiceAdapter, QrServiceAdapter>();
        }
    }
}