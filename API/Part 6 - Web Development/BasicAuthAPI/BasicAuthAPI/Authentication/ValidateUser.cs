using BasicAuthAPI.Business_Logic;
using BasicAuthAPI.Models;
using System.Linq;

namespace BasicAuthAPI.Authentication
{
    /// <summary>
    /// ValidateUser class provides a simple method for user authentication.
    /// </summary>
    public class ValidateUser
    {
        #region Public Methods

        /// <summary>
        /// LogIn method checks if the provided username and password are valid for authentication.
        /// For demonstration purposes, it currently allows access only for the username "admin" with the password "password".
        /// </summary>
        /// <param name="userName">username passed by API headers</param>
        /// <param name="password">password passed by API headers</param>
        /// <returns>True is user is valid else false</returns>
        public static bool LogIn(string userName, string password)
        {
            return BLUser.GetUsers().Any(user => user.UserName.Equals(userName) && user.Password.Equals(password));
        }

        // Get the user details
        public static USR01 GetUserDetails(string userName, string password)
        {
            return BLUser.GetUsers().FirstOrDefault(user => user.UserName.Equals(userName) && user.Password.Equals(password));
        }

        #endregion
    }
}
