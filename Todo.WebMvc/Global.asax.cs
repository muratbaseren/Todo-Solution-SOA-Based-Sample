using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Todo.WebMvc.Models;

namespace Todo.WebMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //WebClient wc = new WebClient() { Encoding = System.Text.Encoding.UTF8 };
            //string result = wc.DownloadString("http://localhost:51844/api/Home/GetCache?key=last_update");

            HttpContext.Current.Cache["last_update"] = CacheHelper.GetLastUpdateDateTime();
            //DateTime.Parse(result.Replace("\"", "").Replace(@"\", ""));
        }
    }
}
