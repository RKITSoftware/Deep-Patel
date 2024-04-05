using AspDotNetCoreRequestProcessingPipeline.Middleware;

namespace AspDotNetCoreRequestProcessingPipeline.Extension
{
    /// <summary>
    /// A extension class that provies extensions methods for the application builder
    /// </summary>
    public static class MiddleWareExtensions
    {
        /// <summary>
        /// For using the CustomMiddleware in Web API
        /// </summary>
        /// <param name="builder">Class that extends this method</param>
        /// <returns>Middleware configuration for using CustomMiddleware</returns>
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
