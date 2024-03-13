using AbstractComponentAPI.Business_Logic;
using System.Web.Http;

namespace AbstractComponentAPI.Controllers
{
    /// <summary>
    /// Employee V1 controller for handling Employee v1 models operations.
    /// </summary>
    [RoutePrefix("api/CLEmployeeV1")]
    public class CLEmployeeV1Controller : CLBaseEmployeeController
    {
        /// <summary>
        /// GET :- api/CLEmployeeV1
        /// For getting employee data of Version 1
        /// </summary>
        /// <returns>List of employees</returns>
        [HttpGet]
        public override IHttpActionResult Get()
        {
            return Ok(BLEmployeeV1.GetAllEmployeeData());
        }

        /// <summary>
        /// GET :- api/CLEmployeeV1/1
        /// For getting a employee with specific id
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Employee</returns>
        [HttpGet]
        public override IHttpActionResult Get(int id)
        {
            return Ok(BLEmployeeV1.GetEmployeeById(id));
        }
    }
}
