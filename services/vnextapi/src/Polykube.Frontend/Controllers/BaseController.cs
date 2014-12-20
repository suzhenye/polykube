using System;
using Microsoft.AspNet.Mvc;
using Polykube.Frontend.Filters;

namespace Polykube.Frontend.Controllers
{
    [InstrumentingFilter]
    public class BaseController : Controller
    {

    }
}
