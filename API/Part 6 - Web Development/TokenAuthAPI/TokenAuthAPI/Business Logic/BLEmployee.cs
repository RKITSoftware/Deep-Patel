using System.Collections.Generic;
using System.Linq;
using TokenAuthAPI.Models;

namespace TokenAuthAPI.Business_Logic
{
    /// <summary>
    /// All helper methods for employee controller
    /// </summary>
    public class BLEmployee
    {
        #region Private Fields

        /// <summary>
        /// Stores all employee details in list
        /// </summary>
        private static List<EMP01> lstEmployee;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialize employee list
        /// </summary>
        static BLEmployee()
        {
            lstEmployee = new List<EMP01>()
            {
                new EMP01{Id = 1, FirstName = "Deep", LastName= "Patel", Email="deeppatel@gmail.com", Gender = "Male"},
                new EMP01{Id = 2, FirstName = "Jeet", LastName= "Sorathiya", Email="jeetsorathiya@gmail.com", Gender = "Male"},
                new EMP01{Id = 3, FirstName = "Prajval", LastName= "Gahine", Email="prajvalgahine@gmail.com", Gender = "Male"},
                new EMP01{Id = 4, FirstName = "Krinsi", LastName= "Kayada", Email="krinsikayada@gmail.com", Gender = "Feale"},
                new EMP01{Id = 5, FirstName = "Prince", LastName= "Goswami", Email="princegoswami@gmail.com", Gender = "Male"}
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// For getting the employee using employee id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns>Employee</returns>
        public static EMP01 GetEmployeeById(int id) => lstEmployee.FirstOrDefault(e => e.Id == id);

        /// <summary>
        /// For getting some employee which id is less than 4
        /// </summary>
        /// <returns>List of Employee</returns>
        public static List<EMP01> GetSomeEmployee() => lstEmployee.Where(e => e.Id < 4).ToList();

        /// <summary>
        /// Getting all employee details
        /// </summary>
        /// <returns>List of employee</returns>
        public static List<EMP01> GetAllEmployee() => lstEmployee;

        #endregion
    }
}