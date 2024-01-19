using System.Collections.Generic;

namespace BasicAuthAPI.Models
{
    /// <summary>
    /// Model class representing a user with authentication and authorization details
    /// </summary>
    public class USR01
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
    }
}
