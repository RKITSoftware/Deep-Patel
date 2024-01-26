using Microsoft.Web.Http;
using Microsoft.Web.Http.Versioning;
using System.Web.Http;

namespace DemoVersioning
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable versioning
            config.AddApiVersioning(options =>
            {
                options.ApiVersionReader = new HeaderApiVersionReader("Accept", "api-version");
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });

            // Your other configuration goes here

            // Map the route template including the API version
            config.Routes.MapHttpRoute(
                name: "VersionedApi",
                routeTemplate: "api/v{version:apiVersion}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
