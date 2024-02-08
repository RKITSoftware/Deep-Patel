using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace OnlineShoppingAPI.Business_Logic
{
    public class BLAuthentication
    {
        internal HttpResponseMessage LogIn(string username, string password)
        {
            if (BLUser.IsExist(username, password))
            {
                string encodedAuthToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + password));
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                CookieHeaderValue cookie = new CookieHeaderValue("MyAuth", encodedAuthToken)
                {
                    Expires = DateTime.Now.AddMinutes(20),
                    Path = "/"
                };

                response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
                return response;
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        }

        internal HttpResponseMessage LogOut()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            CookieHeaderValue cookie = new CookieHeaderValue("MyAuth", "")
            {
                Expires = DateTime.Now.AddMinutes(-1),
                Path = "/"
            };

            response.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            return response;
        }
    }
}