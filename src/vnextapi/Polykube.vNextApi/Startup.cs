using System;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;

namespace Agora.Api
{
    public class Startup
    {
	public void ConfigureServices(IServiceCollection services)
	{
	     services.AddMvc();
	}

        public void Configure(IApplicationBuilder app)
        {
    	    app.UseMvc();
        }

	    /*
            app.Use(async (context, next) =>
            {
                try
                {
                    // TODO not a wise thing to do:
                    context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
                    await next();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });
	    */

	    /*
            app.UseMvc(routes =>
            {
                routes.MapRoute( 
                    "api_route",
                    "",
                    defaults: new
                    {
                        controller = "Example"
                    });
            });
	    */
    }
}
