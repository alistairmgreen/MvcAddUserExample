using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcAddUserExample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // The password hashing service uses libsodium-net, which calls
            // into an unmanaged DLL (because it is a wrapper for a C library).
            // ASP.Net does not make shadow copies of unmanaged DLLs, so in order to find
            // libsodium.dll we need to add the Bin directory to the path.
            // Source: https://stackoverflow.com/questions/28275944/how-to-include-libsodium-net-on-asp-net
            string path = Environment.GetEnvironmentVariable("PATH");
            string binDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Bin");
            Environment.SetEnvironmentVariable("PATH", $"{path};{binDir}");
        }
    }
}
