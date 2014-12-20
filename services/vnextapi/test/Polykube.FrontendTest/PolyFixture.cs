using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.TestHost;
using Microsoft.Framework.Runtime.Infrastructure;
using Polykube.Frontend;

namespace Polykube.FrontendTest
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
