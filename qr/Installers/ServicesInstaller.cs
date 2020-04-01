using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qrAPI.Logic.Factory;
using qrAPI.Logic.Services;

namespace qrAPI.Installers
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddRefitClient<ITweetbookApi>()
            //    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.example.com"));
            //services.AddTransient<IQrService<Qr>, QrService>();
            //services.AddTransient<IPetService<Pet>, PetService>();
            //services.AddTransient<IMedicalRecordService, MedicalRecordService>();

            services.AddTransient<IServiceFactory, ServiceFactory>();

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<QrService>()
                .AddTransient<IQrService, QrService>(s => s.GetService<QrService>());

            services.AddTransient<PetService>()
                .AddTransient<IPetService, PetService>(s => s.GetService<PetService>());

            services.AddTransient<MedicalRecordService>()
                .AddTransient<IMedicalRecordService, MedicalRecordService>(s => s.GetService<MedicalRecordService>());
        }
    }
}