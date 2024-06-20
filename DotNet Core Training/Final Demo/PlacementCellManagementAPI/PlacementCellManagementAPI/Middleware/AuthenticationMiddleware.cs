using Microsoft.AspNetCore.Authorization;
using PlacementCellManagementAPI.Business_Logic.Interface;
using PlacementCellManagementAPI.Models.POCO;
using System.Security.Claims;
using System.Text;

namespace PlacementCellManagementAPI.Middleware
{
    /// <summary>
    /// Middleware responsible for authenticating private APIs.
    /// </summary>
    public class AuthenticationMiddleware : IMiddleware
    {
        #region Private Fields

        /// <summary>
        /// Instance of a <see cref="IUSR01Service"/> to get the methods for the authentication process.
        /// </summary>
        private readonly IUSR01Service _userService;

        #endregion Private Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the AuthenticationMiddleware class.
        /// </summary>
        /// <param name="userService">Service for user authentication.</param>
        public AuthenticationMiddleware(IUSR01Service userService)
        {
            _userService = userService;
        }

        #endregion Constructor

        #region Public Methods

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
            Endpoint? endpoint = context.GetEndpoint();

            if (endpoint != null)
            {
                // Check if the endpoint allows anonymous access
                bool allowAnonymous = endpoint.Metadata.Any(meta => meta.GetType() == typeof(AllowAnonymousAttribute));

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
                    if (_userService.CheckUser(username, password, out USR01 objUser))
                    {
                        // If valid, create claims for the user
                        Claim[] claims = new[]
                        {
                            new Claim(ClaimTypes.Name, objUser.R01F02 ?? string.Empty),
                            new Claim(ClaimTypes.Email, objUser.R01F03 ?? string.Empty),
                            new Claim(ClaimTypes.Role, objUser.R01F05 ?? string.Empty)
                        };

                        // Create identity and principal for the user
                        ClaimsIdentity claimsIdentity = new(claims, "Basic");
                        ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

                        // Set the current user's identity
                        context.User = claimsPrincipal;

                        // Call the next middleware
                        await next(context);
                    }
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

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

        #endregion Private Methods
    }
}