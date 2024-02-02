using DatabaseCRUDAPI.Business_Logic;
using DatabaseCRUDAPI.Models;
using System.Net.Http;
using System.Web.Http;

namespace DatabaseCRUDAPI.Controllers
{
    /// <summary>
    /// Student controller for handling student api endpoints
    /// </summary>
    [RoutePrefix("api/CLStudent")]
    public class CLStudentController : ApiController
    {
        private BLStudent bLStudent;

        /// <summary>
        /// Creating a student in database 
        /// </summary>
        /// <param name="objStudent">Student Data</param>
        /// <returns>Student create response</returns>
        [HttpPost]
        [Route("Create")]
        public HttpResponseMessage CreateStudent(STU01 objStudent)
        {
            bLStudent = new BLStudent();
            return bLStudent.InsertData(objStudent);
        }

        /// <summary>
        /// Getting all students data from database
        /// </summary>
        /// <returns>List of students</returns>
        [HttpGet]
        [Route("GetStudnetsData")]
        public IHttpActionResult GetStudentsData()
        {
            bLStudent = new BLStudent();
            return Ok(bLStudent.ReadData());
        }

        /// <summary>
        /// Updating student data in database
        /// </summary>
        /// <param name="objStudent">Student data</param>
        /// <returns>Update response</returns>
        [HttpPut]
        [Route("UpdateStudent")]
        public HttpResponseMessage UpdateData(STU01 objStudent)
        {
            bLStudent = new BLStudent();
            return bLStudent.UpdateStudent(objStudent);
        }

        /// <summary>
        /// Deleting a student using student id
        /// </summary>
        /// <param name="id">Student id</param>
        /// <returns>Delete response</returns>
        [HttpDelete]
        [Route("DeleteStudent/{id}")]
        public HttpResponseMessage DeleteData(int id)
        {
            bLStudent = new BLStudent();
            return bLStudent.DeleteStudent(id);
        }
    }
}
