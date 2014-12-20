using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNet.Mvc;

namespace Polykube.Frontend.Filters
{
    public class InstrumentingFilter : ActionFilterAttribute
    {
        private Stopwatch requestTimer;
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            requestTimer = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var time = requestTimer.ElapsedMilliseconds;
            context.HttpContext.Response.Headers.Add(
                "ActionElapsedTime",
                new string[] { time.ToString(CultureInfo.InvariantCulture) + " ms" });
        }
    }
}
