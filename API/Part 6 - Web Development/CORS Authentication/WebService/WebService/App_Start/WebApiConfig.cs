using System.Web.Http;

namespace WebService
{
    // Configuration class for Web API routing and settings
    public static class WebApiConfig
    {
        // Method to register Web API configurations
        public static void Register(HttpConfiguration config)
        {
            // Enable Cross-Origin Resource Sharing (CORS) for the Web API
            config.EnableCors();

            // Map Web API attribute routes (routes defined using attributes in controllers)
            config.MapHttpAttributeRoutes();

            // Default route for Web API controllers
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
