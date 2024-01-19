using System;

namespace TokenAuthAPI.Models
{
    /// <summary>
    /// Model class representing an Employee
    /// </summary>
    public class EMP01
    {
        #region Public Properties

        /// <summary>
        /// Employee Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Employee First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Employee Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Employee Email Address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Employee Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Employee Created Date
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now; // Default value is set to the current date and time

        #endregion
    }
}
