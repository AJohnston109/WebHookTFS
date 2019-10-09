using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebHookTFS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //https://ndi-tfs.visualstudio.com/Web%20Hook/_apis/wit/workitems/$Product%20Backlog%20Item?api-version=5.1
            routes.MapRoute(
                name: "WebHook",
                url: "{organisation}/{controller}/{action}/{id}",
                defaults: new { controller = "WebhooksController", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
