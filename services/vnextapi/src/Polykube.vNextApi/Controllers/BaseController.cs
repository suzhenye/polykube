using System;
using Microsoft.AspNet.Mvc;
using Polykube.vNextApi.Filters;

namespace Polykube.vNextApi.Controllers
{
    [InstrumentingFilter]
    public class BaseController : Controller
    {

    }
}
