using System;
using Microsoft.AspNet.Mvc;
using Polykube.vNextApi.Middleware;

namespace Polykube.vNextApi.Controllers
{
    [InstrumentingFilter]
    public class BaseController : Controller
    {

    }
}
