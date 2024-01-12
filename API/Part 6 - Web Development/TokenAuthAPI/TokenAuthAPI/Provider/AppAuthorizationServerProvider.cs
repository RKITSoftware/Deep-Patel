using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;
using TokenAuthAPI.UserRepository;

namespace TokenAuthAPI.Provider
{
    /// <summary>
    /// Custom OAuth Authorization Server Provider for token-based authentication
    /// </summary>
    public class AppAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        #region Override Methods

        /// <summary>
        /// Method to validate client authentication
        /// </summary>
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Validate the client (In this case, client validation is skipped)
            context.Validated();
        }

        /// <summary>
        /// Method to grant resource owner credentials (authenticate the user)
        /// </summary>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // Create an instance of the UserRepo to interact with the user repository
            using (UserRepo repo = new UserRepo())
            {
                // Validate the user credentials
                var user = repo.ValidateUser(context.UserName, context.Password);

                // If the user is not found, return an error response
                if (user == null)
                {
                    context.SetError("invalid_grant", "Username or Password is incorrect.");
                    return;
                }

                // Create a ClaimsIdentity for the authenticated user
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

                // Add user roles as claims
                foreach (var role in user.Roles.Split(','))
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role.Trim()));
                }

                // Validate and authenticate the user by issuing an authentication ticket
                context.Validated(identity);
            }
        }

        #endregion
    }
}
