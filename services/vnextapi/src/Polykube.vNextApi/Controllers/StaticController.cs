using System;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;

namespace Polykube.vNextApi.Controllers
{
    public class StaticController : BaseController
    {
        public const string ResponseText = "Hello World";

        [HttpGet]
        public string Index()
        {
            return ResponseText;
        }
    }
}
