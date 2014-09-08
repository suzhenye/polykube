using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

using etcetera;

using Agora.Config;

namespace Agora.Api
{
    public class ExampleController : IActionFilter, IResultFilter
    {
        private Stopwatch _timer1;
        private Stopwatch _timer2;

        public string Index()
        {
            var client = EtcdSettings.Client;
            
            var resp = client.Get("test");
            Console.WriteLine(resp);

            return "Hello world";
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _timer1 = Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var time = _timer1.ElapsedMilliseconds;
            context.HttpContext.Response.Headers.Add(
                "ActionElapsedTime", 
                new string[] { time.ToString(CultureInfo.InvariantCulture) + " ms" });
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            _timer2 = Stopwatch.StartNew();
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            var time = _timer2.ElapsedMilliseconds;
            context.HttpContext.Response.Headers.Add(
                "ResultElapsedTime", 
                new string[] { time.ToString(CultureInfo.InvariantCulture) + " ms" });
        }
    }
}