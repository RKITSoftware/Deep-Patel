namespace EmployeeAPI.Models
{
    /// <summary>
    /// Employee class represents the model for storing information about an employee.
    /// </summary>
    public class Employee
    {
        #region Public Properties

        /// <summary>
        /// Employee Id to hold speicic employee identification.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Employee Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Employee Salary
        /// </summary>
        public decimal Salary { get; set; }

        /// <summary>
        /// Employee Role to Company
        /// </summary>
        public string Designation { get; set; }

        #endregion
    }
}
