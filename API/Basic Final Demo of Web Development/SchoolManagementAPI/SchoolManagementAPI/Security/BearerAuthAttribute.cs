using SchoolManagementAPI.Business_Logic;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SchoolManagementAPI.Security
{
    /// <summary>
    /// Custom authorization attribute for Bearer token authentication.
    /// </summary>
    public class BearerAuthAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Called when authorization is required.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                // Getting Cookie Value
                CookieHeaderValue cookie = actionContext.Request.Headers
                    .GetCookies("Session-Id").FirstOrDefault();

                if (cookie == null)
                {
                    // Unauthorized: Cookie not found
                    actionContext.Response = BLHelper.ResponseMessage(
                        HttpStatusCode.Unauthorized, "Please login");
                    return;
                }

                string sessionId = cookie["Session-Id"]?.Value;
                string jwtToken = (string)BLHelper.ServerCache.Get(sessionId);

                if (jwtToken == null)
                {
                    // Unauthorized: Login expired
                    actionContext.Response = BLHelper.ResponseMessage(
                        HttpStatusCode.Unauthorized, "Login expired, please login.");
                    return;
                }

                // Validate JWT Token and get principal
                IPrincipal principal = BLToken.GetPrincipal(jwtToken);

                if (principal == null)
                {
                    // Unauthorized: Invalid user
                    actionContext.Response = BLHelper.ResponseMessage(
                        HttpStatusCode.Unauthorized, "Unauthorized user.");
                    return;
                }

                // Set the authenticated principal in the current HttpContext
                HttpContext.Current.User = principal;
            }
            catch (Exception ex)
            {
                // Log and handle any exceptions during authentication
                BLHelper.LogError(ex);
                actionContext.Response = BLHelper.ResponseMessage(HttpStatusCode.BadRequest,
                    "An error occurred during authentication");
            }
        }
    }
}
