namespace BasicAuthAPI.Models
{
    /// <summary>
    /// Employee class represents the model for storing information about an employee.
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
        /// Employee Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Employee City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Employee is login to account or not.
        /// </summary>
        public bool IsActive { get; set; }

        #endregion
    }
}