using Microsoft.AspNetCore.Diagnostics;
using PlacementCellManagementAPI.Business_Logic.Interface;
using PlacementCellManagementAPI.Business_Logic.Services;
using PlacementCellManagementAPI.Models.POCO;
using System.Net;

namespace PlacementCellManagementAPI.Extensions
{
    public static class ExceptionHandlerExtension
    {
        /// <summary>
        /// Configures the global exception handling middleware.
        /// </summary>
        /// <param name="app">The application builder instance.</param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        IExceptionLogger logger = new BLException();
                        logger.Log($"Something went wrong: {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetail()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
    }
}
