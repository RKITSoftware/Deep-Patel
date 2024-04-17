using OnlineShoppingAPI.BL.Common.Interface;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.POCO;

namespace OnlineShoppingAPI.BL.Interface
{
    /// <summary>
    /// Interface for managing <see cref="SUP01"/> operations.
    /// </summary>
    public interface ISUP01Service : ICommonDataHandlerService<DTOSUP01>
    {
        #region Public Methods

        /// <summary>
        /// Changes the email address of a suplier.
        /// </summary>
        /// <param name="username">The username of the suplier.</param>
        /// <param name="newEmail">The new email address.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response ChangeEmail(string username, string newEmail);

        /// <summary>
        /// Validates the parameters before the changing email.
        /// </summary>
        /// <param name="username">The username of the suplier.</param>
        /// <param name="password">The password of the suplier.</param>
        /// <param name="newEmail">The new email address.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response ChangeEmailValidation(string username, string password, string newEmail);

        /// <summary>
        /// Changes the password of a suplier.
        /// </summary>
        /// <param name="newPassword">The new password.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response ChangePassword(string newPassword);

        /// <summary>
        /// Validation before the change password.
        /// </summary>
        /// <param name="username">The username of the suplier.</param>
        /// <param name="oldPassword">The old password of the suplier.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response ChangePasswordValidation(string username, string oldPassword);

        /// <summary>
        /// Deletes a suplier.
        /// </summary>
        /// <param name="id">The ID of the suplier to delete.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response Delete(int id);

        /// <summary>
        /// Delete Validation
        /// </summary>
        /// <param name="id">The ID of the suplier to delete.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response DeleteValidation(int id);

        /// <summary>
        /// Retrieves all suplier.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response GetAll();

        /// <summary>
        /// Retrieves a suplier by its ID.
        /// </summary>
        /// <param name="id">The ID of the suplier to retrieve.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response GetById(int id);

        #endregion Public Methods
    }
}