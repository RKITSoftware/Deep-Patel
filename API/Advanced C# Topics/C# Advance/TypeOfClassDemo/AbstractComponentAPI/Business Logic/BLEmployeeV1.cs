﻿using AbstractComponentAPI.Models;
using System.Collections.Generic;

namespace AbstractComponentAPI.Business_Logic
{
    /// <summary>
    /// Business Logic for Employee version 1
    /// </summary>
    public class BLEmployeeV1
    {
        #region Private Fields

        /// <summary>
        /// Stores Employee Version 1 models data into list
        /// </summary>
        private readonly static List<EMP01> lstEmployeeV1 = new List<EMP01>();

        #endregion

        #region Public Methods

        /// <summary>
        /// For Getting the all employee v1 data
        /// </summary>
        /// <returns>All employee V1 data</returns>
        public static List<EMP01> GetAllEmployeeData()
        {
            return lstEmployeeV1;
        }

        /// <summary>
        /// For getting a specific employee by using Id
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Employee</returns>
        public static EMP01 GetEmployeeById(int id)
        {
            return lstEmployeeV1.Find(e => e.P01F01 == id);
        }

        #endregion
    }
}