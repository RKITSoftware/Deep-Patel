using AbstractComponentAPI.Models;
using System.Collections.Generic;

namespace AbstractComponentAPI.Business_Logic
{
    /// <summary>
    /// Business Logic class for Employee v2 Controller
    /// </summary>
    public class BLEmployeeV2
    {
        #region Private Fields

        /// <summary>
        /// Stores Employee Version 2 models data into list
        /// </summary>
        private readonly static List<EMP02> lstEmployeeV2 = new List<EMP02>();

        #endregion

        #region Public Methods

        /// <summary>
        /// For Getting the all employee data
        /// </summary>
        /// <returns>All employee V2 data</returns>
        public static List<EMP02> GetAllEmployeeData()
        {
            return lstEmployeeV2;
        }

        /// <summary>
        /// For getting a specific employee by using Id
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Employee</returns>
        public static EMP02 GetEmployeeById(int id)
        {
            return lstEmployeeV2.Find(e => e.P02F01 == id);
        }

        #endregion
    }
}