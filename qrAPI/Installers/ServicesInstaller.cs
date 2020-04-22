using System;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qrAPI.Infrastructure.Mail;
using qrAPI.Infrastructure.Mail.SendGun;
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
            services.AddTransient<IPetService, PetService>();
            var sendGunSettings = new SendGunSettings();
            configuration.GetSection($"EmailSettings:{nameof(SendGunSettings)}").Bind(sendGunSettings);
            services.AddSingleton(sendGunSettings);
            services.AddHttpClient<IMailService, SendGunMailService>(x=>
            {
                x.BaseAddress = new Uri(sendGunSettings.BaseUrl);
                x.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"api:{sendGunSettings.ApiKey}")));
            });

        }
    }
}