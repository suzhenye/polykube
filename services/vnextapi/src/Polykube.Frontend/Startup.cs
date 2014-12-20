#define THIS_WORKS

using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;

namespace Polykube.Frontend
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

#if THIS_WORKS
            Action<IServiceCollection> action1 = this.ConfigureServices;
            app.UseServices(action1);
#else
            // Am I missing something here?
            app.UseServices(this.ConfigureServices);
#endif

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
