using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Models.POCO;
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

namespace OnlineShoppingAPI.Security
{
    /// <summary>
    /// Custom Authorization Filter for Cookie-Based Authentication.
    /// </summary>
    public class CookieBasedAuthAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Overrides the default OnAuthorization method to perform Cookie-Based Authentication.
        /// </summary>
        /// <param name="actionContext">The context for the action.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                // Getting Cookie Value
                CookieHeaderValue cookie = actionContext.Request.Headers
                    .GetCookies("MyAuth").FirstOrDefault();

                // Checking if Cookie is present
                if (cookie == null)
                {
                    actionContext.Response = BLHelper.ResponseMessage(
                        HttpStatusCode.Unauthorized, "Please login.");
                    return;
                }

                // Extracting authentication token from the cookie
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
                    // If credentials are valid, create a user identity.
                    USR01 userDetail = BLHelper.GetUser(username);
                    GenericIdentity identity = new GenericIdentity(username);

                    // Adding claims to the user identity.
                    identity.AddClaim(new Claim(ClaimTypes.Name, userDetail.R01F02));
                    identity.AddClaim(new Claim(ClaimTypes.Email,
                        userDetail.R01F02 + "@gmail.com"));

                    // Creating a principal based on the user identity and roles.
                    IPrincipal principal = new GenericPrincipal(identity,
                        userDetail.R01F04.Split(','));

                    // Setting the current principal for the thread.
                    Thread.CurrentPrincipal = principal;

                    // Setting the current principal for the HttpContext if available.
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal;
                    }
                    else
                    {
                        actionContext.Response = BLHelper.ResponseMessage(
                            HttpStatusCode.Unauthorized, "Authorization Denied");
                    }
                }
                else
                {
                    // If invalid, return Unauthorized response.
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
