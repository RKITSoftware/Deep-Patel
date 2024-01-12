using System;
using System.Collections.Generic;

namespace TokenAuthAPI.Models
{
    /// <summary>
    /// Model class representing an Employee
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

        #region Public Methods

        /// <summary>
        /// Static method to get a list of sample employees
        /// </summary>
        /// <returns>List of Employees</returns>
        public static List<Employee> GetEmployees()
        {
            // Return a list of Employee objects with sample data
            return new List<Employee>()
            {
                new Employee{Id = 1, FirstName = "Deep", LastName= "Patel", Email="deeppatel@gmail.com", Gender = "Male"},
                new Employee{Id = 2, FirstName = "Jeet", LastName= "Sorathiya", Email="jeetsorathiya@gmail.com", Gender = "Male"},
                new Employee{Id = 3, FirstName = "Prajval", LastName= "Gahine", Email="prajvalgahine@gmail.com", Gender = "Male"},
                new Employee{Id = 4, FirstName = "Krinsi", LastName= "Kayada", Email="krinsikayada@gmail.com", Gender = "Feale"},
                new Employee{Id = 5, FirstName = "Prince", LastName= "Goswami", Email="princegoswami@gmail.com", Gender = "Male"}
            };
        }

        #endregion
    }
}
