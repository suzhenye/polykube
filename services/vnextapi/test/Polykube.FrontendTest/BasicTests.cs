using System.Collections.Generic;

using Bond;
using Bond.Protocols;
using Bond.IO.Safe;
using Polykube.Common.Model;
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
        public void TestBondSerialization()
        {
            var src = new BaseThing
            {
                Name = "FooBar"
            };

            var output = new OutputBuffer();
            var writer = new CompactBinaryWriter<OutputBuffer>(output);

            // The first calls to Serialize.To and Deserialize<T>.From can take
            // a relatively long time because they generate the de/serializer 
            // for a given type and protocol.
            Serialize.To(writer, src);

            var input = new InputBuffer(output.Data);
            var reader = new CompactBinaryReader<InputBuffer>(input);

            var dst = Deserialize<BaseThing>.From(reader);

            Assert.Equal(dst.Name, "FooBar");
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
