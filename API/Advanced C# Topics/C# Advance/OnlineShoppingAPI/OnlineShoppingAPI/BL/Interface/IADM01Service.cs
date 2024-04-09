using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using OnlineShoppingAPI.Models.POCO;

namespace OnlineShoppingAPI.BL.Interface
{
    /// <summary>
    /// Interface for <see cref="ADM01"/> related operations.
    /// </summary>
    public interface IADM01Service
    {
        /// <summary>
        /// Changes the email address of an admin user.
        /// </summary>
        /// <param name="username">Username of the admin.</param>
        /// <param name="password">Password of the admin.</param>
        /// <param name="newEmail">New email address to set.</param>
        /// <param name="response">Response indicating the outcome of the operation.</param>
        void ChangeEmail(string username, string password, string newEmail, out Response response);

        /// <summary>
        /// Changes the password of an admin user.
        /// </summary>
        /// <param name="username">Username of the admin.</param>
        /// <param name="oldPassword">Old password of the admin.</param>
        /// <param name="newPassword">New password to set.</param>
        /// <param name="response">Response indicating the outcome of the operation.</param>
        void ChangePassword(string username, string oldPassword, string newPassword, out Response response);

        /// <summary>
        /// Deletes an admin user by ID.
        /// </summary>
        /// <param name="id">ID of the admin to delete.</param>
        /// <param name="response">Response indicating the outcome of the operation.</param>
        void Delete(int id, out Response response);

        /// <summary>
        /// Retrieves the profit for a specific date.
        /// </summary>
        /// <param name="date">Date for which profit is to be retrieved.</param>
        /// <param name="response">Response indicating the outcome of the operation.</param>
        void GetProfit(string date, out Response response);

        /// <summary>
        /// Performs pre-save operations for creating or updating an admin.
        /// </summary>
        /// <param name="objDTOADM01">DTO object containing admin details.</param>
        /// <param name="create">Enumeration indicating the operation (Create or Update).</param>
        void PreSave(DTOADM01 objDTOADM01, EnmOperation create);

        /// <summary>
        /// Saves the admin details to the database.
        /// </summary>
        /// <param name="response">Response indicating the outcome of the operation.</param>
        void Save(out Response response);

        /// <summary>
        /// Validates the admin data before saving.
        /// </summary>
        /// <param name="response">Response indicating the outcome of the validation.</param>
        /// <returns>True if validation succeeds, otherwise false.</returns>
        bool Validation(out Response response);
    }
}
