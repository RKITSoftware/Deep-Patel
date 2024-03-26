using BookMyShowAPI.Interface;
using BookMyShowAPI.Model;
using Microsoft.AspNetCore.Authorization;

namespace BookMyShowAPI.Middleware
{
    public class AuthenticationMiddleware : IMiddleware
    {
        private readonly IAccountService _accountService;

        public AuthenticationMiddleware(IAccountService accountService)
        {
            _accountService = accountService;
        }

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
}
