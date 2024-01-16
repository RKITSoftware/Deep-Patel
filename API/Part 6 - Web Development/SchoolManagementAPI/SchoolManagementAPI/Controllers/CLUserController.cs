using Newtonsoft.Json;
using SchoolManagementAPI.Filters;
using SchoolManagementAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace SchoolManagementAPI.Controllers
{
    [RoutePrefix("user")]
    [AuthenticationAttribute]
    [AuthorizationAttribute(Roles = "Admin")]
    [CacheFilter(TimeDuration = 100)]
    public class CLUserController : ApiController
    {
        private static string filePath =
                "F:\\Deep - 380\\Training\\API\\Part 6 - Web Development\\SchoolManagementAPI\\SchoolManagementAPI\\Data\\userData.json";
        public static List<USR01> userList;
        public static int noOfNextUserId;

        static CLUserController()
        {
            string jsonContent = File.ReadAllText(filePath);
            
            userList = JsonConvert.DeserializeObject<List<USR01>>(jsonContent);
            noOfNextUserId = userList.OrderByDescending(e => e.R01F01).FirstOrDefault().R01F01;
        }

        [HttpPost]
        [Route("post")]
        public IHttpActionResult CreateUser([FromBody] USR01 user)
        {
            user.R01F01 = ++noOfNextUserId;
            userList.Add(user);
            return Ok("User Created Successfully");
        }

        [HttpGet]
        [Route("getAllUser")]
        public IHttpActionResult GetAllUser()
        {
            return Ok(userList);
        }

        [HttpGet]
        [Route("getUser/{id}")]
        public IHttpActionResult GetUserById(int id)
        {
            var objUser = userList.FirstOrDefault(u => u.R01F01 == id);

            if (objUser == null)
                return NotFound();

            return Ok(objUser);
        }

        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdateFileData()
        {
            string jsonContent = JsonConvert.SerializeObject(userList, Formatting.Indented);
            File.WriteAllText(filePath, jsonContent);

            return Ok("Data Writtem Successfully.");
        }

        [HttpDelete]
        [Route("delete/{delId}")]
        public IHttpActionResult DeleteUser(int delId)
        {
            var user = userList.FirstOrDefault(u => u.R01F01 == delId);

            if (user == null)
                return NotFound();

            userList.Remove(user);
            return Ok("User Deleted Successfully");
        }

        [HttpPut]
        [Route("put/{id}")]
        public IHttpActionResult UpdateUser(int id, [FromBody] USR01 userDataFrombody)
        {
            var user = userList.FirstOrDefault(u => u.R01F01 == id);

            if (user == null)
                return NotFound();

            user.R01F03 = userDataFrombody.R01F03;
            return Ok(user);
        }
    }
}
