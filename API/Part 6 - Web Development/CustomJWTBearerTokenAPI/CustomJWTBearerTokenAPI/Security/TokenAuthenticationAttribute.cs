using CustomJWTBearerTokenAPI.Business_Logic;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace CustomJWTBearerTokenAPI.Security
{
    /// <summary>
    /// Custom authentication attribute for validating and processing JWT tokens.
    /// </summary>
    public class TokenAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        /// <summary>
        /// Indicates whether the attribute supports multiple authentication.
        /// </summary>
        public bool AllowMultiple => false;

        /// <summary>
        /// Authenticates the request by validating the provided JWT token.
        /// </summary>
        /// <param name="context">The authentication context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var authHeader = context.Request.Headers.Authorization;

            // Check if Authorization header is present
            if (authHeader == null)
            {
                context.ErrorResult = ErrorResponse(HttpStatusCode.BadRequest, "Please provide jwt token.");
                return Task.CompletedTask;
            }

            // Check if the authentication method is "Bearer"
            if (authHeader.Scheme != "Bearer")
            {
                context.ErrorResult = ErrorResponse(HttpStatusCode.BadRequest, "Incorrect authentication method.");
                return Task.CompletedTask;
            }

            // Validate the JWT token
            bool isValidToken = BLToken.IsJwtValid(authHeader.Parameter);
            if (!isValidToken)
            {
                context.ErrorResult = ErrorResponse(HttpStatusCode.Unauthorized, "Jwt token is invalid or expired.");
                return Task.CompletedTask;
            }

            // Set the user principal in the current HttpContext
            HttpContext.Current.User = BLToken.GetPrincipal(authHeader.Parameter);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Challenges the request by adding the necessary authentication headers.
        /// </summary>
        /// <param name="context">The authentication challenge context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Creates an error response message with the specified status code and message.
        /// </summary>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="message">The error message.</param>
        /// <returns>The ResponseMessageResult containing the error response.</returns>
        private ResponseMessageResult ErrorResponse(HttpStatusCode statusCode, string message)
        {
            return new ResponseMessageResult(new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(message)
            });
        }
    }
}
