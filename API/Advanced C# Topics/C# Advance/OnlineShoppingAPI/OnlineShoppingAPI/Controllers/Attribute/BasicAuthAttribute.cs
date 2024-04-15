using OnlineShoppingAPI.BL.Common;
using OnlineShoppingAPI.Models.POCO;
using System;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OnlineShoppingAPI.Controllers.Attribute
{
    /// <summary>
    /// Custom Authorization Filter for Basic Authentication.
    /// </summary>
    public class BasicAuthAttribute : AuthorizationFilterAttribute
    {
        #region Public Methods

        /// <summary>
        /// Overrides the default OnAuthorization method to perform Basic Authentication.
        /// </summary>
        /// <param name="actionContext">The context for the action.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                // If Authorization header is not present, return Unauthorized response.
                actionContext.Response = BLHelper.ResponseMessage(HttpStatusCode.Unauthorized,
                    "Login failed");
            }
            else
            {
                try
                {
                    // Extract the base64-encoded credentials from the Authorization header.
                    string authToken = actionContext.Request.Headers.Authorization.Parameter;

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
                    throw ex;
                }
            }
        }

        #endregion Public Methods
    }
}