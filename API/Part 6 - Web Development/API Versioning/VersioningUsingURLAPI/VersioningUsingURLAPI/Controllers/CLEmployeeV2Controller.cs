using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VersioningUsingURLAPI.Models;

namespace VersioningUsingURLAPI.Controllers
{
    // Controller for API version 2
    public class CLEmployeeV2Controller : ApiController
    {
        #region Public Properties

        /// <summary>
        /// Sample employee data for version 2
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

        // GET api/v2/employee
        // Returns all employees for version 2
        public IEnumerable<EMP02> Get()
        {
            return empList;
        }

        // GET api/v2/employee/{id}
        // Returns a specific employee by id for version 2
        public EMP02 Get(int id)
        {
            return empList.FirstOrDefault(e => e.P02F01 == id);
        }

        #endregion
    }
}
