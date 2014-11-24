using System;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;

namespace Polykube.vNextApi.Controllers
{
    public class SampleController : Controller
    {
        [HttpGet]
        public string Index()
        {
            var envDict = Environment.GetEnvironmentVariables();
            var result = JsonConvert.SerializeObject(envDict);
            return result;
        }
    }
}
