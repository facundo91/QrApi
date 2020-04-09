using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qrAPI.DAL.Daos.CacheImplementations;
using qrAPI.DAL.Daos.EfImplementations;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Data;
using qrAPI.DAL.Data.EFData.Contexts;
using qrAPI.DAL.Data.MongoData;
using qrAPI.DAL.Dtos;
using qrAPI.DAL.Options;
using qrAPI.Infrastructure.Options;
using qrAPI.Infrastructure.Settings;

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

        private static void InstallMongoDb(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoOptions>(configuration.GetSection(nameof(MongoOptions)));
            services.AddSingleton<IDataContext, MongoContext>();
        }

        private static void InstallEfDb(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.AddTransient(typeof(IRepository<MedicalRecordDto>), typeof(EfRepository<MedicalRecordDto>));
            services.AddTransient(typeof(IRepository<QrDto>), typeof(EfRepository<QrDto>));
            services.AddTransient<IRefreshTokenRepository, RefreshTokenEfRepository>();
            services.AddTransient<IPetRepository, PetEfRepository>();

            var redisCacheSettings = new MemoryCacheSettings();
            configuration.GetSection(nameof(MemoryCacheSettings)).Bind(redisCacheSettings);
            if (redisCacheSettings.Enabled)
            {
                services.Decorate<IPetRepository, PetCacheRepository>();
            }
        }
    }
}