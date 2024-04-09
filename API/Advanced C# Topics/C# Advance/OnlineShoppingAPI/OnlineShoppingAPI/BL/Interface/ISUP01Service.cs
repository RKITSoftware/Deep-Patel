using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using OnlineShoppingAPI.Models.POCO;

namespace OnlineShoppingAPI.BL.Interface
{
    /// <summary>
    /// Interface for managing <see cref="SUP01"/> operations.
    /// </summary>
    public interface ISUP01Service
    {
        /// <summary>
        /// Changes the email address of a suplier.
        /// </summary>
        /// <param name="username">The username of the suplier.</param>
        /// <param name="password">The password of the suplier.</param>
        /// <param name="newEmail">The new email address.</param>
        /// <param name="response">The response containing the result of the operation.</param>
        void ChangeEmail(string username, string password, string newEmail, out Response response);

        /// <summary>
        /// Changes the password of a suplier.
        /// </summary>
        /// <param name="username">The username of the suplier.</param>
        /// <param name="oldPassword">The old password of the suplier.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="response">The response containing the result of the operation.</param>
        void ChangePassword(string username, string oldPassword, string newPassword, out Response response);

        /// <summary>
        /// Deletes a suplier.
        /// </summary>
        /// <param name="id">The ID of the suplier to delete.</param>
        /// <param name="response">The response containing the result of the operation.</param>
        void Delete(int id, out Response response);

        /// <summary>
        /// Retrieves all suplier.
        /// </summary>
        /// <param name="response">The response containing the result of the operation.</param>
        void GetAll(out Response response);

        /// <summary>
        /// Retrieves a suplier by its ID.
        /// </summary>
        /// <param name="id">The ID of the suplier to retrieve.</param>
        /// <param name="response">The response containing the result of the operation.</param>
        void GetById(int id, out Response response);

        /// <summary>
        /// Prepares a suplier for saving based on the operation.
        /// </summary>
        /// <param name="objSUP01DTO">The DTO object representing the suplier.</param>
        /// <param name="operation">The type of operation (e.g., add, update).</param>
        void PreSave(DTOSUP01 objSUP01DTO, EnmOperation operation);

        /// <summary>
        /// Saves the changes made to a supplier.
        /// </summary>
        /// <param name="response">The response containing the result of the operation.</param>
        void Save(out Response response);

        /// <summary>
        /// Performs validation on the supplier data.
        /// </summary>
        /// <param name="response">The response containing the result of the operation.</param>
        /// <returns>True if validation succeeds, otherwise false.</returns>
        bool Validation(out Response response);
    }
}
