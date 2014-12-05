using System;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;

namespace Polykube.vNextApi.Controllers
{
    [Route("api/[controller]")]
    public class PassthroughController : Controller
    {
        [HttpGet]
        public string Index()
        {
            var client = new HttpClient();
            var resp = client.Get("http://goapi.default.kubernetes.local/");
            var content = resp.ReadAsStringAsync();

            return content;
        }
    }
}
