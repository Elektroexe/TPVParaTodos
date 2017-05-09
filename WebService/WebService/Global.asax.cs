using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebService.Resources;

namespace WebService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitUsers();
        }

        protected async void InitUsers()
        {
            await Utils.CreateRole("Administrator");
            await Utils.CreateUser("Admin", "Admin123!");
            await Utils.AssignRole("Admin", "Administrator");

            //await Utils.CreateRole("Waiter");
            //await Utils.CreateUser("WaiterTest", "Waiter");
            //await Utils.AssignRole("WaiterTest", "Waiter");

            //await Utils.CreateRole("Manager");
            //await Utils.CreateUser("ManagerTest", "Manager");
            //await Utils.AssignRole("ManagerTest", "Manager");
        }
    }
}
