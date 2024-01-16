using System;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace SchoolManagementAPI.Filters
{
    /// <summary>
    /// Custom caching filter for Web API actions.
    /// </summary>
    public class CacheFilter : ActionFilterAttribute
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the time duration for which the response should be cached.
        /// </summary>
        public int TimeDuration { get; set; }

        #endregion

        #region Override Methods

        /// <summary>
        /// Called after the action method executes. Sets cache-related HTTP headers in the response.
        /// </summary>
        /// <param name="actionExecutedContext">The context for the action executed.</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            // Set cache-related HTTP headers in the response.
            actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromSeconds(TimeDuration), // Set the maximum age of the cached content.
                MustRevalidate = true, // Indicate that the client should revalidate with the server before using a cached response.
                Public = true // Cache the response in public caches.
            };
        }

        #endregion
    }
}
