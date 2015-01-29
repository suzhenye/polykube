using System.Collections.Generic;
using Newtonsoft.Json;
using Xunit;

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
        public async void EnvironmentEndpointBasicFunctionality()
        {
            var client = Fixture.Server.CreateClient();
            var environmentVariables = await client.GetStringAsync("/api/environment");
            var decoded = JsonConvert.DeserializeObject<Dictionary<string, string>>(environmentVariables);
            Assert.InRange(decoded.Keys.Count, 1, 100);
        }
    }
}
