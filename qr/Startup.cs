using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using qrAPI.Installers;
using qrAPI.Presentation.Controllers.v1;

namespace qrAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().AddApiExplorer();
            //services.AddControllersWithViews();
            services.AddRazorPages();
            services.InstallServicesInAssembly(Configuration);

            //var assembly = typeof(qrAPI.Presentation).Assembly;
            //services.AddControllersWithViews()
            //    .AddApplicationPart(assembly);

            //services.AddControllers().

            //var controllers = typeof(Startup).Assembly.ExportedTypes
            //    .Where(x => typeof(ControllerBase).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            //    .Select(x => x.Assembly).ToList();
            //controllers.ForEach(controller =>   controller.InstallServices(services, configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.AddHealthChecks();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.AddSwagger(Configuration);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}