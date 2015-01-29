#define THIS_WORKS 

using System;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;

namespace Polykube.Frontend
{
    public class Startup
    {
        bool initialized = false;

        public void AddServices(IServiceCollection services)
        {
            if (initialized)
            {
                throw new InvalidOperationException();
            }
            initialized = true;

            var configuration = new Configuration();
            configuration.AddEnvironmentVariables();
            services.AddMvc(configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
#if THIS_WORKS
            Action<IServiceCollection> action1 = this.AddServices;
            app.UseServices(action1);
            app.UseMvc();
#else
            // Am I missing something here? Removing the #define on line 1 results in:
            // Error CS0407
            // 'void Startup.ConfigureServices(IServiceCollection)' has the wrong return type
            // Startup.cs  31
            app.UseServices(this.AddServices);
            app.UseMvc();
#endif
        }
    }
}
