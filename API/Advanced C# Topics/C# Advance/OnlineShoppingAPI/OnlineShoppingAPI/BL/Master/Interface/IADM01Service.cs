﻿using OnlineShoppingAPI.BL.Common.Interface;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.POCO;

namespace OnlineShoppingAPI.BL.Master.Interface
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
        /// <param name="newEmail">New email address to set.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response ChangeEmail(string username, string newEmail);

        /// <summary>
        /// Validate the data is correct or not for the change email process.
        /// </summary>
        /// <param name="username">Username of the admin.</param>
        /// <param name="password">Password of the admin.</param>
        /// <param name="newEmail">New email address to set.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response ChangeEmailValidation(string username, string password, string newEmail);

        /// <summary>
        /// Changes the password of an admin user.
        /// </summary>
        /// <param name="newPassword">New password to set.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response ChangePassword(string newPassword);

        /// <summary>
        /// Valiation before the change password
        /// </summary>
        /// <param name="username">Username of the admin.</param>
        /// <param name="oldPassword">Current password of admin.</param>
        /// <returns>Success response if validation succesful else error response.</returns>
        Response ChangePasswordValidation(string username, string oldPassword);

        /// <summary>
        /// Deletes an admin user by ID.
        /// </summary>
        /// <param name="id">ID of the admin to delete.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response Delete(int id);

        /// <summary>
        /// Checks the record is exists for deletion or not.
        /// </summary>
        /// <param name="id">ID of the admin to delete.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response DeleteValidation(int id);

        #endregion Public Methods
    }
}