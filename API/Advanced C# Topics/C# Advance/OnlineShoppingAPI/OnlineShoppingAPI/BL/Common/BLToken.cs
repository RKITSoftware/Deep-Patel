using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using OnlineShoppingAPI.BL.Master.Interface;
using OnlineShoppingAPI.BL.Master.Service;
using OnlineShoppingAPI.Models.POCO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace OnlineShoppingAPI.BL.Common
{
    /// <summary>
    /// Business Logic for JWT Token related functionalities.
    /// </summary>
    public class BLToken
    {
        #region Private Fields

        /// <summary>
        /// The secret key used for signing and validating JWT tokens
        /// </summary>
        private const string secretKey = "thisissecuritykeyofcustomjwttokenaut";

        #endregion Private Fields

        #region Public Methods

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
            jwtEncodePayload += new string('=', padding);

            string decodedPayloadBytes = Encoding.UTF8.GetString(
                Convert.FromBase64String(jwtEncodePayload));
            JObject json = JObject.Parse(decodedPayloadBytes);

            IUSR01Service usr01Service = new BLUSR01Handler();
            USR01 user = usr01Service.GetUser(int.Parse(json["Id"].ToString()));

            // Creating GenericIdentity and GenericPrincipal objects for the user
            GenericIdentity identity = new GenericIdentity(user.R01F02);
            IPrincipal principal = new GenericPrincipal(identity, user.R01F04.Split(','));

            return principal;
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
            HMACSHA256 hash = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            byte[] digest = hash.ComputeHash(Encoding.UTF8.GetBytes(payload));

            string digestBase64 = Convert.ToBase64String(digest)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");

            // If JWT hash matches the calculated hash, check for expiry time
            if (jwtHash.Equals(digestBase64))
            {
                // Getting current time in seconds
                TimeSpan span = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
                long currTotalSecond = (long)span.TotalSeconds;

                // Decoding jwtPayload from Base64
                int mod = jwtPayload.Length % 4;
                int padding = mod > 0 ? 4 - mod : 0;

                string paddedJwtPayload = jwtPayload + new string('=', padding);
                byte[] encodedData = Convert.FromBase64String(paddedJwtPayload);
                string decodedData = Encoding.UTF8.GetString(encodedData);

                // Deserializing the jwtPayload decoded string
                JwtPayload jwtPayloadObj = JwtPayload.Deserialize(decodedData);

                // Getting exp (expiry) claim
                jwtPayloadObj.TryGetValue("exp", out object expiryTotalSecond);

                long exp = (long)expiryTotalSecond;

                // Comparing expiry and current time
                if (exp >= currTotalSecond)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Generates a JWT Token for the specified user.
        /// </summary>
        /// <param name="sessionId">Session id for one user login authentication.</param>
        /// <param name="objUser">Contains the information of the user.</param>
        /// <returns>
        /// <see cref="HttpResponseMessage"/> containing the JWT Token with other session
        /// related tokens in Cookie.
        /// </returns>
        public HttpResponseMessage GenerateToken(Guid sessionId, USR01 objUser)
        {
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
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string jwtToken = handler.WriteToken(token);

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jwtToken)
            };

            return response;
        }

        #endregion Public Methods
    }
}