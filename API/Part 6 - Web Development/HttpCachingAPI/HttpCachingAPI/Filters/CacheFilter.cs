using System;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace HttpCachingAPI.Filters
{
    /// <summary>
    /// Custom cache filter attribute derived from ActionFilterAttribute
    /// </summary>
    public class CacheFilter : ActionFilterAttribute
    {
        #region Public Properties

        /// <summary>
        /// Time duration for caching in seconds
        /// </summary>
        public int TimeDuration { get; set; }

        #endregion

        #region Override Methods

        /// <summary>
        /// Override the OnActionExecuted method to set caching headers
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            // Create a new CacheControlHeaderValue instance to manage caching headers
            actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                // Set the maximum age of the cached content
                MaxAge = TimeSpan.FromSeconds(TimeDuration),

                // Indicate that the cached content must be revalidated with the server before serving
                MustRevalidate = true,

                // Specify that the cached content can be stored and used by public caches
                Public = true
            };
        }

        #endregion
    }
}
