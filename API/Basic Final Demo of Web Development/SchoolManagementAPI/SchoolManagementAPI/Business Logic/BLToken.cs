using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using SchoolManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web.Caching;

namespace SchoolManagementAPI.Business_Logic
{
    public class BLToken
    {
        // The secret key used for signing and validating JWT tokens
        private const string secretKey = "thisissecuritykeyofcustomjwttokenaut";

        /// <summary>
        /// Generates a JWT token for the provided user.
        /// </summary>
        /// <param name="objUser">The user for whom the token is generated.</param>
        /// <returns>The generated JWT token as a string.</returns>
        public HttpResponseMessage GenerateToken(Guid sessionId, USR01 objUser)
        {
            try
            {
                if (BLHelper.ServerCache.Get(objUser.R01F02) != null)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.BadRequest,
                        "User is already login");
                }

                TimeSpan slidingExpiration = new TimeSpan(0, 20, 0);

                BLHelper.ServerCache.Add(objUser.R01F02,
                    sessionId.ToString(),
                    null,
                    DateTime.MaxValue,
                    slidingExpiration,
                    CacheItemPriority.Normal,
                    null);

                string issuer = "CustomJWTBearerTokenAPI";

                // Creating SymmetricSecurityKey and SigningCredentials
                SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(secretKey));

                SigningCredentials credentials = new SigningCredentials(symmetricSecurityKey,
                    SecurityAlgorithms.HmacSha256);

                // Creating claims for the user
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, sessionId.ToString()),
                    new Claim("Id", objUser.R01F01.ToString())
                };

                // Creating JWT token with claims and signing credentials
                JwtSecurityToken token = new JwtSecurityToken(issuer, issuer, claims,
                    expires: null,
                    signingCredentials: credentials);

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                string jwtToken = handler.WriteToken(token);

                CookieHeaderValue cookieSessionId =
                    new CookieHeaderValue("Session-Id", sessionId.ToString())
                    {
                        Path = "/",
                        HttpOnly = true,
                    };

                BLHelper.ServerCache.Add(sessionId.ToString(),
                                         value: jwtToken,
                                         dependencies: null,
                                         absoluteExpiration: DateTime.MaxValue,
                                         slidingExpiration: slidingExpiration,
                                         priority: CacheItemPriority.Normal,
                                         onRemoveCallback: null);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Logged in")
                };
                response.Headers.AddCookies(new CookieHeaderValue[] { cookieSessionId });

                return response;
            }
            catch (Exception ex)
            {
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occured during generating token.");
            }
        }

        /// <summary>
        /// Gets the IPrincipal object from the provided JWT token.
        /// </summary>
        /// <param name="token">The JWT token.</param>
        /// <returns>The IPrincipal object representing the user.</returns>
        public static IPrincipal GetPrincipal(string token)
        {
            // Extracting and decoding the payload from the JWT token
            string jwtEncodePayload = token.Split('.')[1];
            int mod = jwtEncodePayload.Length % 4;
            int padding = mod > 0 ? 4 - mod : 0;
            jwtEncodePayload = jwtEncodePayload + new string('=', padding);

            try
            {
                string decodedPayloadBytes = Encoding.UTF8.GetString(
                    Convert.FromBase64String(jwtEncodePayload));
                JObject json = JObject.Parse(decodedPayloadBytes);

                // Retrieving user information based on the decoded payload
                USR01 user = BLHelper.GetUser(int.Parse(json["Id"].ToString()));

                // Creating GenericIdentity and GenericPrincipal objects for the user
                GenericIdentity identity = new GenericIdentity(user.R01F02);
                IPrincipal principal = new GenericPrincipal(identity, user.R01F04.Split(','));

                return principal;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Validates if the provided JWT token is valid and not expired.
        /// </summary>
        /// <param name="jwt">The JWT token to validate.</param>
        /// <returns>True if the token is valid and not expired, false otherwise.</returns>
        public static bool IsJwtValid(string token)
        {
            // Splitting the JWT token into header, payload, and hash parts
            string[] jwtArray = token.Split('.');
            string jwtHeader = jwtArray[0];
            string jwtPayload = jwtArray[1];
            string jwtHash = jwtArray[2];

            string payload = jwtHeader + "." + jwtPayload;

            // Calculating HMAC-SHA-256 for the header and payload
            var hash = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            byte[] digest = hash.ComputeHash(Encoding.UTF8.GetBytes(payload));

            string digestBase64 = Convert.ToBase64String(digest)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");

            // If JWT hash matches the calculated hash, check for expiry time
            if (jwtHash.Equals(digestBase64))
            {
                // If not expired, return true; otherwise, return false

                // Getting current time
                TimeSpan span = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
                long currTotalSecond = (long)span.TotalSeconds;

                // Decoding jwtPayload from Base64
                int mod = jwtPayload.Length % 4;
                int padding = mod > 0 ? 4 - mod : 0;

                string paddedJwtPayload = jwtPayload + new string('=', padding);
                byte[] encodedData = Convert.FromBase64String(paddedJwtPayload);
                string decodedData = Encoding.UTF8.GetString(encodedData);

                // Deserializing the jwtPayload decoded string
                var jwtPayloadObj = JwtPayload.Deserialize(decodedData);

                // Getting exp (expiry) claim
                object expiryTotalSecond;
                jwtPayloadObj.TryGetValue("exp", out expiryTotalSecond);

                long exp = (long)expiryTotalSecond;

                // Comparing expiry and current time
                if (exp >= currTotalSecond)
                {
                    return true;
                }
            }
            return false;
        }

        public HttpResponseMessage LogOut(string username)
        {
            try
            {
                // Generate a response for logout by expiring the authentication token cookie.
                HttpResponseMessage response = BLHelper.ResponseMessage(HttpStatusCode.OK,
                    "Successfully logout.");

                CookieHeaderValue expiredCookie = new CookieHeaderValue("Session-Id", "")
                {
                    Expires = DateTime.Now.AddMinutes(-1),
                    Path = "/",
                };

                response.Headers.AddCookies(new CookieHeaderValue[] { expiredCookie });

                string sessionID = (string)BLHelper.ServerCache.Get(username);

                if (sessionID != null)
                {
                    BLHelper.ServerCache.Remove(sessionID);
                    BLHelper.ServerCache.Remove(username);
                }

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
    }
}