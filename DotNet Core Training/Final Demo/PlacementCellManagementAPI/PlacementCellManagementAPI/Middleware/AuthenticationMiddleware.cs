using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text;

namespace PlacementCellManagementAPI.Middleware
{
    /// <summary>
    /// Middleware responsible for authenticating private APIs.
    /// </summary>
    public class AuthenticationMiddleware : IMiddleware
    {
        /// <summary>
        /// Invokes the middleware to check user authentication.
        /// </summary>
        /// <param name="context">Current request context.</param>
        /// <param name="next">Reference to the next middleware.</param>
        /// <returns>
        /// Calls the next middleware if user is authenticated, else gives a 401 status code response.
        /// </returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Check if the endpoint allows anonymous access
            bool allowAnonymous = context.GetEndpoint()
                .Metadata.Any(meta => meta.GetType() == typeof(AllowAnonymousAttribute));

            if (allowAnonymous)
            {
                await next(context);
            }
            else
            {
                // Check if Authorization header exists and is of type Basic
                string authHeaders = context.Request.Headers["Authorization"];

                if (string.IsNullOrEmpty(authHeaders) || !authHeaders.StartsWith("Basic "))
                {
                    // Return 401 Unauthorized if Authorization header is missing or not Basic
                    context.Response.StatusCode = 401;
                    return;
                }

                // Extract username and password from Authorization header
                Tuple<string, string> usernameAndPassword = GetUsernameAndPassword(authHeaders);

                string username = usernameAndPassword.Item1;
                string password = usernameAndPassword.Item2;

                // Validate username and password
                if (username == "Admin" && password == "123")
                {
                    // If valid, create claims for the user
                    Claim[] claims = new[]
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role, "Admin")
                    };

                    // Create identity and principal for the user
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Basic");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    // Set the current user's identity
                    context.User = claimsPrincipal;

                    // Call the next middleware
                    await next(context);
                }
            }
        }

        /// <summary>
        /// Decode the authentication header and extract the username and password from it.
        /// </summary>
        /// <param name="authHeaders">Authorization header value.</param>
        /// <returns>A tuple containing the username and password.</returns>
        private Tuple<string, string> GetUsernameAndPassword(string authHeaders)
        {
            string credential = authHeaders.Split(' ')[1];

            string decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(credential));
            string[] usernameAndPassword = decodedCredentials.Split(':');

            return new Tuple<string, string>(usernameAndPassword[0], usernameAndPassword[1]);
        }
    }
}
