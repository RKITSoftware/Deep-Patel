using OnlineShoppingAPI.BL.Common;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace OnlineShoppingAPI.Controllers.Filter
{
    /// <summary>
    /// My exception filter for writing all exception occured during when server is run.
    /// </summary>
    public class ExceptionLogFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Method run when exception occurs
        /// </summary>
        /// <param name="actionExecutedContext">Contains the information about exceptions.</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            BLHelper.LogError(actionExecutedContext.Exception);
            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An internal server error occured during request.")
            };
        }
    }
}