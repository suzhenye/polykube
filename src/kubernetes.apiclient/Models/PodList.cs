using System;
using System.Collections;
using System.Collections.Generic;

using RestSharp;
using RestSharp.Serializers;

namespace Kubernetes.ApiClient.Models
{
    public class PodList
    {
        [SerializeAs(Name = "kind")]
        public string Kind { get; set; }

        [SerializeAs(Name = "creationTimestamp")]
        public string CreationTimestamp { get; set; }

        [SerializeAs(Name = "resourceVersion")]
        public int ResourceVersion { get; set; }

        [SerializeAs(Name = "apiVersion")]
        public string ApiVersion { get; set; }

        [SerializeAs(Name = "items")]
        public IList<PodListEntry> Items { get; set; }
    }
}
