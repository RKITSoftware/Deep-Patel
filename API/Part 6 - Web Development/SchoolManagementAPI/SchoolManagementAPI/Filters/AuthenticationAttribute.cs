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
    public class AuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request
                    .CreateErrorResponse(HttpStatusCode.BadRequest, "Please enter authorization details");
            }

            try
            {
                string authToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodeAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                string[] usernameAndPassword = decodeAuthToken.Split(':');

                string username = usernameAndPassword[0];
                string password = usernameAndPassword[1];

                if(ValidateUser.CheckUser(username, password))
                {
                    USR01 userDetail = ValidateUser.GetUserDetail(username, password);

                    GenericIdentity identity = new GenericIdentity(username);

                    identity.AddClaim(new Claim(ClaimTypes.Name, userDetail.R01F02));
                    identity.AddClaim(new Claim("Id", Convert.ToString(userDetail.R01F01)));

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
                    actionContext.Response = actionContext.Request
                            .CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid Credentials");
                }

            }
            catch (Exception)
            {
                actionContext.Response = actionContext.Request
                        .CreateErrorResponse(HttpStatusCode.InternalServerError,
                            "Internal Server Error - Please Try After Some Time");
            }
        }
    }
}