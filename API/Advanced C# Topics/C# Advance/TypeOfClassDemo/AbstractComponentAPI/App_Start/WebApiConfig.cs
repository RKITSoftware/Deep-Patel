using AbstractComponentAPI.Filter;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace AbstractComponentAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

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
