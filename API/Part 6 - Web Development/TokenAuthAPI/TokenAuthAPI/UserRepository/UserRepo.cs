using System;
using System.Collections.Generic;
using System.Linq;
using TokenAuthAPI.Models;

namespace TokenAuthAPI.UserRepository
{
    /// <summary>
    /// Repository class for managing user-related operations
    /// </summary>
    public class UserRepo
    {
        public static List<USR01> lstUsers = new List<USR01>
            {
                new USR01 { UserId = 1, UserName ="deep", Password = "1234", Roles = "Admin,SuperAdmin", Email = "deeppatel@gmail.com"},
                new USR01 { UserId = 2, UserName ="jeet", Password = "1234", Roles = "Admin", Email = "jeetsorathiya@gmail.com"},
                new USR01 { UserId = 3, UserName = "prajval", Password = "1234", Roles = "User", Email = "prajvalgahine@gmail.com" },
                new USR01 { UserId = 4, UserName = "krinsi", Password = "1234", Roles = "User", Email = "krinsikayada@gmail.com" }
            };

        #region Public Methods

        /// <summary>
        /// Method to validate user credentials
        /// </summary>
        /// <param name="username">User's username</param>
        /// <param name="password">User's password</param>
        /// <returns></returns>
        public static USR01 ValidateUser(string username, string password)
        {
            // Find the user in the list whose username and password match the provided credentials
            return lstUsers.Find(user => user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password); ;
        }

        #endregion
    }
}
