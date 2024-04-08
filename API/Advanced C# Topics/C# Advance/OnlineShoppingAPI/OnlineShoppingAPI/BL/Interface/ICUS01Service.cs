using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;

namespace OnlineShoppingAPI.BL.Interface
{
    /// <summary>
    /// Interface for service handling CUS01 related operations.
    /// </summary>
    public interface ICUS01Service
    {
        /// <summary>
        /// Changes the email address of a customer.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="newEmail">The new email address to set.</param>
        /// <param name="response">Out parameter containing the response status after the email change operation.</param>
        void ChangeEmail(string username, string password, string newEmail, out Response response);

        /// <summary>
        /// Changes the password of a customer.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="oldPassword">The old password of the user.</param>
        /// <param name="newPassword">The new password to set.</param>
        /// <param name="response">Out parameter containing the response status after the password change operation.</param>
        void ChangePassword(string username, string oldPassword, string newPassword, out Response response);

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        /// <param name="response">Out parameter containing the response status after the deletion operation.</param>
        void Delete(int id, out Response response);

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <param name="response">Out parameter containing the response with all users.</param>
        void GetAll(out Response response);

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <param name="response">Out parameter containing the response with the requested user.</param>
        void GetById(int id, out Response response);

        /// <summary>
        /// Prepares for saving a user.
        /// </summary>
        /// <param name="objCUS01DTO">The DTO object representing the user.</param>
        /// <param name="operation">The operation type for the save action.</param>
        void PreSave(DTOCUS01 objCUS01DTO, EnmOperation operation);

        /// <summary>
        /// Saves changes made to a user.
        /// </summary>
        /// <param name="response">Out parameter containing the response status after saving.</param>
        void Save(out Response response);

        /// <summary>
        /// Validates user information.
        /// </summary>
        /// <param name="response">Out parameter containing the validation result.</param>
        /// <returns>True if the user information is valid, otherwise false.</returns>
        bool Validation(out Response response);
    }
}
