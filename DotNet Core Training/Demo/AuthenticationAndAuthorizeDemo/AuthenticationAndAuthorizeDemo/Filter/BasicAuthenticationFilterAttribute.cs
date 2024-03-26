using AuthenticationAndAuthorizeDemo.Business_Logic;
using AuthenticationAndAuthorizeDemo.Helper;
using AuthenticationAndAuthorizeDemo.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthenticationAndAuthorizeDemo.Filter
{
    public class BasicAuthenticationFilterAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool allowAnnoymous = context.ActionDescriptor.EndpointMetadata
                .Any(meta => meta.GetType() == typeof(AllowAnonymousAttribute));

            if (allowAnnoymous)
            {
                return;
            }

            string authHeader = context.HttpContext.Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                // Extract credentials
                string encodedCredentials = authHeader.Substring("Basic ".Length).Trim();

                Tuple<string, string> usernameAndPassword =
                    AuthenticationHelper.ExtractUserNameAndPassword(encodedCredentials);

                string username = usernameAndPassword.Item1;
                string password = usernameAndPassword.Item2;

                // Get the list of users.
                List<USR01> _lstUsers = BLUser.GetUsers();

                // Find user with matching credentials.
                USR01? objUser = _lstUsers.FirstOrDefault(u =>
                    u.R01F02.Equals(username) && u.R01F03.Equals(password));

                // If user not found, authentication fails.
                if (objUser == null)
                {
                    context.Result = new NotFoundResult();
                }

                // Set user for the request
                context.HttpContext.User = new System.Security.Claims.ClaimsPrincipal(
                    new System.Security.Claims.ClaimsIdentity(
                        new[] { new System.Security.Claims.Claim("Name", username) },
                        "Basic"));
                return;
            }
            else
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
