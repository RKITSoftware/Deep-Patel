using OnlineShoopingApp.Models;
using System.Collections.Generic;

namespace OnlineShoopingApp.Helpers
{
    public class Helper
    {
        public static List<Cookie> SetCookiesFromHeader(string cookieHeader)
        {
            List<Cookie> cookies = new List<Cookie>();

            // Split the cookie header into individual cookies (they are separated by commas)
            string[] cookieParts = cookieHeader.Split(',');

            foreach (var cookiePart in cookieParts)
            {
                // Parse each individual cookie and create a Cookie object
                string[] cookieKeyValue = cookiePart.Trim().Split(';');
                string[] keyValue = cookieKeyValue[0].Trim().Split('=');

                if (keyValue.Length == 2)
                {
                    Cookie cookie = new Cookie
                    {
                        Name = keyValue[0].Trim(),
                        Value = keyValue[1].Trim()
                    };

                    // Add the cookie to the list
                    cookies.Add(cookie);
                }
            }

            return cookies;
        }
    }
}