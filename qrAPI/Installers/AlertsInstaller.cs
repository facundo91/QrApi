using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qrAPI.Infra.Mail;
using qrAPI.Infra.Mail.SendGrid;
using qrAPI.Infra.Mail.SendGun;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace qrAPI.Installers
{
    public class AlertsInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            InjectSendGrid(services, configuration);
            InjectSendGun(services, configuration);
        }

        private static void InjectSendGun(IServiceCollection services, IConfiguration configuration)
        {
            var sendGunSettings = new SendGunSettings();
            configuration.GetSection($"EmailSettings:{nameof(SendGunSettings)}").Bind(sendGunSettings);
            if (!sendGunSettings.Enabled) return;
            services.AddSingleton(sendGunSettings);
            services.AddHttpClient<IMailBroker, SendGunMailBroker>(x =>
            {
                x.BaseAddress = new Uri(sendGunSettings.BaseUrl);
                x.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"api:{sendGunSettings.ApiKey}")));
            });
        }

        private static void InjectSendGrid(IServiceCollection services, IConfiguration configuration)
        {
            var sendGridSettings = new SendGridSettings();
            configuration.GetSection($"EmailSettings:{nameof(SendGridSettings)}").Bind(sendGridSettings);
            if (!sendGridSettings.Enabled) return;
            services.AddSingleton(sendGridSettings);
            services.AddScoped<IMailBroker, SendGridMailBroker>();
        }
    }
}
