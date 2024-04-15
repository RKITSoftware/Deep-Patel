using OnlineShoppingAPI.BL.Common;
using System;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OnlineShoppingAPI.Controllers.Attribute
{
    /// <summary>
    /// Custom Authorization Filter for Bearer Token Authentication.
    /// </summary>
    public class BearerAuthAttribute : AuthorizationFilterAttribute
    {
        #region Public Methods

        /// <summary>
        /// Overrides the default OnAuthorization method to perform Bearer Token Authentication.
        /// </summary>
        /// <param name="actionContext">The context for the action.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                if (!CheckAllowAnonymousAttribute(actionContext))
                {
                    string token = actionContext.Request.Headers.Authorization?.ToString();

                    // Checking if Token is provided
                    if (string.IsNullOrEmpty(token))
                    {
                        actionContext.Response = BLHelper.ResponseMessage(
                            HttpStatusCode.Unauthorized, "Please provide a token.");
                        return;
                    }

                    // Validating JWT Token
                    if (!BLToken.IsJwtValid(token))
                    {
                        actionContext.Response = BLHelper.ResponseMessage(
                            HttpStatusCode.Unauthorized, "Token has been altered.");
                        return;
                    }

                    // Setting the authenticated user in HttpContext
                    IPrincipal principal = BLToken.GetPrincipal(token);

                    if (principal == null)
                    {
                        actionContext.Response = BLHelper.ResponseMessage(
                            HttpStatusCode.Unauthorized, "Unauthorized user.");
                        return;
                    }

                    HttpContext.Current.User = principal;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Checks the AllowAnonymous Attribute Exists or not.
        /// </summary>
        /// <param name="actionContext">The context for the action.</param>
        /// <returns>True if exists else false.</returns>
        private bool CheckAllowAnonymousAttribute(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor
                .GetCustomAttributes<AllowAnonymousAttribute>().Any() || actionContext.ControllerContext
                .ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }

        #endregion Private Methods
    }
}