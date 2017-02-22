using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EF_Unity_PostgreSQL.Controllers;
using EF_Unity_PostgreSQL.Models;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace EF_Unity_PostgreSQL
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
