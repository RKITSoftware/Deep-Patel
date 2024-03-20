namespace MiddleWareDemo.Middleware
{
    public class CustomHeaderMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Do all work related to response before the requestDelegate because it sends the response
            // to the client so no other modifictaions can be done after that.
            context.Response.Headers.Append("Author", "Deep Patel");
            context.Response.Redirect("https://www.google.com");

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
