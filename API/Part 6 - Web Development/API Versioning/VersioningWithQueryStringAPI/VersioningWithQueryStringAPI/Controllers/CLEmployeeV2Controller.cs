using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VersioningWithQueryStringAPI.Models;

namespace VersioningWithQueryStringAPI.Controllers
{
    /// <summary>
    /// Controller for API version 2 that handles an updated EMP02 model for employee data
    /// </summary>
    public class CLEmployeeV2Controller : ApiController
    {
        #region Public Properties

        /// <summary>
        /// Sample list of EMP02 objects representing updated employee data for version 2
        /// </summary>
        List<EMP02> empList = new List<EMP02>
        {
            new EMP02 {P02F01 = 1, P02F02 = "Deep", P02F03 = "Patel", P02F04 = new DateTime(2003, 5, 25),
                P02F05= "Limbdi", P02F06 = "Gujarat"},
            new EMP02 {P02F01 = 2, P02F02 = "Jeet", P02F03 = "Sorathiya", P02F04 = new DateTime(2003, 5, 25),
                P02F05= "Tankala", P02F06 = "Gujarat"},
        };

        #endregion

        #region API Endpoints

        // GET: api/CLEmployeeV2
        // Retrieve all employees in the version 2 API
        public IEnumerable<EMP02> GetEmployee()
        {
            return empList;
        }

        // GET: api/CLEmployeeV2/5
        // Retrieve a specific employee by ID in the version 2 API
        public EMP02 GetEmployee(int id)
        {
            // Use LINQ to find the employee with the specified ID
            return empList.FirstOrDefault(e => e.P02F01 == id);
        }

        #endregion
    }
}
