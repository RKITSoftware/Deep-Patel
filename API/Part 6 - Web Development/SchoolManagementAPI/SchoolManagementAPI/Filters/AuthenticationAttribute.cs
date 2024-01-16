using SchoolManagementAPI.Models;
using SchoolManagementAPI.Validation;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SchoolManagementAPI.Filters
{
    /// <summary>
    /// Custom authentication attribute for Web API actions.
    /// </summary>
    public class AuthenticationAttribute : AuthorizationFilterAttribute
    {
        #region Override Methods

        /// <summary>
        /// Performs authorization by validating the provided credentials.
        /// </summary>
        /// <param name="actionContext">The context for the action being authorized.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // Check if the Authorization header is present in the request.
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request
                    .CreateErrorResponse(HttpStatusCode.BadRequest, "Please enter authorization details");
            }

            try
            {
                // Extract and decode the authentication token from the Authorization header.
                string authToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                string[] usernameAndPassword = decodedAuthToken.Split(':');

                string username = usernameAndPassword[0];
                string password = usernameAndPassword[1];

                // Check if the provided username and password are valid.
                if (ValidateUser.CheckUser(username, password))
                {
                    // Retrieve user details based on the provided username and password.
                    USR01 userDetail = ValidateUser.GetUserDetail(username, password);

                    // Create a generic identity and add claims.
                    GenericIdentity identity = new GenericIdentity(username);
                    identity.AddClaim(new Claim(ClaimTypes.Name, userDetail.R01F02));
                    identity.AddClaim(new Claim("Id", Convert.ToString(userDetail.R01F01)));

                    // Create a generic principal with the identity and roles.
                    IPrincipal principal = new GenericPrincipal(identity, userDetail.R01F04.Split(','));

                    // Set the current principal for the executing thread.
                    Thread.CurrentPrincipal = principal;

                    // Set the current user for the HttpContext if available.
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal;
                    }
                    else
                    {
                        // If HttpContext.Current is null, return an unauthorized response.
                        actionContext.Response = actionContext.Request
                            .CreateErrorResponse(HttpStatusCode.Unauthorized, "Authorization Denied");
                    }
                }
                else
                {
                    // If the provided credentials are invalid, return an unauthorized response.
                    actionContext.Response = actionContext.Request
                            .CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid Credentials");
                }
            }
            catch (Exception)
            {
                // Handle any unexpected exceptions and return an internal server error response.
                actionContext.Response = actionContext.Request
                        .CreateErrorResponse(HttpStatusCode.InternalServerError,
                            "Internal Server Error - Please Try After Some Time");
            }
        }

        #endregion
    }
}
