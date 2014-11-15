using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

using Newtonsoft.Json;

namespace Agora.Api
{
    [Route("/")]
    public class ExampleController : IActionFilter, IResultFilter
    {
        private Stopwatch _timer1;
        private Stopwatch _timer2;

        public string Index()
        {
            // var envDict = Environment.GetEnvironmentVariables();
            // var result = JsonConvert.SerializeObject(envDict);
            var result = "This is a test.";
            return result;
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
