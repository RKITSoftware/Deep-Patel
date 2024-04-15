using OnlineShoppingAPI.BL.Common.Interface;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.POCO;

namespace OnlineShoppingAPI.BL.Interface
{
    /// <summary>
    /// Interface for <see cref="ADM01"/> related operations.
    /// </summary>
    public interface IADM01Service : ICommonDataHandlerService<DTOADM01>
    {
        #region Public Methods

        /// <summary>
        /// Changes the email address of an admin user.
        /// </summary>
        /// <param name="username">Username of the admin.</param>
        /// <param name="password">Password of the admin.</param>
        /// <param name="newEmail">New email address to set.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response ChangeEmail(string username, string password, string newEmail);

        /// <summary>
        /// Changes the password of an admin user.
        /// </summary>
        /// <param name="username">Username of the admin.</param>
        /// <param name="oldPassword">Old password of the admin.</param>
        /// <param name="newPassword">New password to set.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response ChangePassword(string username, string oldPassword, string newPassword);

        /// <summary>
        /// Deletes an admin user by ID.
        /// </summary>
        /// <param name="id">ID of the admin to delete.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response Delete(int id);

        /// <summary>
        /// Retrieves the profit for a specific date.
        /// </summary>
        /// <param name="date">Date for which profit is to be retrieved.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response GetProfit(string date);

        #endregion Public Methods
    }
}