using System.Collections.Generic;

namespace TokenAuthAPI.Models
{
    /// <summary>
    /// Model class representing a User
    /// </summary>
    public class User
    {
        #region Public Properties

        /// <summary>
        /// User Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User's Username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User's Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User's Roles
        /// </summary>
        public string Roles { get; set; }

        /// <summary>
        /// User Email Address
        /// </summary>
        public string Email { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Static method to get a list of sample users
        /// </summary>
        /// <returns>List of Users</returns>
        public static List<User> GetUsers()
        {
            // Return a list of User objects with sample data
            return new List<User>
            {
                new User { UserId = 1, UserName ="deep", Password = "1234", Roles = "Admin,SuperAdmin", Email = "deeppatel@gmail.com"},
                new User { UserId = 2, UserName ="jeet", Password = "1234", Roles = "Admin", Email = "jeetsorathiya@gmail.com"},
                new User { UserId = 3, UserName ="prajval", Password = "1234", Roles = "User", Email = "prajvalgahine@gmail.com"},
                new User { UserId = 4, UserName ="krinsi", Password = "1234", Roles = "User", Email = "krinsikayada@gmail.com"}
            };
        }

        #endregion
    }
}
