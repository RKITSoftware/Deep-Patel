using System;
using System.Web.Http.Filters;

namespace MyLibrary.Filter
{
    /// <summary>
    /// Custom action filter for adding caching-related headers to HTTP responses.
    /// </summary>
    public class CacheFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Gets or sets the duration (in seconds) for which the response should be cached.
        /// </summary>
        public int TimeDuration { get; set; }

        /// <summary>
        /// Overrides the base method to execute custom logic after an action method has been executed.
        /// </summary>
        /// <param name="actionExecutedContext">The context for the action executed.</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            // Set cache control headers for the HTTP response
            actionExecutedContext.Response.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue
            {
                // Set the maximum age for which the response is considered fresh
                MaxAge = TimeSpan.FromSeconds((TimeDuration > 0) ? TimeDuration : 30),

                // Indicates that the client must revalidate stale content before using it
                MustRevalidate = true,

                // Specifies that the response may be cached by any cache, even if it's not specific to a single user
                Public = true
            };
        }
    }
}
