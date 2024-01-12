using System.Collections.Generic;

namespace BasicAuthAPI.Models
{
    /// <summary>
    /// Model class representing a user with authentication and authorization details
    /// </summary>
    public class User
    {
        #region Public Properties

        /// <summary>
        /// Unique identifier for the user
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User's username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User's password (Note: In a real-world scenario, passwords should be securely hashed and stored)
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User's roles, separated by commas
        /// </summary>
        public string Roles { get; set; }

        /// <summary>
        /// User's email address
        /// </summary>
        public string Email { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Static method to get a list of sample users
        /// </summary>
        /// <returns>List of Users</returns>
        public static List<User> GetUsers()
        {
            return new List<User>()
            {
                new User { Id = 1001, UserName = "NormalUser", Password = "12345", Roles = "User", Email = "user@email.com" },
                new User { Id = 1002, UserName = "AdminUser", Password = "12345", Roles = "User,Admin", Email = "user@email.com" },
                new User { Id = 1003, UserName = "SuperAdminUser", Password = "12345", Roles = "User,Admin,SuperAdmin", Email = "user@email.com" }
            };
        }

        #endregion
    }
}
