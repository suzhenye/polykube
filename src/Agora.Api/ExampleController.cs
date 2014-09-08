using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;

using RestSharp;
using Kubernetes.ApiClient;
using Kubernetes.ApiClient.Models;

namespace Agora.Api
{
    public class ExampleController : IActionFilter, IResultFilter
    {
        private Stopwatch _timer1;
        private Stopwatch _timer2;

        public string Index()
        {
            var kubeClient = new KubeClient();
            
            var podList = kubeClient.ListPods();
            if (podList == null)
            {
                return "Hello World!";
            }
            
            var podListEntryList = podList.Items;
            if (podListEntryList == null)
            {
                return "Hello World!!";
            }

            var result = String.Format(
                CultureInfo.InvariantCulture,
                "Hello World. There are {0} frontends.",
                podListEntryList.Count()/*,
                String.Join(",", podListEntryList.Select(fe => fe.Id))*/);

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