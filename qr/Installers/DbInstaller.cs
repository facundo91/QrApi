using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qrAPI.DAL.Data;
using qrAPI.DAL.Data.EFData;
using qrAPI.DAL.Data.JsonData;
using qrAPI.DAL.Data.MongoData;
using qrAPI.DAL.Options;
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
                    InstallJsonDb(services);
                    break;
                case "ef":
                    InstallEfDb(services, configuration);
                    break;
                case "mongodb":
                    InstallMongoDb(services, configuration);
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private static void InstallJsonDb(IServiceCollection services)
        {
            services.AddSingleton<IDataContext, JsonContext>();
        }

        private static void InstallMongoDb(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoOptions>(configuration.GetSection(nameof(MongoOptions)));
            services.AddSingleton<IDataContext, MongoContext>();
        }

        private static void InstallEfDb(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<IDataContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        }
    }
}