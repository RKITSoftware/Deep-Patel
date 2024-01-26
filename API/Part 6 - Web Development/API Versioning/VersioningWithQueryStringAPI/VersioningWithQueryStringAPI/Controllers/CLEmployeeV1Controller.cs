using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VersioningWithQueryStringAPI.Models;

namespace VersioningWithQueryStringAPI.Controllers
{
    /// <summary>
    /// Controller for API version 1 that handles employee data
    /// </summary>
    public class CLEmployeeV1Controller : ApiController
    {
        #region Public Properties

        // Sample list of EMP01 objects representing employee data
        List<EMP01> empList = new List<EMP01>
        {
            new EMP01 {P01F01 = 1, P01F02 = "Deep", P01F03 = 22, P01F04 = "Limbdi", P01F05= "Gujarat"},
            new EMP01 {P01F01 = 2, P01F02 = "Jeet", P01F03 = 22, P01F04 = "Tankala", P01F05= "Gujarat"}
        };

        #endregion

        #region API Endpoints

        // GET: api/CLEmployeeV1
        // Retrieve all employees in the version 1 API
        public IEnumerable<EMP01> GetEmployee()
        {
            return empList;
        }

        // GET: api/CLEmployeeV1/5
        // Retrieve a specific employee by ID in the version 1 API
        public EMP01 GetEmployee(int id)
        {
            // Use LINQ to find the employee with the specified ID
            return empList.FirstOrDefault(e => e.P01F01 == id);
        }

        #endregion
    }
}
