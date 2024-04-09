using OnlineShoppingAPI.Business_Logic;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OnlineShoppingAPI.Controllers.Attribute
{
    /// <summary>
    /// Custom Authorization Filter for Bearer Token Authentication.
    /// </summary>
    public class BearerAuthAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Overrides the default OnAuthorization method to perform Bearer Token Authentication.
        /// </summary>
        /// <param name="actionContext">The context for the action.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                string token = actionContext.Request.Headers.Authorization?.ToString();

                // Getting Cookie Value
                // CookieHeaderValue cookieJwtToken = actionContext.Request.Headers
                // .GetCookies().FirstOrDefault();

                // Checking if Token is provided
                if (token == null || token == "")
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

                // Retrieving Login Session ID from Cookie
                CookieHeaderValue cookieLoginSessionId = actionContext.Request.Headers
                    .GetCookies("LoginSessionId").FirstOrDefault();
                string sessionId = cookieLoginSessionId["LoginSessionId"]?.Value;

                // Retrieving JWT Token from Server Cache
                string jwtToken = (string)BLHelper.ServerCache.Get(sessionId);

                // Checking for Login Session Expiry
                if (jwtToken == null)
                {
                    actionContext.Response = BLHelper.ResponseMessage(
                        HttpStatusCode.Unauthorized, "Login session expired, please log in again.");
                    return;
                }

                // Checking for Token Modification Error
                if (jwtToken != token)
                {
                    actionContext.Response = BLHelper.ResponseMessage(
                        HttpStatusCode.Unauthorized, "Token modification error.");
                    return;
                }

                // Setting the authenticated user in HttpContext
                IPrincipal principal = BLToken.GetPrincipal(jwtToken);

                if (principal == null)
                {
                    actionContext.Response = BLHelper.ResponseMessage(
                        HttpStatusCode.Unauthorized, "Unauthorized user.");
                    return;
                }

                HttpContext.Current.User = principal;
            }
            catch (Exception ex)
            {
                // Logging the exception and returning an error response
                BLHelper.LogError(ex);
                actionContext.Response = BLHelper.ResponseMessage(HttpStatusCode.BadRequest,
                    "An error occurred during authentication.");
            }
        }
    }
}
