using SchoolManagementAPI.Business_Logic;
using SchoolManagementAPI.Models;
using System.Net.Http;
using System.Web.Http;

namespace SchoolManagementAPI.Controllers
{
    /// <summary>
    /// Controller for handling version 1 of student-related API endpoints.
    /// </summary>
    public class CLStudentV1Controller : ApiController
    {
        private BLStudentV1 _blStudent;

        /// <summary>
        /// Constructor to initialize the BLStudentV1 instance.
        /// </summary>
        public CLStudentV1Controller()
        {
            _blStudent = new BLStudentV1();
        }

        /// <summary>
        /// API endpoint to add a new student.
        /// </summary>
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin")] // Authorization restriction to only Admin role
        public HttpResponseMessage Add(STUUSR objSTUUSR)
        {
            return _blStudent.Create(objSTUUSR);
        }

        /// <summary>
        /// API endpoint to get all students.
        /// </summary>
        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Admin")] // Authorization restriction to only Admin role
        public IHttpActionResult GetAll()
        {
            return Ok(_blStudent.GetAll());
        }

        /// <summary>
        /// API endpoint to get a student by ID.
        /// </summary>
        [HttpGet]
        [Route("GetById")]
        [Authorize(Roles = "Student,Admin")] // Authorization restriction to Student and Admin roles
        public IHttpActionResult Get(int id)
        {
            STU01 objStudent = _blStudent.Get(id);

            if (objStudent == null)
                return NotFound();

            return Ok(objStudent);
        }

        /// <summary>
        /// API endpoint to delete a student by ID.
        /// </summary>
        [HttpDelete]
        [Route("Delete")]
        [Authorize(Roles = "Admin")] // Authorization restriction to only Admin role
        public HttpResponseMessage Delete(int id)
        {
            return _blStudent.Delete(id);
        }

        /// <summary>
        /// API endpoint to update a student.
        /// </summary>
        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Student,Admin")] // Authorization restriction to Student and Admin roles
        public HttpResponseMessage UpdateAdmin(STU01 objStudent)
        {
            return _blStudent.Update(objStudent);
        }
    }
}
