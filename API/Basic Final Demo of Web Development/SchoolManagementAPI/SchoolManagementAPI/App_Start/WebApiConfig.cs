using SchoolManagementAPI.Filter;
using System.Web.Http;

namespace SchoolManagementAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new ExceptionLogFilter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "STUVersion1",
                routeTemplate: "api/v1/CLStudent/{id}",
                defaults: new { controller = "CLStudentV1", id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "STUVersion2",
                routeTemplate: "api/v2/CLStudent/{id}",
                defaults: new { controller = "CLStudentV2", id = RouteParameter.Optional }
            );
        }
    }
}
