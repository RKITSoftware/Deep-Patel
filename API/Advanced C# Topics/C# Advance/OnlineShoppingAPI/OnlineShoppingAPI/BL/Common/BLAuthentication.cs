using OnlineShoppingAPI.Extension;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace OnlineShoppingAPI.BL.Common
{
    /// <summary>
    /// <see cref="BLAuthentication"/> for handling authentication realted api's business logic.
    /// </summary>
    public class BLAuthentication
    {
        #region Public Methods

        /// <summary>
        /// Creates a <see cref="HttpResponseMessage"/> that contains the cookies for basic authentication.
        /// </summary>
        /// <param name="username">The username of user.</param>
        /// <param name="password">The password of user.</param>
        /// <returns><see cref="HttpResponseMessage"/> with cookies header.</returns>
        public HttpResponseMessage LogIn(string username, string password)
        {
            try
            {
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
                ex.LogException();
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred during login.");
            }
        }

        /// <summary>
        /// Removes the authentication cookies from the user's browser.
        /// </summary>
        /// <returns><see cref="HttpResponseMessage"/> containing the authentication 
        /// cookie with expire time -1.</returns>
        public HttpResponseMessage LogOut()
        {
            try
            {
                // Generate a response for logout by expiring the authentication token cookie.
                HttpResponseMessage response = BLHelper.ResponseMessage(HttpStatusCode.OK,
                    "Successfully logout.");

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
