using System;
using etcetera;
using RestSharp;

namespace Kubernetes.ApiClient
{
    private RestClient restClient;

    public class Client
    {
        public Client()
        {
            this.restClient = new RestClient("/v1beta1/");
        }

        public ListPods()
        {
            var request = new RestRequest("pods/", Method.GET);
            var response = this.restClient.Execute(request);
        }

        public ListPods(string selector)
        {
            var request = new RestRequest("pods/", Method.GET);
            request.AddParameter("labels", selector);
            var response = this.restClient.Execute(request);
        }
    }
}
