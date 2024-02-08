using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace VersioningWithQueryStringAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Enable attribute-based routing for Web API
            config.MapHttpAttributeRoutes();

            // Configure the default Web API route
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Replace the default controller selector with a custom implementation
            // This allows customizing controller selection logic, as demonstrated by CustomSelectorController
            config.Services.Replace(typeof(IHttpControllerSelector), new CustomSelectorController(config));
        }
    }
}
