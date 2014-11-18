using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;

namespace Polykube.vNextApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
	    {
            var configuration = new Configuration();
            configuration.AddEnvironmentVariables();
	        services.AddMvc(configuration);
	    }

        public void Configure(IApplicationBuilder app)
        {
            if(false)
            {
                // Not sure why I can't get static routes to work
                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "API v0 Environment",
                        template: "api/environment/{id?}",
                        defaults: new { controller = "Environment" });
                });
            }
            else
            {
                app.UseMvc();
            }

            app.UseWelcomePage();
        }
    }
}
