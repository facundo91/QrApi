using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using qrAPI.Installers;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using FluentValidation.AspNetCore;
using qrAPI.Presentation.Filters;
using static Microsoft.AspNetCore.Mvc.CompatibilityVersion;
using static Microsoft.OData.ODataUrlKeyDelimiter;

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
            // the sample application always uses the latest version, but you may want an explicit version such as Version_2_2
            // note: Endpoint Routing is enabled by default; however, it is unsupported by OData and MUST be false
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add<ValidationFilter>();
            })
                .AddFluentValidation(mvcConfiguration => mvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(Latest);
            services.InstallServicesInAssembly(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, VersionedODataModelBuilder modelBuilder, IApiVersionDescriptionProvider provider)
        {
            app.AddHealthChecks();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseResponseCaching();
            app.UseMvc(
                routeBuilder =>
                {
                    // the following will not work as expected
                    // BUG: https://github.com/OData/WebApi/issues/1837
                    // routeBuilder.SetDefaultODataOptions( new ODataOptions() { UrlKeyDelimiter = Parentheses } );
                    routeBuilder.ServiceProvider.GetRequiredService<ODataOptions>().UrlKeyDelimiter = Parentheses;

                    // global odata query options
                    routeBuilder.Select().Filter().OrderBy().MaxTop(10).Count();

                    routeBuilder.MapVersionedODataRoutes("odata", "api", modelBuilder.GetEdmModels());
                });
            app.AddSwagger(Configuration, provider);
        }
    }
}