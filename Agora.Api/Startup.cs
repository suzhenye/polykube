using System;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

using Agora.Config;

namespace Agora.Api
{
    public class Startup
    {
        public void Configure(IBuilder app)
        {
            app.UseServices(services =>
            {
                services.AddMvc();
            });

            app.Use(async (context, next) =>
            {
                Console.WriteLine(context.Request.Path);

                Console.WriteLine(EtcdSettings.TestString);

                var etcdClient = EtcdSettings.Client;

                try
                {
                    await next();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });

            app.UseMvc();
        }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return new EmptyResult();
        }
    }
}