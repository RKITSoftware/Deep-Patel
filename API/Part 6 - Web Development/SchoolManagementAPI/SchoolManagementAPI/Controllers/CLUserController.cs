using SchoolManagementAPI.Business_Logic;
using SchoolManagementAPI.Filters;
using SchoolManagementAPI.Models;
using SchoolManagementAPI.Security;
using System.Web.Http;

namespace SchoolManagementAPI.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    [RoutePrefix("api/CLUser")]
    [CookieBasedAuthentication]
    [Authorization(Roles = "Admin")]
    public class CLUserController : ApiController
    {
        /// <summary>
        /// Business logic instance of User
        /// </summary>
        private BLUser _blUser;

        /// <summary>
        /// Initialize instances of business logic class.
        /// </summary>
        public CLUserController()
        {
            _blUser = new BLUser();
        }

        #region API Endpoints

        /// <summary>
        /// POST Endpoint: api/CLUser/post
        /// </summary>
        /// <param name="objUSR01">User data</param>
        [HttpPost]
        [Route("post")]
        public IHttpActionResult CreateUser(USR01 objUSR01)
        {
            BLUser.AddUser(objUSR01);
            return Ok("User Created Successfully");
        }

        /// <summary>
        /// GET Endpoint: api/CLUser/getAllUser
        /// </summary>
        /// <returns>Retrieves a list of all users.</returns>
        [HttpGet]
        [Route("getAllUser")]
        public IHttpActionResult GetAllUser()
        {
            // Return the list of all users.
            return Ok(BLUser.GetUserList());
        }

        /// <summary>
        /// GET Endpoint: api/CLUser/getUser/{id}
        /// </summary>
        /// <param name="id">User Id</param>
        [HttpGet]
        [Route("getUser/{id}")]
        public IHttpActionResult GetUserById(int id)
        {
            // Retrieve a user by ID and return it.
            USR01 objUser = BLUser.GetUser(id);

            if (objUser == null)
                return NotFound();

            // Returns a user object with Ok response
            return Ok(objUser);
        }

        /// <summary>
        /// POST Endpoint: api/CLUser/update
        /// Updates file data.
        /// </summary>
        [HttpPost]
        [Route("update")]
        public IHttpActionResult UpdateFileData()
        {
            BLUser.UpdateFileData();
            return Ok("Data Written Successfully.");
        }

        /// <summary>
        /// DELETE Endpoint: api/CLUser/delete/{delId}
        ///  Deletes a user by ID.
        /// </summary>
        /// <param name="delId">User id</param>
        [HttpDelete]
        [Route("delete/{delId}")]
        public IHttpActionResult DeleteUser(int delId)
        {
            return Ok(BLUser.DeleteUser(delId));
        }

        /// <summary>
        /// PUT Endpoint: api/CLUser/put
        /// Updates user data with the provided data.
        /// </summary>
        /// <param name="userDataFromBody">User Data</param>
        /// <returns></returns>
        [HttpPut]
        [Route("put")]
        public IHttpActionResult UpdateUser(USR01 userDataFromBody)
        {
            return Ok(BLUser.UpdateUserData(userDataFromBody));
        }

        #endregion
    }
}
