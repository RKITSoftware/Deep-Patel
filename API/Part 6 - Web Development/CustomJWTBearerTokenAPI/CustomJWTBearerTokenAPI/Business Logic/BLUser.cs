using CustomJWTBearerTokenAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace CustomJWTBearerTokenAPI.Business_Logic
{
    public class BLUser
    {
        // Sample list of users for demonstration purposes
        private static List<USR01> _lstUsers = new List<USR01>
        {
            new USR01 {R01F01 = 1, R01F02 = "deep", R01F03 = "deep2513", R01F04 = "Admin"},
            new USR01 {R01F01 = 2, R01F02 = "jeet", R01F03 = "jeet11234", R01F04 = "User"}
        };

        /// <summary>
        /// Gets a user based on the provided username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>The user if found, otherwise null.</returns>
        public static USR01 GetUser(string username, string password)
        {
            // Using LINQ to find the user based on the provided username and password
            return _lstUsers.FirstOrDefault(user =>
                        user.R01F02.Equals(username) && user.R01F03.Equals(password));
        }

        /// <summary>
        /// Gets a user based on the provided user ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user if found, otherwise null.</returns>
        internal static USR01 GetUser(int id)
        {
            // Using LINQ to find the user based on the provided user ID
            return _lstUsers.FirstOrDefault(user => user.R01F01 == id);
        }
    }
}
