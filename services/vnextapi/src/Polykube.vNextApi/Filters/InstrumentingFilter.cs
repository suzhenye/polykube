using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNet.Mvc;

namespace Polykube.vNextApi.Filters
{
    public class InstrumentingFilter : ActionFilterAttribute
    {
        private Stopwatch _timer1;
        private Stopwatch _timer2;
        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _timer1 = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var time = _timer1.ElapsedMilliseconds;
            context.HttpContext.Response.Headers.Add(
                "ActionElapsedTime",
                new string[] { time.ToString(CultureInfo.InvariantCulture) + " ms" });
        }
    }
}
