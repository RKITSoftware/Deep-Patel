using Microsoft.AspNetCore.Mvc;
using MiddleWareDemo.Business_Logic;
using MiddleWareDemo.Model;

namespace MiddleWareDemo.Controllers
{
    /// <summary>
    /// Controller for managing user-related API endpoints.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private BLUser _blUser;

        /// <summary>
        /// Initializes a new instance of the UserController class.
        /// </summary>
        public UserController()
        {
            _blUser = new BLUser();
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>An action result containing the list of users.</returns>
        [HttpGet]
        public IActionResult Get() => Ok(_blUser.Get());

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user object if found; otherwise, null.</returns>
        [HttpGet("{id}")]
        public USR01 Get(int id) => _blUser.Get(id);

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="objUser">The user object to add.</param>
        /// <returns>A status code indicating the result of the operation.</returns>
        [HttpPost]
        public IActionResult Post(USR01 objUser)
        {
            _blUser.Add(objUser);
            return StatusCode(201, "User created successfully.");
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>A status code indicating the result of the operation.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _blUser.Delete(id);
            return Ok("User deleted successfully.");
        }
    }
}
