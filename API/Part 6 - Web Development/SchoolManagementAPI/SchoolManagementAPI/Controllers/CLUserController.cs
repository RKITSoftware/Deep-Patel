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
    /// Controller for managing user-related operations.
    /// </summary>
    [RoutePrefix("user")] // All user routes starts with user
    [AuthenticationAttribute] // Apply authentication filter to ensure only authenticated users can access the controller.
    [AuthorizationAttribute(Roles = "Admin")] // Apply authorization filter to restrict access to users with the "Admin" role.
    [CacheFilter(TimeDuration = 100)] // Apply caching filter to cache responses for a specified duration.
    public class CLUserController : ApiController
    {
        #region Private Fields

        /// <summary>
        /// File Path for storing User Data
        /// </summary>
        private static string filePath = "F:\\Deep - 380\\Training\\API\\Part 6 - Web Development\\SchoolManagementAPI\\SchoolManagementAPI\\Data\\userData.json";

        #endregion

        #region Public Properties

        /// <summary>
        /// User List for Operation of API Endpoints
        /// </summary>
        public static List<USR01> userList { get; set; }

        /// <summary>
        /// User id for creating new user's userId. 
        /// </summary>
        public static int noOfNextUserId { get; set; }

        #endregion

        #region Constructor

        static CLUserController()
        {
            // Read user data from the JSON file when the controller is initialized.
            string jsonContent = File.ReadAllText(filePath);

            userList = JsonConvert.DeserializeObject<List<USR01>>(jsonContent);
            noOfNextUserId = userList.OrderByDescending(e => e.R01F01).FirstOrDefault().R01F01;
        }

        #endregion

        #region API Endpoints

        [HttpPost]
        [Route("post")] // POST Endpoint :- user/post
        public IHttpActionResult CreateUser([FromBody] USR01 user)
        {
            // Create a new user, assign a unique ID, and add it to the user list.
            user.R01F01 = ++noOfNextUserId;
            userList.Add(user);

            return Ok("User Created Successfully");
        }

        [HttpGet]
        [Route("getAllUser")] // GET Endpoint :- user/getAllUser
        public IHttpActionResult GetAllUser()
        {
            // Return the list of all users.
            return Ok(userList);
        }

        [HttpGet]
        [Route("getUser/{id}")] // GET Endpoint :- user/getUser/1
        public IHttpActionResult GetUserById(int id)
        {
            // Retrieve a user by ID and return it.
            var objUser = userList.FirstOrDefault(u => u.R01F01 == id);

            if (objUser == null)
                return NotFound();

            // Returns a user object with Ok response
            return Ok(objUser);
        }

        [HttpPost]
        [Route("update")] // POST Endpoint :- user/update
        public IHttpActionResult UpdateFileData()
        {
            // Serialize the user list to JSON and write it to the file.
            string jsonContent = JsonConvert.SerializeObject(userList, Formatting.Indented);
            File.WriteAllText(filePath, jsonContent);

            return Ok("Data Written Successfully.");
        }

        [HttpDelete]
        [Route("delete/{delId}")] // DELETE Endpoint :- user/delete/1
        public IHttpActionResult DeleteUser(int delId)
        {
            // Get a user by ID.
            var user = userList.FirstOrDefault(u => u.R01F01 == delId);

            if (user == null)
                return NotFound();

            // Delete Data from userList
            userList.Remove(user);

            // Return 200 Status Response witth updated data to userList
            return Ok("User Deleted Successfully");
        }

        [HttpPut]
        [Route("put/{id}")] // PUT Endpoint :- user/put/1
        public IHttpActionResult UpdateUser(int id, [FromBody] USR01 userDataFromBody)
        {
            // Get user data based on ID.
            var user = userList.FirstOrDefault(u => u.R01F01 == id);

            if (user == null)
                return NotFound();

            // Update User's Password
            user.R01F03 = userDataFromBody.R01F03;

            // Return 200 Status Response witth updated data to userList
            return Ok(user);
        }

        #endregion
    }
}
