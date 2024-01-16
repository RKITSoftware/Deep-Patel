using Newtonsoft.Json;
using SchoolManagementAPI.Filters;
using SchoolManagementAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace SchoolManagementAPI.Controllers
{
    /// <summary>
    /// Controller for managing student-related operations.
    /// </summary>
    [RoutePrefix("student")] // All user routes starts with student
    [AuthenticationAttribute] // Apply authentication filter to ensure only authenticated users can access the controller.
    // [CacheFilter(TimeDuration = 100)] // Apply caching filter to cache responses for a specified duration.
    public class CLStudentController : ApiController
    {
        #region Private Fields

        /// <summary>
        /// File Path for storing Student Data
        /// </summary>
        private static string filePath = "F:\\Deep - 380\\Training\\API\\Part 6 - Web Development\\SchoolManagementAPI\\SchoolManagementAPI\\Data\\studentData.json";

        #endregion

        #region Public Properties

        /// <summary>
        /// Student List for Operation of API Endpoints
        /// </summary>
        public static List<STU01> studentList { get; set; }

        /// <summary>
        /// Student id for creating new student's studentId. 
        /// </summary>
        public static int noOfNextStudentId { get; set; }

        #endregion

        #region Constructor

        public CLStudentController()
        {
            // Constructor logic can be added if needed.
        }

        static CLStudentController()
        {
            // Initialize studentList and noOfNextStudentId when the controller is first accessed.
            string jsonContent = File.ReadAllText(filePath);

            studentList = JsonConvert.DeserializeObject<List<STU01>>(jsonContent);
            noOfNextStudentId = studentList.OrderByDescending(stu => stu.U01F01).FirstOrDefault().U01F01;
        }

        #endregion

        #region API Endpoints

        /// <summary>
        /// Create a student
        /// </summary>
        /// <param name="student">Get student model from api body</param>
        /// <returns>200 Response if user created</returns>
        [HttpPost]
        [Route("add")] // POST Endpoint :- student/add
        [AuthorizationAttribute(Roles = "Admin")] // Allow only users with the "Admin" role to add a student.
        public IHttpActionResult AddStudent([FromBody] STU01 student)
        {
            // Add a new student and create a corresponding user.
            student.U01F01 = ++noOfNextStudentId;
            studentList.Add(student);

            USR01 user = new USR01()
            {
                R01F01 = ++CLUserController.noOfNextUserId,
                R01F02 = student.U01F03.Split('@')[0],
                R01F03 = student.U01F04,
                R01F04 = "Student"
            };

            // Adding a user to userList
            CLUserController.userList.Add(user);

            return Ok(student);
        }

        /// <summary>
        /// Write the student data to the studentData.json file
        /// </summary>
        /// <returns>Ok Response for data successfully written.</returns>
        [HttpPost]
        [Route("update")] // POST Endpoint  :- student/update
        [AuthorizationAttribute(Roles = "Admin")] // Allow only users with the "Admin" role to update student data.
        public IHttpActionResult WriteStudentDataToFile()
        {
            // Serialize the student list to JSON and write it to the file.
            string jsonContent = JsonConvert.SerializeObject(studentList, Formatting.Indented);
            File.WriteAllText(filePath, jsonContent);

            return Ok("Data Written Successfully.");
        }

        /// <summary>
        /// Get all Student Data
        /// </summary>
        /// <returns>All Student Data</returns>
        [HttpGet]
        [Route("get/allData")] // GET Endpoint :- student/get/allData
        [AuthorizationAttribute(Roles = "Admin")] // Allow only users with the "Admin" role to get all student data.
        public IHttpActionResult GetAllStudentData()
        {
            // Return the list of all students.
            return Ok(studentList);
        }

        /// <summary>
        /// Retrieve Student by using student Id
        /// </summary>
        /// <param name="studentId">For Retrieving Student Data</param>
        /// <returns>Student Data</returns>
        [HttpGet]
        [Route("get/{studentId}")] // Get Endpoint :- student/get/1
        [AuthorizationAttribute(Roles = "Student")] // Allow only users with the "Student" role to get individual student data.
        public IHttpActionResult GetStudentData(int studentId)
        {
            // Retrieve a student by ID and return it.
            var student = studentList.FirstOrDefault(stu => stu.U01F01 == studentId);

            if (student == null)
                return NotFound();

            // Return student with Ok Response
            return Ok(student);
        }

        /// <summary>
        /// Delete Student
        /// </summary>
        /// <param name="delId">For find ftudent</param>
        /// <returns>200 Response With student delete message</returns>
        [HttpDelete]
        [Route("delete/{delId}")] // Delete Endpoint :- student/delete/1
        [AuthorizationAttribute(Roles = "Admin")] // Allow only users with the "Admin" role to delete a student.
        public IHttpActionResult DeleteStudentData(int delId)
        {
            // Delete a student by ID.
            var studentDetail = studentList.Find(stu => stu.U01F01 == delId);
            var userDetail = CLUserController.userList
                .Find(usr => usr.R01F02.Equals(studentDetail.U01F03.Split('@')[0]));
            
            CLUserController.userList.Remove(userDetail);
            studentList.Remove(studentDetail);

            return Ok("Student Deleted");
        }

        /// <summary>
        /// Update the student's data with new data
        /// </summary>
        /// <param name="studentId">Student id for finding specific student</param>
        /// <param name="updateData">New updated student data</param>
        /// <returns>Updated data</returns>
        [HttpPut]
        [Route("put/studentData/{studentId}")] // Put Endpoint :- student/put/studentData/1
        [AuthorizationAttribute(Roles = "Student")] // Allow only users with the "Student" role to update their own data.
        public IHttpActionResult UpdateStudentInfo(int studentId, [FromBody] STU01 updateData)
        {
            // Update student information based on ID.
            var oldData = studentList.FirstOrDefault(stu => stu.U01F01 == studentId);

            if (oldData == null)
                return NotFound();

            oldData.U01F02 = updateData.U01F02;
            oldData.U01F05 = updateData.U01F05;
            oldData.U01F06 = updateData.U01F06;
            oldData.U01F07 = updateData.U01F07;

            return Ok(oldData);
        }

        #endregion
    }
}
