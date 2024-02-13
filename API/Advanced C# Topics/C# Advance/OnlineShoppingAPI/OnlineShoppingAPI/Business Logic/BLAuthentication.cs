using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace OnlineShoppingAPI.Business_Logic
{
    /// <summary>
    /// Business Logic class handling user authentication operations.
    /// </summary>
    public class BLAuthentication
    {
        /// <summary>
        /// Validates user credentials and generates an authentication token on successful login.
        /// </summary>
        /// <param name="username">The username for login.</param>
        /// <param name="password">The password for login.</param>
        /// <returns>
        /// HttpResponseMessage indicating the success or failure of the login attempt,
        /// including an authentication token in the response headers on success.
        /// </returns>
        internal HttpResponseMessage LogIn(string username, string password)
        {
            // Validate user credentials using the IsExist method from BLUser.
            if (BLUser.IsExist(username, password))
            {
                // Generate an authentication token and set it as a cookie in the response.
                string encodedAuthToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

                var response = new HttpResponseMessage(HttpStatusCode.OK);
                var cookie = new CookieHeaderValue("MyAuth", encodedAuthToken)
                {
                    Expires = DateTime.Now.AddMinutes(20),
                    Path = "/"
                };

                response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                return response;
            }

            // Return a NotFound response if the user credentials are not valid.
            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Handles user logout by expiring the authentication token.
        /// </summary>
        /// <returns>
        /// HttpResponseMessage indicating the success of the logout attempt,
        /// including an expired authentication token in the response headers.
        /// </returns>
        internal HttpResponseMessage LogOut()
        {
            // Generate a response for logout by expiring the authentication token cookie.
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            var expiredCookie = new CookieHeaderValue("MyAuth", "")
            {
                Expires = DateTime.Now.AddMinutes(-1),
                Path = "/"
            };

            response.Headers.AddCookies(new CookieHeaderValue[] { expiredCookie });
            return response;
        }
    }
}
