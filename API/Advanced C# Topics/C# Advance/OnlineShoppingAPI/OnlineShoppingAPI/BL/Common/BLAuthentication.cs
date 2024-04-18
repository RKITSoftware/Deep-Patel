using OnlineShoppingAPI.BL.Master.Interface;
using OnlineShoppingAPI.BL.Master.Service;
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
        #region Private Fields

        /// <summary>
        /// User services for the authentication.
        /// </summary>
        private readonly IUSR01Service _usr01Service;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor to initialize the instance of <see cref="BLAuthentication"/>.
        /// </summary>
        public BLAuthentication()
        {
            _usr01Service = new BLUSR01Handler();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a <see cref="HttpResponseMessage"/> that contains the cookies for basic authentication.
        /// </summary>
        /// <param name="username">The username of user.</param>
        /// <param name="password">The password of user.</param>
        /// <returns><see cref="HttpResponseMessage"/> with cookies header.</returns>
        public HttpResponseMessage LogIn(string username, string password)
        {
            if (_usr01Service.IsExist(username, password))
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

        /// <summary>
        /// Removes the authentication cookies from the user's browser.
        /// </summary>
        /// <returns><see cref="HttpResponseMessage"/> containing the authentication
        /// cookie with expire time -1.</returns>
        public HttpResponseMessage LogOut()
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

        #endregion Public Methods
    }
}