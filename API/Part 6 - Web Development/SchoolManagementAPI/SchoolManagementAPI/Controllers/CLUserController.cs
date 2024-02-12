using SchoolManagementAPI.Business_Logic;
using SchoolManagementAPI.Filters;
using SchoolManagementAPI.Models;
using System.Web.Http;

namespace SchoolManagementAPI.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    [RoutePrefix("api/CLUser")] // All user routes starts with user
    [Authentication] // Apply authentication filter to ensure only authenticated users can access the controller.
    [Authorization(Roles = "Admin")] // Apply authorization filter to restrict access to users with the "Admin" role.
    [CacheFilter(TimeDuration = 100)] // Apply caching filter to cache responses for a specified duration.
    public class CLUserController : ApiController
    { 
        #region API Endpoints

        [HttpPost]
        [Route("post")] // POST Endpoint :- api/CLUser/post
        public IHttpActionResult CreateUser([FromBody] USR01 objUSR01)
        {
            BLUser.AddUser(objUSR01);
            return Ok("User Created Successfully");
        }

        [HttpGet]
        [Route("getAllUser")] // GET Endpoint :- api/CLUser/getAllUser
        public IHttpActionResult GetAllUser()
        {
            // Return the list of all users.
            return Ok(BLUser.GetUserList());
        }

        [HttpGet]
        [Route("getUser/{id}")] // GET Endpoint :- api/CLUser/getUser/1
        public IHttpActionResult GetUserById(int id)
        {
            // Retrieve a user by ID and return it.
            USR01 objUser = BLUser.GetUser(id);

            if (objUser == null)
                return NotFound();

            // Returns a user object with Ok response
            return Ok(objUser);
        }

        [HttpPost]
        [Route("update")] // POST Endpoint :- api/CLUser/update
        public IHttpActionResult UpdateFileData()
        {
            BLUser.UpdateFileData();
            return Ok("Data Written Successfully.");
        }

        [HttpDelete]
        [Route("delete/{delId}")] // DELETE Endpoint :- api/CLUser/delete/1
        public IHttpActionResult DeleteUser(int delId)
        {
            return Ok(BLUser.DeleteUser(delId));
        }

        [HttpPut]
        [Route("put")] // PUT Endpoint :- api/CLUser/put
        public IHttpActionResult UpdateUser([FromBody] USR01 userDataFromBody)
        {
            return Ok(BLUser.UpdateUserData(userDataFromBody));
        }

        #endregion
    }
}
