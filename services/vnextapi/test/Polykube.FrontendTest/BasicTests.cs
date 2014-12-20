using Ploeh.AutoFixture;
using System.Collections.Generic;
using Newtonsoft.Json;
using Polykube.Frontend.Controllers;
using Xunit;

// TODO: aspnet5- Is this is okay? (Not splitting Startup into Configure/ConfigureServices anymore)
//              - if not, how does one properly get ConfigureServices to play nicely with these tests?

// TODO: aspnet5- Determine why the static routing doesn't work

namespace Polykube.FrontendTest
{
    public class BasicTests : IClassFixture<PolyFixture>
    {
        private PolyFixture Fixture;

        public BasicTests(PolyFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public void BasicAutoFixtureTest()
        {
            Fixture fixture = new Fixture();
            int expectedNumber = fixture.Create<int>();
            Assert.True(expectedNumber > 0 || expectedNumber <= 0);
        }

        [Fact]
        public async void EnvironmentEndpointBasicFunctionality()
        {
            var client = Fixture.Server.CreateClient();
            var environmentVariables = await client.GetStringAsync("/api/environment");
            var decoded = JsonConvert.DeserializeObject<Dictionary<string, string>>(environmentVariables);
            Assert.InRange(decoded.Keys.Count, 1, 100);
        }
    }
}
