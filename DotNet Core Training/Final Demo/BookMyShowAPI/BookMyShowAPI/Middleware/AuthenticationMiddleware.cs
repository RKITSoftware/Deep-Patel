using BookMyShowAPI.Interface;
using BookMyShowAPI.Model;
using Microsoft.AspNetCore.Authorization;

namespace BookMyShowAPI.Middleware
{
    /// <summary>
    /// For handling the process of Basic Authentication of this project.
    /// </summary>
    public class AuthenticationMiddleware : IMiddleware
    {
        /// <summary>
        /// AccountService for handling account related information like login, signin, password change etc.
        /// </summary>
        private readonly IAccountService _accountService;

        /// <summary>
        /// Initialize the <see cref="AuthenticationMiddleware"/> private fields and properties.
        /// </summary>
        /// <param name="accountService"></param>
        public AuthenticationMiddleware(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Checks the user is authenticated or not.
        /// </summary>
        /// <param name="context">Current request context</param>
        /// <param name="next">Next middlware reference</param>
        /// <returns>
        /// If user is authenticated then give it access else return and gives unauthorized response
        /// </returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // AllowAnonymous attribute checking
            bool allowAnonymous = context.GetEndpoint()
                .Metadata.Any(meta => meta.GetType() == typeof(AllowAnonymousAttribute));

            if (allowAnonymous)
            {
                await next(context);
                return;
            }

            // Basic Authentication Check
            string authHeader = context.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authHeader))
            {
                return;
            }

            bool isBasicAuthentictaion = authHeader.StartsWith("Basic");
            if (!isBasicAuthentictaion)
            {
                return;
            }

            string credentials = authHeader.Substring(6);
            USR01 objUser;

            // If user's credentials are valid then give access and execute further middlwares.
            if (_accountService.IsCredentialsValid(credentials, out objUser))
            {
                context.User = new System.Security.Claims.ClaimsPrincipal(
                        new System.Security.Claims.ClaimsIdentity(
                            new[] { new System.Security.Claims.Claim("Name", objUser.R01F02) },
                            "Basic"));
                await next(context);
            }
        }
    }
}
