using System;

using RestSharp;

using Kubernetes.ApiClient.Models;

namespace Kubernetes.ApiClient
{
    public class KubeClient
    {
        private RestClient restClient;

        public KubeClient()
        {
            // is this actually supposed to be localhost:8080? how do we discover the kubernetes api endpt?
            this.restClient = new RestClient("http://localhost:8080/api/v1beta1/");
        }

        public PodList ListPods()
        {
            var request = new RestRequest("pods/", Method.GET);
            var response = this.restClient.Execute<PodList>(request);
            return response.Data;
        }

        public PodList ListPods(string selector)
        {
            var request = new RestRequest("pods/", Method.GET);
            request.AddParameter("labels", selector);
            var response = this.restClient.Execute<PodList>(request);
            return response.Data;
        }
    }
}
