using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace EmployeeAPI.Filter
{
    /// <summary>
    /// Custom exception filter to handle NotImplementedException and return a specific response.
    /// </summary>
    public class NotImplExceptionFilterAttribute : ExceptionFilterAttribute
    {
        #region Public Methods

        /// <summary>
        /// Overrides the OnException method to provide custom exception handling.
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            // Check if the exception is of type NotImplementedException.
            if (actionExecutedContext.Exception is Exception)
            {
                // Set the HTTP response with a status code of NotImplemented and a custom message.
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented)
                {
                    Content = new StringContent("Not Implemented Method")
                };
            }
        }
        #endregion
    }
}
