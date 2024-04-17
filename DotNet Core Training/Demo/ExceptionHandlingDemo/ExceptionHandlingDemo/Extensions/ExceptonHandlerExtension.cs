using Microsoft.AspNetCore.Diagnostics;

namespace ExceptionHandlingDemo.Extensions
{
    public static class ExceptonHandlerExtension
    {
        public static void UseCustomExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync("Exception Handling from Extension Method.");
                    }
                });
            });
        }
    }
}
