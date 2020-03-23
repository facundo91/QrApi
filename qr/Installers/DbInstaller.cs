using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qrAPI.Data;
using qrAPI.Data.EFData;
using qrAPI.Data.JsonData;
using qrAPI.Data.MongoData;
using qrAPI.Options;

namespace qrAPI.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var ioCOptions = new IoCOptions();
            configuration.GetSection(nameof(IoCOptions)).Bind(ioCOptions);
            switch (ioCOptions.DalImplementation)
            {
                case "json":
                    services.AddSingleton<IDataContext, JsonContext>();
                    break;
                case "ef":
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(
                            configuration.GetConnectionString("DefaultConnection")));
                    services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                        .AddEntityFrameworkStores<ApplicationDbContext>();
                    services.AddScoped<IDataContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
                    break;
                case "mongodb":
                    services.Configure<MongoOptions>(configuration.GetSection(nameof(MongoOptions)));
                    services.AddSingleton<IDataContext, MongoContext>();
                    break;
                default:
                    break;
            }
        }
    }
}