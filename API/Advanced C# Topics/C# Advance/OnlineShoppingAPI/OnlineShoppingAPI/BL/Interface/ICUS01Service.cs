using OnlineShoppingAPI.BL.Common.Interface;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;

namespace OnlineShoppingAPI.BL.Interface
{
    /// <summary>
    /// Interface for handling CUS01 related operations.
    /// </summary>
    public interface ICUS01Service : IOperationService, IDataHandlerService
    {
        #region Public Methods

        /// <summary>
        /// Changes the email address of a customer.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="newEmail">The new email address to set.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void ChangeEmail(string username, string password, string newEmail, out Response response);

        /// <summary>
        /// Changes the password of a customer.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="oldPassword">The old password of the user.</param>
        /// <param name="newPassword">The new password to set.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void ChangePassword(string username, string oldPassword, string newPassword, out Response response);

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void Delete(int id, out Response response);

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void GetAll(out Response response);

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void GetById(int id, out Response response);

        /// <summary>
        /// Prepares for saving a user.
        /// </summary>
        /// <param name="objCUS01DTO">The DTO object representing the customer.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void PreSave(DTOCUS01 objCUS01DTO);

        #endregion
    }
}
