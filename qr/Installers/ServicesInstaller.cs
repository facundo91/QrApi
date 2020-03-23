using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qrAPI.Domain;
using qrAPI.Services;

namespace qrAPI.Installers
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IQrService<Qr>, QrService>();
            services.AddTransient<IPetService<Pet>, PetService>();
        }
    }
}