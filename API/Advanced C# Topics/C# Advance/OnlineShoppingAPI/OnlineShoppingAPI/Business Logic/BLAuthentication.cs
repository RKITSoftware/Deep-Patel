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
        #region Public Methods

        /// <summary>
        /// Validates user credentials and generates an authentication token on successful login.
        /// </summary>
        /// <param name="username">The username for login.</param>
        /// <param name="password">The password for login.</param>
        /// <returns>
        /// HttpResponseMessage indicating the success or failure of the login attempt,
        /// including an authentication token in the response headers on success.
        /// </returns>
        public HttpResponseMessage LogIn(string username, string password)
        {
            try
            {
                // Validate user credentials using the IsExist method from BLUser.
                if (BLHelper.IsExist(username, password))
                {
                    // Generate an authentication token and set it as a cookie in the response.
                    string encodedAuthToken = Convert.ToBase64String(
                        Encoding.UTF8.GetBytes($"{username}:{password}"));

                    HttpResponseMessage response = BLHelper.ResponseMessage(HttpStatusCode.OK,
                        $"{username} successfully login.");

                    CookieHeaderValue cookie = new CookieHeaderValue("MyAuth", encodedAuthToken)
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
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred during login.");
            }
        }

        /// <summary>
        /// Handles user logout by expiring the authentication token.
        /// </summary>
        /// <returns>
        /// HttpResponseMessage indicating the success of the logout attempt,
        /// including an expired authentication token in the response headers.
        /// </returns>
        public HttpResponseMessage LogOut()
        {
            try
            {
                // Generate a response for logout by expiring the authentication token cookie.
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Successfully logout.")
                };

                CookieHeaderValue expiredCookie = new CookieHeaderValue("MyAuth", "")
                {
                    Expires = DateTime.Now.AddMinutes(-1),
                    Path = "/"
                };

                response.Headers.AddCookies(new CookieHeaderValue[] { expiredCookie });
                return response;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred during logout.");
            }
        }

        #endregion
    }
}
