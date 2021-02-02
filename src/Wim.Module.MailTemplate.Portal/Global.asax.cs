using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Wim.Module.MailTemplate.UI;

namespace Wim.Module.MailTemplate.Portal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //configure ORM
            string connectionString = Wim.Data.Common.GetPortal("PORTAL").Connection;
            Sushi.MicroORM.DatabaseConfiguration.SetDefaultConnectionString(connectionString);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var mailer = new Mailer();
        }
    }
}
