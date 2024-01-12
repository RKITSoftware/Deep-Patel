using System.Web.Http;

namespace VersioningUsingURLAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Route for API version 1, using "api/v1/employee/{id}" pattern
            config.Routes.MapHttpRoute(
                name: "EmpVersion1", // Route name
                routeTemplate: "api/v1/employee/{id}", // URL pattern
                defaults: new { controller = "CLEmployeeV1", id = RouteParameter.Optional } // Controller and optional id parameter
            );

            // Route for API version 2, using "api/v2/employee/{id}" pattern
            config.Routes.MapHttpRoute(
                name: "EmpVersion2", // Route name
                routeTemplate: "api/v2/employee/{id}", // URL pattern
                defaults: new { controller = "CLEmployeeV2", id = RouteParameter.Optional } // Controller and optional id parameter
            );
        }
    }
}
