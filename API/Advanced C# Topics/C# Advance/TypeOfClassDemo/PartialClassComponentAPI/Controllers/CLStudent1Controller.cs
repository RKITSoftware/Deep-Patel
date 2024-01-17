using PartialClassComponentAPI.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PartialClassComponentAPI.Controllers
{
    public partial class CLStudentController : ApiController
    {
        #region API Endpoints

        /// <summary>
        /// Get all student data
        /// </summary>
        /// <returns>All Student Data</returns>
        [HttpGet]
        public IHttpActionResult GetAllStudent() 
        {
            return Ok(studentList);
        }

        /// <summary>
        /// Create a student and add it to student list
        /// </summary>
        /// <param name="student">Student Data</param>
        /// <returns>Ok response</returns>
        [HttpPost]
        public HttpResponseMessage CreateStudent([FromBody] STU01 student)
        {
            studentList.Add(student);
            return Request.CreateResponse(HttpStatusCode.Created, "Created Succesfully");
        }

        #endregion
    }
}
