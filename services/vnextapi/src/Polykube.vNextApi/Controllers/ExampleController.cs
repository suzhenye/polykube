using System;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;

namespace Polykube.vNextApi.Controllers
{
    [Route("api/[controller]")]
    public class ExampleController : Controller
    {
        [HttpGet]
        public string Index()
        {
            return "this is a test";
        }
    }
}
