namespace AspDotNetCoreRequestProcessingPipeline.Middleware
{
    public class CustomMiddleware : IMiddleware
    {
        /// <summary>
        /// Gets the current request context and write headers into the response of the current request and
        /// executed next middleware.
        /// </summary>
        /// <param name="context">Current request context</param>
        /// <param name="next">Next middleware reference</param>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Processing request accordingly to the specified Middleware

            // Getting headers data and adding it to responseMsg
            string responseMsg = "";
            foreach (var item in context.Request.Headers)
            {
                responseMsg += item.Key + "-" + item.Value + Environment.NewLine;
            }

            // Returning responseMsg
            //await context.Response.WriteAsync(responseMsg);

            await next(context);
        }
    }
}
