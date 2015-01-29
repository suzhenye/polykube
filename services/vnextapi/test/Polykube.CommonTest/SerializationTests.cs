using Bond;
using Bond.Protocols;
using Bond.IO.Safe;
using Polykube.Common.Model;
using Xunit;

namespace Polykube.CommonTest
{
    public class SerializationTests
    {

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
    }
}