using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qrAPI.App.Domain;
using qrAPI.App.Domain.Validators;
using qrAPI.App.Services.Implementations;
using qrAPI.App.Services.Interfaces;
using qrAPI.App.Services.Validators;

namespace qrAPI.Installers
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IQrService, QrService>();
            services.AddTransient(typeof(IGenericService<Pet>), typeof(PetService));
            services.AddSingleton(typeof(AbstractValidator<Pet>), typeof(PetValidator));
            services.AddSingleton(typeof(AbstractValidator<Qr>), typeof(QrValidator));
            services.Decorate(typeof(IGenericService<>), typeof(AbstractGenericServiceValidator<>));
        }
    }
}