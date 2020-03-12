using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace qr.Installers
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}