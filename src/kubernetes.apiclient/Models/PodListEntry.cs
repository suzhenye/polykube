using System;
using System.Collections;

using RestSharp;
using RestSharp.Serializers;

namespace Kubernetes.ApiClient.Models
{
    public class PodListEntry
    {
        [SerializeAs(Name = "id")]
        public Guid Id { get; set; }
    }
}
