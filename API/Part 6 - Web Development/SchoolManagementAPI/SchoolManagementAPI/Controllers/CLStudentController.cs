using SchoolManagementAPI.Business_Logic;
using SchoolManagementAPI.Filters;
using SchoolManagementAPI.Models;
using SchoolManagementAPI.Security;
using System.Web.Http;

namespace SchoolManagementAPI.Controllers
{
    /// <summary>
    /// Controller for managing student-related operations.
    /// </summary>
    [RoutePrefix("api/CLStudent")] // All user routes starts with student
    [CookieBasedAuthentication]
    public class CLStudentController : ApiController
    {
        /// <summary>
        /// Business logic instance of Student
        /// </summary>
        private BLStudent _blStudent;

        /// <summary>
        /// Initialize instances of business logic class.
        /// </summary>
        public CLStudentController()
        {
            _blStudent = new BLStudent();
        }

        #region API Endpoints

        /// <summary>
        /// POST Endpoint: api/CLStudent/add
        /// </summary>
        /// <param name="student">New student information</param>
        [HttpPost]
        [Route("add")]
        [Authorization(Roles = "Admin")]
        public IHttpActionResult AddStudent(STU01 student)
        {
            return Ok(_blStudent.AddData(student));
        }

        /// <summary>
        /// POST Endpoint: api/CLStudent/update
        /// </summary>
        /// <returns>Updates student data and writes it to a file.</returns>
        [HttpPost]
        [Route("update")]
        [Authorization(Roles = "Admin")]
        public IHttpActionResult WriteStudentDataToFile()
        {
            _blStudent.UpdateStudentDataFile();
            return Ok("Data Written Successfully.");
        }

        /// <summary>
        /// GET Endpoint: api/CLStudent/get/allData
        /// </summary>
        /// <returns>Retrieves a list of all student data.</returns>
        [HttpGet]
        [Route("get/allData")]
        [Authorization(Roles = "Admin")]
        public IHttpActionResult GetAllStudentData()
        {
            // Return the list of all students.
            return Ok(_blStudent.GetAllStudentData());
        }

        /// <summary>
        /// GET Endpoint: api/CLStudent/get/{studentId}
        /// </summary>
        /// <param name="studentId">Retrieves individual student data by ID.</param>
        [HttpGet]
        [Route("get/{studentId}")]
        [Authorization(Roles = "Student")]
        public IHttpActionResult GetStudentData(int studentId)
        {
            // Retrieve a student by ID and return it.
            STU01 student = _blStudent.GetStudentById(studentId);

            if (student == null)
                return NotFound();

            // Return student with Ok Response
            return Ok(student);
        }

        /// <summary>
        /// DELETE Endpoint: api/CLStudent/delete/{delId}
        /// </summary>
        /// <param name="delId">Deletes a student by ID.</param>
        [HttpDelete]
        [Route("delete/{delId}")]
        [Authorization(Roles = "Admin")]
        public IHttpActionResult DeleteStudentData(int delId)
        {
            _blStudent.DeleteStudent(delId);
            return Ok("Student Deleted");
        }

        /// <summary>
        /// PUT Endpoint: api/CLStudent/put/studentData
        /// Requires "Student" role for authorization.
        /// </summary>
        /// <param name="updateData">Updates a student's data with new information.</param>
        [HttpPut]
        [Route("put/studentData")]
        [Authorization(Roles = "Student")]
        public IHttpActionResult UpdateStudentInfo(STU01 updateData)
        {
            return Ok(_blStudent.UpdateStudentData(updateData));
        }

        #endregion
    }
}
