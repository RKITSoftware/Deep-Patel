using Newtonsoft.Json;
using SchoolManagementAPI.Filters;
using SchoolManagementAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace SchoolManagementAPI.Controllers
{
    [RoutePrefix("student")]
    [AuthenticationAttribute]
    [CacheFilter(TimeDuration = 100)]
    public class CLStudentController : ApiController
    {
        private static string filePath = "F:\\Deep - 380\\Training\\API\\Part 6 - Web Development\\SchoolManagementAPI\\SchoolManagementAPI\\Data\\studentData.json";
        public static List<STU01> studentList;
        public static int noOfNextStudentId;

        public CLStudentController()
        {
            
        }

        static CLStudentController()
        {
            string jsonContent = File.ReadAllText(filePath);

            studentList = JsonConvert.DeserializeObject<List<STU01>>(jsonContent);
            noOfNextStudentId = studentList.OrderByDescending(stu => stu.U01F01).FirstOrDefault().U01F01;
        }

        [HttpPost]
        [Route("add")]
        [AuthorizationAttribute(Roles = "Admin")]
        public IHttpActionResult AddStudent([FromBody]STU01 student)
        {
            student.U01F01 = ++noOfNextStudentId;
            studentList.Add(student);

            USR01 user = new USR01()
            {
                R01F01 = ++CLUserController.noOfNextUserId,
                R01F02 = student.U01F03.Split('@')[0],
                R01F03 = student.U01F04,
                R01F04 = "Student"
            };

            CLUserController.userList.Add(user);

            return Ok(student);
        }

        [HttpPost]
        [Route("update")]
        [AuthorizationAttribute(Roles = "Admin")]
        public IHttpActionResult WriteStudentDataToFile()
        {
            string jsonContent = JsonConvert.SerializeObject(studentList, Formatting.Indented);
            File.WriteAllText(filePath, jsonContent);

            return Ok("Data Writtem Successfully.");
        }

        [HttpGet]
        [Route("get/allData")]
        [AuthorizationAttribute(Roles = "Admin")]
        public IHttpActionResult GetAllStudentData()
        {
            return Ok(studentList);
        }

        [HttpGet]
        [Route("get/{studentId}")]
        [AuthorizationAttribute(Roles = "Student")]
        public IHttpActionResult GetStudentData(int studentId)
        {
            var student = studentList.FirstOrDefault(stu => stu.U01F01 == studentId);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpDelete]
        [Route("delete/{delId}")]
        [AuthorizationAttribute(Roles = "Admin")]
        public IHttpActionResult DeleteStudentData(int delId)
        {
            var studentDetail = studentList.Find(stu => stu.U01F01 == delId);
            studentList.Remove(studentDetail);

            return Ok("Student Deleted");
        }

        [HttpPut]
        [Route("put/studentData/{studentId}")]
        [AuthorizationAttribute(Roles = "Student")]
        public IHttpActionResult UpdateStudentInfo(int studentId, [FromBody]STU01 updateData)
        {
            var oldData = studentList.FirstOrDefault(stu => stu.U01F01 == studentId);

            if (oldData == null)
                return NotFound();

            oldData.U01F02 = updateData.U01F02;
            oldData.U01F05 = updateData.U01F05;
            oldData.U01F06 = updateData.U01F06;
            oldData.U01F07 = updateData.U01F07;

            return Ok(oldData);
        }
    }
}
