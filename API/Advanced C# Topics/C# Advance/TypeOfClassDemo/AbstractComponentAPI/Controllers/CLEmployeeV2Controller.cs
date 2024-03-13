using AbstractComponentAPI.Business_Logic;
using System.Web.Http;

namespace AbstractComponentAPI.Controllers
{
    /// <summary>
    /// Employee V1 controller for handling Employee v1 models operations.
    /// </summary>
    [RoutePrefix("api/CLEmployeeV2")]
    public class CLEmployeeV2Controller : CLBaseEmployeeController
    {
        /// <summary>
        /// GET :- api/CLEmployeeV2
        /// For getting employee data of Version 2
        /// </summary>
        /// <returns>List of employees</returns>
        [HttpGet]
        public override IHttpActionResult Get()
        {
            return Ok(BLEmployeeV2.GetAllEmployeeData());
        }

        /// <summary>
        /// GET :- api/CLEmployeeV2/1
        /// For getting a employee with specific id
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Employee</returns>
        [HttpGet]
        public override IHttpActionResult Get(int id)
        {
            return Ok(BLEmployeeV2.GetEmployeeById(id));
        }
    }
}
