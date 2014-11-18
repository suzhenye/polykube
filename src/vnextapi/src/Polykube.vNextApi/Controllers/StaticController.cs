using System;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;

namespace Polykube.vNextApi.Controllers
{
    [Route("api/[controller]/{id?}")]
    public class StaticController : BaseController
    {
        [HttpGet]
        public string Index()
        {
            var result = "Hello world.";
            return result;
        }
    }
}
