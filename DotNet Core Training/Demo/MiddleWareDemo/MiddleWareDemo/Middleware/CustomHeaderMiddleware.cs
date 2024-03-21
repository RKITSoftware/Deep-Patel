namespace MiddleWareDemo.Middleware
{
    /// <summary>
    /// Middleware for adding a custom header to HTTP responses.
    /// </summary>
    public class CustomHeaderMiddleware : IMiddleware
    {
        /// <summary>
        /// Adds a custom header to the HTTP response and performs a redirection.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="next">The delegate representing the next middleware in the pipeline.</param>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Add custom header to the response.
            context.Response.Headers.Append("Author", "Deep Patel");

            // Perform a redirection.
            context.Response.Redirect("https://www.google.com");

            // Call the next middleware in the pipeline.
            await next(context);
        }

        /* Redirect Script
        
            fetch('https://localhost:7136/api/User')
              .then(response => {
                if (response.ok) {
                  return response;
                }
              })
              .then(response => {
                const location = response.headers.get('Location');
                if (location) {
                  window.location.href = location;
                } else {
                  console.error('Location header not found in the response.');
                }
              })
              .catch(error => {
                console.error('Fetch error:', error);
              });

         */
    }
}
