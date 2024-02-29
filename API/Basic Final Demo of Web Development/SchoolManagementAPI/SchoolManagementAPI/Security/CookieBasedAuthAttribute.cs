using SchoolManagementAPI.Business_Logic;
using SchoolManagementAPI.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SchoolManagementAPI.Security
{
    /// <summary>
    /// Custom authorization attribute for Cookie-based authentication.
    /// </summary>
    public class CookieBasedAuthAttribute : AuthorizationFilterAttribute
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
                    .GetCookies("MyAuth").FirstOrDefault();

                if (cookie == null)
                {
                    // Unauthorized: Cookie not found
                    actionContext.Response = BLHelper.ResponseMessage(
                        HttpStatusCode.Unauthorized, "Please login");
                    return;
                }

                string authToken = cookie["MyAuth"]?.Value;

                // Decode the base64-encoded credentials to get the username and password.
                string decodedAuthToken = Encoding.UTF8.GetString(
                    Convert.FromBase64String(authToken));
                string[] usernamePassword = decodedAuthToken.Split(':');

                string username = usernamePassword[0];
                string password = usernamePassword[1];

                // Validate user credentials using the BLUser class.
                if (BLHelper.IsExist(username, password))
                {
                    // Get user details from BLHelper
                    USR01 userDetail = BLHelper.GetUser(username, password);

                    // Create a generic identity
                    GenericIdentity identity = new GenericIdentity(username);

                    // Add claims to the identity
                    identity.AddClaim(new Claim(ClaimTypes.Name, userDetail.R01F02));
                    identity.AddClaim(new Claim(ClaimTypes.Email, userDetail.R01F02 + "@gmail.com"));

                    // Create a generic principal with the identity and roles
                    IPrincipal principal = new GenericPrincipal(identity, userDetail.R01F04.Split(','));

                    // Set the current principal for the current thread
                    Thread.CurrentPrincipal = principal;

                    // Set the principal for the HttpContext, if available
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal;
                    }
                    else
                    {
                        // If HttpContext is not available, return Unauthorized response
                        actionContext.Response = BLHelper.ResponseMessage(
                            HttpStatusCode.Unauthorized, "Authorization Denied");
                    }
                }
                else
                {
                    // Unauthorized: Invalid credentials
                    actionContext.Response = BLHelper.ResponseMessage(
                        HttpStatusCode.Unauthorized, "Invalid Credentials");
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected errors and return Internal Server Error response.
                BLHelper.LogError(ex);
                actionContext.Response = BLHelper.ResponseMessage(
                    HttpStatusCode.InternalServerError,
                        "Internal Server Error - Please Try After Some Time");
            }
        }
    }
}
