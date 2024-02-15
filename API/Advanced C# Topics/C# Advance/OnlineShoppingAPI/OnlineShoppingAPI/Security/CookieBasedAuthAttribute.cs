using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Models;
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
    public class CookieBasedAuthAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                // Getting Cookie Value
                CookieHeaderValue cookie = actionContext.Request.Headers.GetCookies("MyAuth").FirstOrDefault();
                string authToken = cookie["MyAuth"].Value;

                // Decode the base64-encoded credentials to get the username and password.
                string decodedAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                string[] usernamePassword = decodedAuthToken.Split(':');

                string username = usernamePassword[0];
                string password = usernamePassword[1];

                // Validate user credentials using the BLUser class.
                if (BLHelper.IsExist(username, password))
                {
                    USR01 userDetail = BLHelper.GetUser(username);
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
                        actionContext.Response = actionContext.Request
                            .CreateErrorResponse(HttpStatusCode.Unauthorized, "Authorization Denied");
                    }
                }
                else
                {
                    // If invalid, return Unauthorized response.
                    actionContext.Response = actionContext.Request
                        .CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid Credentials");
                }
            }
            catch (Exception ex)
            {
                // Handle unexpected errors and return Internal Server Error response.
                BLHelper.SendErrorToTxt(ex, HttpContext.Current.Server.MapPath("~/Logs"));
                actionContext.Response = actionContext.Request
                    .CreateErrorResponse(HttpStatusCode.InternalServerError,
                        "Internal Server Error - Please Try After Some Time");
            }
        }
    }
}