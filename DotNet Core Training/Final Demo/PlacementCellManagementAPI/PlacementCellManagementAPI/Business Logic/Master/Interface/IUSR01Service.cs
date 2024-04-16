using PlacementCellManagementAPI.Models.POCO;

namespace PlacementCellManagementAPI.Business_Logic.Interface
{
    /// <summary>
    /// Interface for user service operations.
    /// </summary>
    public interface IUSR01Service
    {
        /// <summary>
        /// Checks if a user with the provided username and password exists.
        /// </summary>
        /// <param name="username">Username to check.</param>
        /// <param name="password">Password to check.</param>
        /// <param name="objUser">If user exists, contains user data; otherwise, null.</param>
        /// <returns>True if user exists, otherwise false.</returns>
        bool CheckUser(string username, string password, out USR01 objUser);
    }
}
