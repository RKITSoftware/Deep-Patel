using FilterDemo.Business_Logic;
using FilterDemo.Helper;
using FilterDemo.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace FilterDemo.Security
{
    /// <summary>
    /// Handler for basic authentication scheme.
    /// </summary>
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        /// <summary>
        /// Initializes a new instance of the BasicAuthHandler class.
        /// </summary>
        /// <param name="options">The authentication scheme options.</param>
        /// <param name="logger">The logger factory.</param>
        /// <param name="encoder">The URL encoder.</param>
        /// <param name="clock">The system clock.</param>
        public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        /// <summary>
        /// Handles the authentication process.
        /// </summary>
        /// <returns>An authentication result.</returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Console.WriteLine("Authentication");

            // Checking if currently executing action have AllowAnonymous Attribute or not.
            Endpoint? endpoints = Context.GetEndpoint();

            if (endpoints is not null &&
                endpoints.Metadata.Any(meta => meta.GetType() == typeof(AllowAnonymousAttribute)))
            {
                return AuthenticateResult.NoResult();
            }

            // Check if Authorization header exists.
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Authorization header missing");

            // Extract username and password from Authorization header.
            string header = Request.Headers["Authorization"].ToString();
            string encodedUsernameAndPassword = header.Substring(6);
            Tuple<string, string> credentials =
                AuthenticationHelper.ExtractUserNameAndPassword(encodedUsernameAndPassword);

            string username = credentials.Item1;
            string password = credentials.Item2;

            // Get the list of users.
            List<USR01> _lstUsers = BLUser.GetUsers();

            // Find user with matching credentials.
            USR01? objUser = _lstUsers.FirstOrDefault(u =>
                u.R01F02.Equals(username) && u.R01F03.Equals(password));

            // If user not found, authentication fails.
            if (objUser == null)
                return AuthenticateResult.Fail("Invalid username or password");

            // Create claims for the authenticated user.
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, objUser.R01F04)
            };

            // Create identity and principal.
            ClaimsIdentity identity = new ClaimsIdentity(claims, Scheme.Name);
            ClaimsPrincipal principal1 = new ClaimsPrincipal(identity);
            AuthenticationTicket ticket = new AuthenticationTicket(principal1, Scheme.Name);

            // Authentication successful.
            return AuthenticateResult.Success(ticket);
        }
    }
}
