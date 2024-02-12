using SchoolManagementAPI.Business_Logic;
using SchoolManagementAPI.Models;
using System.Linq;

namespace SchoolManagementAPI.Validation
{
    /// <summary>
    /// Helper Class to Authenticate user details
    /// </summary>
    public class ValidateUser
    {
        #region Helper Methods

        /// <summary>
        /// Check if the provided username and password match any user in the userList.
        /// </summary>
        /// <param name="username">User's username for authentication</param>
        /// <param name="password">User's password for authentication</param>
        /// <returns>True if user is present in database.</returns>
        internal static bool CheckUser(string username, string password)
        {
            // Using LINQ to check if there's any user with the provided username and password.
            return BLUser.lstUser.Any(user => user.R01F02.Equals(username) &&
                user.R01F03.Equals(password));
        }

        /// <summary>
        /// Retrieve user details based on the provided username and password.
        /// </summary>
        /// <param name="username">User's username for authentication</param>
        /// <param name="password">User's password for authentication</param>
        /// <returns>User by using username and password</returns>
        internal static USR01 GetUserDetail(string username, string password)
        {
            // Using LINQ to find the user with the provided username and password.
            return BLUser.lstUser.Find(user => user.R01F02.Equals(username) &&
                user.R01F03.Equals(password));
        }

        #endregion
    }
}
