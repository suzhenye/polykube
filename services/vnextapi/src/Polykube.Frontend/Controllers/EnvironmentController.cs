using System;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;

namespace Polykube.Frontend.Controllers
{
    [Route("api/[controller]")]
    public class EnvironmentController : Controller
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
