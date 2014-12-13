using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.TestHost;
using Microsoft.Framework.Runtime.Infrastructure;
using Polykube.vNextApi;

namespace Polykube.vNextApiTest
{
    public class PolyFixture
    {
        public TestServer Server;

        public PolyFixture()
        {
            var startup = new Startup();

            var serviceProvider = CallContextServiceLocator.Locator.ServiceProvider;
            Action<IApplicationBuilder> app = startup.Configure;

            Server = TestServer.Create(serviceProvider, app);
        }
    }
}
