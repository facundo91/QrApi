using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qrAPI.Logic.Domain;
using qrAPI.Logic.Services.Implementations;
using qrAPI.Logic.Services.Interfaces;

namespace qrAPI.Installers
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IQrService, QrService>();
            services.AddTransient(typeof(IGenericService<Pet>), typeof(PetService));
        }
    }
}