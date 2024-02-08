using CacheDemoUsingCacheClass.Business_Logic;
using System.Web.Http;

namespace CacheDemoUsingCacheClass.Controllers
{
    /// <summary>
    /// Controller class for managing user-related HTTP requests.
    /// </summary>
    public class CLUserController : ApiController
    {
        // Business Logic instance for handling user-related operations.
        private BLUser _user;

        /// <summary>
        /// Constructor to initialize the Business Logic instance.
        /// </summary>
        public CLUserController()
        {
            _user = new BLUser();
        }

        /// <summary>
        /// Handles HTTP GET request to retrieve all users.
        /// </summary>
        /// <returns>HTTP response containing the list of users or an error status.</returns>
        public IHttpActionResult GetAllUsers()
        {
            // Call the Business Logic layer to get the list of users.
            return Ok(_user.GetUsers());
        }
    }
}
