using SchoolManagementAPI.Business_Logic;
using SchoolManagementAPI.Models;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SchoolManagementAPI.Controllers
{
    /// <summary>
    /// Controller for handling version 2 of student-related API endpoints.
    /// </summary>
    [RoutePrefix("api/v2/CLStudent")]
    [EnableCors("https://www.google.com", "*", "*")]
    public class CLStudentV2Controller : ApiController
    {
        private BLStudentV2 _blStudent;

        /// <summary>
        /// Constructor to initialize the BLStudentV2 instance.
        /// </summary>
        public CLStudentV2Controller()
        {
            _blStudent = new BLStudentV2();
        }

        /// <summary>
        /// API endpoint to get all students (Version 2).
        /// </summary>
        [HttpGet]
        [Route("getAll")]
        public IHttpActionResult GetAll()
        {
            return Ok(_blStudent.GetAll());
        }

        /// <summary>
        /// API endpoint to add a new student (Version 2).
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage Add(STU02 objStudent)
        {
            return _blStudent.Add(objStudent);
        }
    }
}
