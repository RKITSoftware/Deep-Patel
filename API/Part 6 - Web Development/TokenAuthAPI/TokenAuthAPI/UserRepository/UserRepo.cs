using System;
using System.Collections.Generic;
using System.Linq;
using TokenAuthAPI.Models;

namespace TokenAuthAPI.UserRepository
{
    /// <summary>
    /// Repository class for managing user-related operations
    /// </summary>
    public class UserRepo : IDisposable
    {
        #region Public Properties

        // Static list to store user data (initialized with sample users)
        public static List<User> userList = User.GetUsers();

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to validate user credentials
        /// </summary>
        /// <param name="username">User's username</param>
        /// <param name="password">User's password</param>
        /// <returns></returns>
        public User ValidateUser(string username, string password)
        {
            // Find the user in the list whose username and password match the provided credentials
            return userList.FirstOrDefault(user =>
                user.UserName.Equals(username) && user.Password.Equals(password));
        }

        #endregion

        #region Code Cleanup

        /// <summary>
        /// Dispose method to clear the user list when the repository is disposed
        /// </summary>
        public void Dispose()
        {
            userList.Clear();
        }

        #endregion
    }
}
