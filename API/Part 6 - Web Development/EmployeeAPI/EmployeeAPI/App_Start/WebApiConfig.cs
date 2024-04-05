using System.Web.Http;

namespace EmployeeAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Add a custom exception filter (NotImplExceptionFilterAttribute) to the global filters collection.
            config.Filters.Add(new EmployeeAPI.Filter.NotImplExceptionFilterAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
