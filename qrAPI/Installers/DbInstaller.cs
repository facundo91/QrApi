using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qrAPI.DAL.Daos.CacheImplementations;
using qrAPI.DAL.Daos.EfImplementations;
using qrAPI.DAL.Daos.Interfaces;
using qrAPI.DAL.Daos.MongoImplementations;
using qrAPI.DAL.Data.EFData.Contexts;
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            var mongoOptions = new MongoOptions();
            configuration.GetSection(nameof(MongoOptions)).Bind(mongoOptions);
            services.AddSingleton(mongoOptions);

            services.AddTransient(typeof(IRepository<QrDto>), typeof(QrEfRepository));
            services.AddTransient<IRefreshTokenRepository, RefreshTokenEfRepository>();
            services.AddTransient<IPetRepository, PetMongoRepository>();

            var redisCacheSettings = new MemoryCacheSettings();
            configuration.GetSection(nameof(MemoryCacheSettings)).Bind(redisCacheSettings);
            if (redisCacheSettings.Enabled)
            {
                services.Decorate<IPetRepository, PetCacheRepository>();
                services.Decorate(typeof(IRepository<QrDto>), typeof(CacheRepository<QrDto>));
            }
        }

        private static void InstallEfDb(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.AddTransient(typeof(IRepository<QrDto>), typeof(QrEfRepository));
            services.AddTransient<IRefreshTokenRepository, RefreshTokenEfRepository>();
            services.AddTransient<IPetRepository, PetEfRepository>();
            services.AddTransient<IUserPetRepository, UserPetRepository>();

            var redisCacheSettings = new MemoryCacheSettings();
            configuration.GetSection(nameof(MemoryCacheSettings)).Bind(redisCacheSettings);
            if (redisCacheSettings.Enabled)
            {
                services.Decorate<IPetRepository, PetCacheRepository>();
                services.Decorate(typeof(IRepository<QrDto>), typeof(CacheRepository<QrDto>));
            }
        }
    }
}