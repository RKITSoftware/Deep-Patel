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
    public class CookieBasedAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple => false;

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
                    USR01 userDetail = ValidateUser.GetUserDetail(username, password);
                    GenericIdentity identity = new GenericIdentity(username);

                    identity.AddClaim(new Claim(ClaimTypes.Name, userDetail.R01F02));
                    identity.AddClaim(new Claim(ClaimTypes.Email, userDetail.R01F02 + "@gmail.com"));

                    IPrincipal principal = new GenericPrincipal(identity, userDetail.R01F04.Split(','));

                    Thread.CurrentPrincipal = principal;

                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal;
                    }
                    else
                    {
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
                context.ErrorResult = ErrorResponse(HttpStatusCode.InternalServerError,
                        ex.Message);
            }
        }

        public async Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {

        }

        private ResponseMessageResult ErrorResponse(HttpStatusCode statusCode, string message)
        {
            return new ResponseMessageResult(new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(message)
            });
        }
    }
}