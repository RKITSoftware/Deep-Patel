using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VersioningUsingURLAPI.Models;

namespace VersioningUsingURLAPI.Controllers
{
    // Controller for API version 1
    public class CLEmployeeV1Controller : ApiController
    {
        #region Public Properties

        // Sample employee data for version 1
        List<EMP01> empList = new List<EMP01>
        {
            new EMP01 {P01F01 = 1, P01F02 = "Deep", P01F03 = 22, P01F04 = "Limbdi", P01F05= "Gujarat"},
            new EMP01 {P01F01 = 2, P01F02 = "Jeet", P01F03 = 22, P01F04 = "Tankala", P01F05= "Gujarat"}
        };

        #endregion

        #region API Endpoints

        /// <summary>
        /// GET api/v1/employee
        /// </summary>
        /// <returns>Returns all employees for version 1</returns>
        public IEnumerable<EMP01> Get()
        {
            return empList;
        }

        /// <summary>
        /// GET api/v1/employee/{id}
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>Returns a specific employee by id for version 1</returns>
        public EMP01 Get(int id)
        {
            return empList.FirstOrDefault(e => e.P01F01 == id);
        }

        #endregion
    }
}
