using System.Collections.Generic;

namespace TokenAuthAPI.Models
{
    /// <summary>
    /// Model class representing a User
    /// </summary>
    public class USR01
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
    }
}
