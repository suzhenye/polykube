using System;
using etcetera;

namespace Agora.Config
{
    // TODO(colemick): maybe this should just turn into Agora.Discovery with an .NET api for etcd discovery
    public static class EtcdSettings
    {
        public static string TestString
        {
            get
            {
                return "This is a test string.";
            }
        }

        private static EtcdClient client;

        public static EtcdClient Client
        {
            get
            {
                var etcdMasterUrl = "http://localhost:4001/v2/keys/";

                if (client == null)
                {
                    client = new EtcdClient(new Uri(etcdMasterUrl));
                }

                return client;
            }
        }
    }
}