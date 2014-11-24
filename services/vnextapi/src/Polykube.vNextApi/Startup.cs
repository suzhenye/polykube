using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;

namespace Polykube.vNextApi
{
    public class Startup
    {
        private static bool UseStaticRoutes = false;

        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new Configuration();
            configuration.AddEnvironmentVariables();
            services.AddMvc(configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
            if(UseStaticRoutes)
            {
                app.UseMvc(routes =>
                {
                    // This doesn't work
                    routes.MapRoute(
                        name: "API v0 Environment",
                        template: "api/environment",
                        defaults: new { controller = "Environment" });

                    /*
                    This works, but only because of the annotation on the ExampleController class.
                    
                    routes.MapRoute(
                        name: "API v0 Example",
                        template: "api/example",
                        defaults: new { controller = "Example" });
                    */
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
