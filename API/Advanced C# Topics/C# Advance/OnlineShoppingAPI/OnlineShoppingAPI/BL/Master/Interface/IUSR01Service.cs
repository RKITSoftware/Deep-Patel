using OnlineShoppingAPI.Models.POCO;

namespace OnlineShoppingAPI.BL.Master.Interface
{
    /// <summary>
    /// Services for <see cref="USR01"/>.
    /// </summary>
    public interface IUSR01Service
    {
        #region Publie Methods

        /// <summary>
        /// Retrieves user details using user id.
        /// </summary>
        /// <param name="username">User id of the user.</param>
        /// <returns>User details. Null if the user is not found.</returns>
        USR01 GetUser(int id);

        /// <summary>
        /// Retrieves user details using username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <returns>User details. Null if the user is not found.</returns>
        USR01 GetUser(string username);

        /// <summary>
        /// Retrieves user details using username and password.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <returns>User details. Null if the user is not found.</returns>
        USR01 GetUser(string username, string password);

        /// <summary>
        /// Checks if a user exists using username and password.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        bool IsExist(string username, string password);

        #endregion
    }
}