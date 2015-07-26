using GardenManager.Web.App_Start;
using GardenManager.Web.CustomViewEngines;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GardenManager.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Initialize global logging capabilities
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .WriteTo.RollingFile(@"C:\Logs\Log-{Date}.txt")
                .CreateLogger();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterTypes(UnityConfig.GetConfiguredContainer());

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new GardenHQViewEngine());
        }

        protected void Application_EndRequest()
        {
            // this used for debugging 500 Internal Server errors....
            Exception[] exceptions = this.Context.AllErrors;
        }
    }
}
