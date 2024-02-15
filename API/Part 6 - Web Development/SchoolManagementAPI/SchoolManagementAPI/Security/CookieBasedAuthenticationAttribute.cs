using SchoolManagementAPI.Models;
using SchoolManagementAPI.Validation;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace SchoolManagementAPI.Security
{
    /// <summary>
    /// Custom attribute for cookie-based authentication in ASP.NET Web API.
    /// Implements IAuthenticationFilter interface to handle authentication process.
    /// </summary>
    public class CookieBasedAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        /// <summary>
        /// Gets a value indicating whether this attribute can be specified more than once.
        /// </summary>
        public bool AllowMultiple => false;

        /// <summary>
        /// Authenticates the user based on the provided cookie containing authentication information.
        /// </summary>
        /// <param name="context">HttpAuthenticationContext for authentication.</param>
        /// <param name="cancellationToken">Cancellation token for asynchronous operation.</param>
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            try
            {
                // Getting Cookie Value
                CookieHeaderValue cookie = context.Request.Headers.GetCookies("MyAuth").FirstOrDefault();
                string authToken = cookie["MyAuth"].Value;

                // Decode the base64-encoded credentials to get the username and password.
                string decodedAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                string[] usernamePassword = decodedAuthToken.Split(':');

                string username = usernamePassword[0];
                string password = usernamePassword[1];

                // Validate user credentials using the BLUser class.
                if (ValidateUser.CheckUser(username, password))
                {
                    // Retrieve user details for creating claims.
                    USR01 userDetail = ValidateUser.GetUserDetail(username, password);
                    GenericIdentity identity = new GenericIdentity(username);

                    // Add claims for user identity.
                    identity.AddClaim(new Claim(ClaimTypes.Name, userDetail.R01F02));
                    identity.AddClaim(new Claim(ClaimTypes.Email, userDetail.R01F02 + "@gmail.com"));

                    // Create a principal with the user identity and roles.
                    IPrincipal principal = new GenericPrincipal(identity, userDetail.R01F04.Split(','));

                    // Set the current principal for the thread and HttpContext.
                    Thread.CurrentPrincipal = principal;

                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal;
                    }
                    else
                    {
                        // Set an error result for unauthorized access.
                        context.ErrorResult = ErrorResponse(HttpStatusCode.Unauthorized, "Authorization Denied");
                    }
                }
                else
                {
                    // If invalid, return Unauthorized response.
                    context.ErrorResult = ErrorResponse(HttpStatusCode.Unauthorized, "Invalid Credentials");
                }
            }
            catch (Exception ex)
            {
                // Set an error result for internal server error.
                context.ErrorResult = ErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// ChallengeAsync method from IAuthenticationFilter interface.
        /// No additional challenge logic is implemented in this case.
        /// </summary>
        /// <param name="context">HttpAuthenticationChallengeContext for authentication challenge.</param>
        /// <param name="cancellationToken">Cancellation token for asynchronous operation.</param>
        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken) { }

        /// <summary>
        /// Helper method to generate a custom error response.
        /// </summary>
        /// <param name="statusCode">HTTP status code for the response.</param>
        /// <param name="message">Error message for the response content.</param>
        /// <returns>ResponseMessageResult with the specified status code and message.</returns>
        private ResponseMessageResult ErrorResponse(HttpStatusCode statusCode, string message)
        {
            return new ResponseMessageResult(new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(message)
            });
        }
    }

}