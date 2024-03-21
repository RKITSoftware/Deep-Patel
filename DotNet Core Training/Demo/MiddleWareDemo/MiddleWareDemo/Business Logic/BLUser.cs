using MiddleWareDemo.Model;

namespace MiddleWareDemo.Business_Logic
{
    /// <summary>
    /// Business logic class for managing users.
    /// </summary>
    public class BLUser
    {
        private static List<USR01> _lstUsers;

        // Static constructor to initialize the list of users.
        static BLUser()
        {
            _lstUsers = new List<USR01>();
        }

        /// <summary>
        /// Adds a new user to the list.
        /// </summary>
        /// <param name="objUser">The user object to be added.</param>
        public void Add(USR01 objUser) => _lstUsers.Add(objUser);

        /// <summary>
        /// Deletes a user from the list based on their ID.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        public void Delete(int id) => _lstUsers.RemoveAll(usr => usr.R01F01 == id);

        /// <summary>
        /// Retrieves all users from the list.
        /// </summary>
        /// <returns>An enumerable collection of users.</returns>
        public IEnumerable<USR01> Get() => _lstUsers;

        /// <summary>
        /// Retrieves a user from the list based on their ID.
        /// </summary>
        /// <param name="id">The ID of the user to be retrieved.</param>
        /// <returns>The user object if found; otherwise, null.</returns>
        public USR01 Get(int id) => _lstUsers.FirstOrDefault(usr => usr.R01F01 == id);
    }
}
