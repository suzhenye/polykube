using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;

namespace Polykube.vNextApi
{
    public class Startup
    {
        /*
        public IServiceCollection ConfigureServices(IServiceCollection services)
        {
            var configuration = new Configuration();
            configuration.AddEnvironmentVariables();
            services.AddMvc(configuration);

            return services;

            // Can't make this work with tests... grah
        }
        */

        public void Configure(IApplicationBuilder app)
        {
            app.UseServices(services => {
                var configuration = new Configuration();
                configuration.AddEnvironmentVariables();
                services.AddMvc(configuration);
            });

            app.UseMvc(routes =>
            {
                // This doesn't work
                routes.MapRoute(
                    name: "API v0 Static",
                    template: "api/static",
                    defaults: new { controller = "Static" });
            });
        }
    }
}
