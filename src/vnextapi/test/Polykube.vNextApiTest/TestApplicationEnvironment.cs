using System;
using System.Runtime.Versioning;
using Microsoft.Framework.Runtime;

namespace Polykube.vNextApiTest
{
    public class TestApplicationEnvironment : IApplicationEnvironment
    {
        public string ApplicationName
        {
            get { return "Test App environment"; }
        }

        public string Version
        {
            get { return "1.0.0"; }
        }

        public string ApplicationBasePath
        {
            get { return Environment.CurrentDirectory; }
        }

        public string Configuration
        {
            get { return "Test"; }
        }

        public FrameworkName RuntimeFramework
        {
            get { return new FrameworkName(".NETFramework", new Version(4, 5)); }
        }
    }
}