using BasicAuthAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace BasicAuthAPI.Business_Logic
{
    public class BLEmployee
    {
        #region Private Fields

        /// <summary>
        /// Stores the employee information in list
        /// </summary>
        private static List<EMP01> lstEmployee;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize employee information to list
        /// </summary>
        static BLEmployee()
        {
            lstEmployee = new List<EMP01>()
            {
                new EMP01 { Id = 1, FirstName = "Deep", LastName = "Patel", Gender = "Male", City = "Limbdi", IsActive = true},
                new EMP01 { Id = 2, FirstName = "Prajval", LastName = "Gahine", Gender = "Male", City = "Surat", IsActive = true},
                new EMP01 { Id = 3, FirstName = "Vishal", LastName = "Gohil", Gender = "Male", City = "Bhavnagar", IsActive = true},
                new EMP01 { Id = 4, FirstName = "Krinsi", LastName = "Kayada", Gender = "Female", City = "Rajkot", IsActive = true},
                new EMP01 { Id = 5, FirstName = "Jeet", LastName = "Sorathiya", Gender = "Male", City = "Tankara", IsActive = true},
            };
        }

        #endregion

        #region public Methods

        /// <summary>
        /// Getting employee which id is less than 3
        /// </summary>
        /// <returns>List of employee</returns>
        public static List<EMP01> GetFewEmployee() => lstEmployee.Where(e => e.Id < 3).ToList();

        /// <summary>
        /// Getting employees which id is less than 5
        /// </summary>
        /// <returns></returns>
        public static List<EMP01> GetMoreEmployee() => lstEmployee.Where(e => e.Id < 5).ToList();

        /// <summary>
        /// Getting all employees
        /// </summary>
        /// <returns>List of employees</returns>
        public static List<EMP01> GetAllEmployee() => lstEmployee;

        #endregion
    }
}