using Owin;
using System.Web.Http;

namespace WebHookTFS
{
    public class Startup
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            // "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
                );
        }
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();

            httpConfiguration.InitializeReceiveVstsWebHooks();
        }
    }
}