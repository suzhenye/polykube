using Xunit;

// https://github.com/bricelam/EntityFramework/commit/e3d7962ca8a11b1b54efe242c0a3791b70aa4edc

namespace Polykube.vNextApiTest
{   
    public class BasicTests
    {
        [Fact]
        public void TestTruth()
        {
            Assert.True(true);
        }

        [Fact(Skip = "Doesn't work")]
        public async void TestEnvironmentEndpoint()
        {
            Assert.NotNull(null);
        }
    }
}
