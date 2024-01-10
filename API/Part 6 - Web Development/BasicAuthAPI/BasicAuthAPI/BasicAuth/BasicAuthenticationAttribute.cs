using BasicAuthAPI.Authentication;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BasicAuthAPI.BasicAuth
{
    /// <summary>
    /// BasicAuthenticationAttribute is an authorization filter attribute for handling basic authentication.
    /// </summary>
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        #region Public Methods

        /// <summary>
        /// Override the OnAuthorization method to implement custom authentication logic.
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Check if Authorization header is present in the request.
            if (actionContext.Request.Headers.Authorization == null)
            {
                // If not, return Unauthorized response.
                actionContext.Response = actionContext.Request
                    .CreateErrorResponse(HttpStatusCode.Unauthorized, "Login Failed");
            }
            else
            {
                try
                {
                    // Extract the base64-encoded credentials from the Authorization header.
                    string authToken = actionContext.Request.Headers.Authorization.Parameter;

                    // Decode the base64-encoded credentials to get the username and password.
                    string decodedAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                    string[] usernamePassword = decodedAuthToken.Split(':');

                    string username = usernamePassword[0];
                    string password = usernamePassword[1];

                    // Validate user credentials using the ValidateUser class.
                    if (ValidateUser.LogIn(username, password))
                    {
                        // If valid, set the current principal with the authenticated user's identity.
                        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                    }
                    else
                    {
                        // If invalid, return Unauthorized response.
                        actionContext.Response = actionContext.Request
                            .CreateErrorResponse(HttpStatusCode.Unauthorized, "Login Failed");
                    }
                }
                catch (Exception)
                {
                    // Handle unexpected errors and return Internal Server Error response.
                    actionContext.Response = actionContext.Request
                        .CreateErrorResponse(HttpStatusCode.InternalServerError,
                            "Internal Server Error - Please Try After Some Time");
                }
            }
        }

        #endregion
    }
}
