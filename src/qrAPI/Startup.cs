using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using qrAPI.Contracts.v1.Responses;
using qrAPI.Installers;
using static Microsoft.OData.ODataUrlKeyDelimiter;

namespace qrAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallServicesInAssembly(Configuration);
        }

        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider, IWebHostEnvironment env)
        {
            if (IsTestEnvironment(env))
            {
                app.UseDeveloperExceptionPage();
            } else if (env.IsProduction())
            {
                app.UseHttpsRedirection();
            }
            app.AddHealthChecks();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseResponseCaching();

            app.UseEndpoints(endpoints =>
            {
                // the following will not work as expected
                // BUG: https://github.com/OData/WebApi/issues/1837
                // routeBuilder.SetDefaultODataOptions( new ODataOptions() { UrlKeyDelimiter = Parentheses } );
                endpoints.ServiceProvider.GetRequiredService<ODataOptions>().UrlKeyDelimiter = Parentheses;
                endpoints.MapControllers();
                endpoints.EnableDependencyInjection();
                endpoints.Select().Filter().OrderBy().Count().Expand().MaxTop(10);
                endpoints.MapODataRoute("odata", "odata", GetEdmModel());
            });
            app.AddSwagger(provider);
        }

        private static bool IsTestEnvironment(IHostEnvironment env) => env.IsDevelopment() || env.IsEnvironment("Docker");

        private IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<PetResponse>("Pets").EntityType.HasKey(o => o.Id);
            odataBuilder.EntitySet<QrResponse>("Qrs").EntityType.HasKey(o => o.Id);

            return odataBuilder.GetEdmModel();
        }
    }
}