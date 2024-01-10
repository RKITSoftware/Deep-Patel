using System.Collections.Generic;

namespace BasicAuthAPI.Models
{
    /// <summary>
    /// Employee class represents the model for storing information about an employee.
    /// </summary>
    public class Employee
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

        #region Public Methods

        /// <summary>
        /// GetEmployees method returns a list of sample employees for demonstration purposes.
        /// </summary>
        /// <returns>List of employees</returns>
        public static List<Employee> GetEmployees()
        {
            // Create a list of Employee objects with sample data.
            List<Employee> employees = new List<Employee>()
            {
                new Employee { Id = 1, FirstName = "Deep", LastName = "Patel", Gender = "Male", City = "Limbdi", IsActive = true},
                new Employee { Id = 2, FirstName = "Prajval", LastName = "Gahine", Gender = "Male", City = "Surat", IsActive = true},
                new Employee { Id = 3, FirstName = "Vishal", LastName = "Gohil", Gender = "Male", City = "Bhavnagar", IsActive = true}
            };

            // Return the list of employees.
            return employees;
        }

        #endregion
    }
}