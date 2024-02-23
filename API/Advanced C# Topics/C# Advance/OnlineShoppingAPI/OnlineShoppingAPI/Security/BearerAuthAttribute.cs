using OnlineShoppingAPI.Business_Logic;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OnlineShoppingAPI.Security
{
    public class BearerAuthAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                // Getting Cookie Value
                CookieHeaderValue cookieJwtToken = actionContext.Request.Headers
                    .GetCookies().FirstOrDefault();

                if (cookieJwtToken["Token"] == null || cookieJwtToken["Token"].Value == "")
                {
                    actionContext.Response = BLHelper.ResponseMessage(
                        HttpStatusCode.Unauthorized, "Please provide token");
                    return;
                }

                string token = cookieJwtToken["Token"].Value;

                if (!BLToken.IsJwtValid(token))
                {
                    actionContext.Response = BLHelper.ResponseMessage(
                        HttpStatusCode.Unauthorized, "Alteration with the Token");
                    return;
                }

                CookieHeaderValue cookieLoginSessionId = actionContext.Request.Headers
                    .GetCookies("LoginSessionId").FirstOrDefault();
                string sessionId = cookieLoginSessionId["LoginSessionId"]?.Value;

                string jwtToken = (string)BLHelper.ServerCache.Get(sessionId);

                if (jwtToken == null)
                {
                    actionContext.Response = BLHelper.ResponseMessage(
                        HttpStatusCode.Unauthorized, "Login expired, please login.");
                    return;
                }

                if (jwtToken != token)
                {
                    actionContext.Response = BLHelper.ResponseMessage(
                        HttpStatusCode.Unauthorized, "Token Modificication Error");
                    return;
                }

                IPrincipal principal = BLToken.GetPrincipal(jwtToken);

                if (principal == null)
                {
                    actionContext.Response = BLHelper.ResponseMessage(
                        HttpStatusCode.Unauthorized, "Unauthorized user.");
                    return;
                }

                HttpContext.Current.User = principal;
            }
            catch (Exception ex)
            {
                BLHelper.LogError(ex);
                actionContext.Response = BLHelper.ResponseMessage(HttpStatusCode.BadRequest,
                    "An error occured during authentication");
            }
        }
    }
}