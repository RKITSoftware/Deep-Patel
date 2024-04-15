using OnlineShoppingAPI.BL.Common.Interface;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;

namespace OnlineShoppingAPI.BL.Interface
{
    /// <summary>
    /// Interface for handling CUS01 related operations.
    /// </summary>
    public interface ICUS01Service : ICommonDataHandlerService<DTOCUS01>
    {
        #region Public Methods

        /// <summary>
        /// Changes the email address of a customer.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="newEmail">The new email address to set.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response ChangeEmail(string username, string password, string newEmail);

        /// <summary>
        /// Changes the password of a customer.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="oldPassword">The old password of the user.</param>
        /// <param name="newPassword">The new password to set.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response ChangePassword(string username, string oldPassword, string newPassword);

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response Delete(int id);

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response GetAll();

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response GetById(int id);

        #endregion Public Methods
    }
}