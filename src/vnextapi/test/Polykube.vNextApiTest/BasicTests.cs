using System.Collections.Generic;
using Microsoft.AspNet.TestHost;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Fallback;
using Microsoft.Framework.Runtime;
using Newtonsoft.Json;
using Polykube.vNextApi;
using Xunit;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Hosting.Server;
using Microsoft.Framework.OptionsModel;
using Microsoft.Framework.Runtime.Infrastructure;
using System.Linq;
using System;

// https://github.com/aspnet/Testing/wiki/How-to-create-test-projects

namespace Polykube.vNextApiTest
{   
    public class BasicTests
    {
        [Fact]
        public async void BasicTest01()
        {
            var startup = new Startup();
            var hostingServices = new ServiceCollection()
                    .Add(HostingServices.GetDefaultServices())
                    .AddInstance<IHostingEnvironment>(new HostingEnvironment { EnvironmentName = "Development" })
                    .BuildServiceProvider(CallContextServiceLocator.Locator.ServiceProvider);
            var services = new ServiceCollection()
                    .Add(OptionsServices.GetDefaultServices());
            startup.ConfigureServices(services);
            var applicationServices = services.BuildServiceProvider(hostingServices);

            using (var server = TestServer.Create(applicationServices, startup.Configure))
            {
                var client = server.CreateClient();
                var response = await client.GetAsync("/api/environment");
                var obj = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(await response.Content.ReadAsStringAsync());

                Assert.True(obj.Keys.Count() > 1, "no environment variables were retuned");
            }
        }
    }
}

/*
Exception has been thrown by the target of an invocation.
Object reference not set to an instance of an object.
   at System.RuntimeMethodHandle.InvokeMethod(Object target, Object[] arguments, Signature sig, Boolean constructor)
   at System.Reflection.RuntimeConstructorInfo.Invoke(BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at System.Reflection.ConstructorInfo.Invoke(Object[] parameters)
   at Microsoft.Framework.DependencyInjection.ServiceLookup.Service.ConstructorCallSite.Invoke(ServiceProvider provider)
   at Microsoft.Framework.DependencyInjection.ServiceProvider.ScopedCallSite.Invoke(ServiceProvider provider)
   at Microsoft.Framework.DependencyInjection.ServiceProvider.<>c__DisplayClass5.<RealizeService>b__6(ServiceProvider provider)
   at Microsoft.Framework.DependencyInjection.ServiceProvider.GetService(Type serviceType)
   at Microsoft.Framework.DependencyInjection.ServiceProvider.GetFallbackService(Type serviceType)
   at Microsoft.Framework.DependencyInjection.ServiceProvider.GetServiceCallSite(Type serviceType)
   at Microsoft.Framework.DependencyInjection.ServiceProvider.<GetService>b__2(Type key)
   at System.Collections.Concurrent.ConcurrentDictionary`2.GetOrAdd(TKey key, Func`2 valueFactory)
   at Microsoft.Framework.DependencyInjection.ServiceProvider.GetService(Type serviceType)
   at Microsoft.Framework.DependencyInjection.ServiceProviderExtensions.GetRequiredService(IServiceProvider provider, Type serviceType)
   at Microsoft.AspNet.Mvc.DefaultControllerActivator.<>c__DisplayClass12.<CreateActivateInfo>b__13(ActionContext actionContext)
   at Microsoft.AspNet.Mvc.PropertyActivator`1.Activate(Object view, TContext context)
   at Microsoft.AspNet.Mvc.DefaultControllerActivator.Activate(Object controller, ActionContext context)
   at Microsoft.AspNet.Mvc.DefaultControllerFactory.CreateController(ActionContext actionContext)
   at Microsoft.AspNet.Mvc.ControllerActionInvoker.<InvokeAsync>d__1.MoveNext()
   --- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at Microsoft.AspNet.Mvc.MvcRouteHandler.<RouteAsync>d__1.MoveNext()
   --- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at Microsoft.AspNet.Routing.Template.TemplateRoute.<RouteAsync>d__1.MoveNext()
   --- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at Microsoft.AspNet.Mvc.Routing.AttributeRoute.<RouteAsync>d__1.MoveNext()
   --- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at Microsoft.AspNet.Routing.RouteCollection.<RouteAsync>d__1.MoveNext()
   --- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at Microsoft.AspNet.Builder.RouterMiddleware.<Invoke>d__1.MoveNext()
   --- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at Microsoft.AspNet.TestHost.ClientHandler.<>c__DisplayClass0.<<SendAsync>b__1>d__0.MoveNext()
   --- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   at Microsoft.AspNet.TestHost.ClientHandler.<SendAsync>d__1.MoveNext()
   --- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   at Polykube.vNextApiTest.BasicTests.<BasicTest01>d__1.MoveNext() in C:\\Users\\colemickens\\Source\\Repos\\polykube\\src\\vnextapi\\test\\Polykube.vNextApiTest\\BasicTests.cs:line 43
   --- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.AsyncMethodBuilderCore.<ThrowAsync>b__3(Object state)
   at Xunit.Sdk.AsyncTestSyncContext.Send(SendOrPostCallback d, Object state)
   at Microsoft.AspNet.Mvc.UrlHelper..ctor(IContextAccessor`1 contextAccessor, IActionSelector actionSelector)",
*/
