using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text;

namespace PlacementCellManagementAPI.Middleware
{
    /// <summary>
    /// Authenticate the API's which is private.
    /// </summary>
    public class AuthenticationMiddleware : IMiddleware
    {
        /// <summary>
        /// Checks the user is authenticated or not.
        /// </summary>
        /// <param name="context">Current request context.</param>
        /// <param name="next">Next middleware reference</param>
        /// <returns>
        /// Calls the next middleware if user is authenticate else gives 401 statuscode response
        /// </returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            bool allowAnonymous = context.GetEndpoint()
                .Metadata.Any(meta => meta.GetType() == typeof(AllowAnonymousAttribute));

            if (allowAnonymous)
            {
                await next(context);
            }
            else
            {
                string authHeaders = context.Request.Headers["Authorization"];

                if (string.IsNullOrEmpty(authHeaders) || !authHeaders.StartsWith("Basic "))
                {
                    context.Response.StatusCode = 401;
                    return;
                }

                Tuple<string, string> usernameAndPassword = GetUsernameAndPassword(authHeaders);

                string username = usernameAndPassword.Item1;
                string password = usernameAndPassword.Item2;

                if (username == "Admin" && password == "123")
                {
                    Claim[] claims = new[]
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role, "Admin")
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Basic");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    context.User = claimsPrincipal;

                    await next(context);
                }
            }
        }

        /// <summary>
        /// Decode the authentication header and get the username and password from it.
        /// </summary>
        /// <param name="authHeaders">Authorization header value.</param>
        /// <returns><see cref="Tuple{T1, T2}"/> where T1 is username and T2 is password.</returns>
        private Tuple<string, string> GetUsernameAndPassword(string authHeaders)
        {
            string credential = authHeaders.Split(' ')[1];

            string decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(credential));
            string[] usernameAndPassword = decodedCredentials.Split(':');

            return new Tuple<string, string>(usernameAndPassword[0], usernameAndPassword[1]);
        }
    }
}
