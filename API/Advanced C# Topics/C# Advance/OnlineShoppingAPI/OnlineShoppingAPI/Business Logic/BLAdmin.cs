using OnlineShoppingAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace OnlineShoppingAPI.Business_Logic
{
    /// <summary>
    /// BLAdmin class contains the business logic of CLAdminController
    /// </summary>
    public class BLAdmin
    {
        #region Private Fields

        /// <summary>
        /// _dbFactory is used to store the reference of database connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Static constructor is used to initialize _dbfactory for future reference.
        /// </summary>
        /// <exception cref="ApplicationException">
        /// If database can't connect then this exception shows.
        /// </exception>
        public BLAdmin()
        {
            // Getting data connection from Application state
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            // If database can't be connect.
            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Changes the password for an admin using the username.
        /// </summary>
        /// <param name="username">Admin username</param>
        /// <param name="oldPassword">Old password</param>
        /// <param name="newPassword">New password</param>
        /// <returns>Ok response</returns>
        public HttpResponseMessage ChangePassword(string username,
                string oldPassword, string newPassword)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve admin details.
                    ADM01 objAdmin = db.Single(db.From<ADM01>()
                        .Where(a => a.M01F03.StartsWith(username) &&
                                    a.M01F04.Equals(oldPassword)));

                    USR01 objUser = db.Single(db.From<USR01>()
                        .Where(u => u.R01F02.StartsWith(username)));

                    // If admin doesn't exist, return Not Found status code.
                    if (objAdmin == null)
                        return new HttpResponseMessage(HttpStatusCode.NotFound);

                    // Update passwords
                    objAdmin.M01F04 = newPassword;
                    objUser.R01F03 = newPassword;
                    objUser.R01F05 = BLHelper.GetEncryptPassword(newPassword);

                    // Update data in the database.
                    db.Update(objAdmin);
                    db.Update(objUser);

                    // Return success response
                    return BLHelper.ResponseMessage(HttpStatusCode.OK,
                        "Password changed successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request of changing password");
            }
        }


        /// <summary>
        /// Creates a new admin along with the associated user record.
        /// </summary>
        /// <param name="objAdmin">Admin data</param>
        /// <returns>Create response</returns>
        public HttpResponseMessage Create(ADM01 objAdmin)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Inserting admin details
                    db.Insert(objAdmin);

                    // Extracting information for USR01
                    var emailParts = objAdmin.M01F03.Split('@');
                    var username = emailParts[0];
                    var newPassword = objAdmin.M01F04;

                    // Inserting related USR01 record
                    db.Insert(new USR01
                    {
                        R01F02 = username,
                        R01F03 = newPassword,
                        R01F04 = "Admin",
                        R01F05 = BLHelper.GetEncryptPassword(newPassword)
                    });

                    // Return success response
                    return BLHelper.ResponseMessage(HttpStatusCode.Created,
                        "Admin created successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the create request.");
            }
        }


        /// <summary>
        /// Deletes admin details using admin id. If only one admin exists, it won't delete the admin.
        /// </summary>
        /// <param name="id">Admin id</param>
        /// <returns>Delete response message</returns>
        public HttpResponseMessage Delete(int id)
        {
            // Using a try-catch block to handle any potential exceptions
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve the list of admins
                    List<ADM01> lstAdmin = db.Select<ADM01>();

                    // Check if there is only one admin, in which case, deny deletion
                    if (lstAdmin.Count == 1)
                    {
                        return BLHelper.ResponseMessage(HttpStatusCode.Forbidden,
                            "Server can't fulfill the request because there is only one admin.");
                    }

                    // Find the admin by id
                    ADM01 adminToDelete = lstAdmin.FirstOrDefault(a => a.M01F01 == id);

                    // If the admin with the given id is not found, return Not Found status
                    if (adminToDelete == null)
                    {
                        return BLHelper.ResponseMessage(HttpStatusCode.NotFound,
                            "Admin not found.");
                    }

                    // Extracting information for USR01
                    string username = adminToDelete.M01F03.Split('@')[0];

                    // Delete admin and related USR01 record
                    db.DeleteById<ADM01>(id);
                    db.Delete<USR01>(u => u.R01F02 == username);

                    // Return success response
                    return BLHelper.ResponseMessage(HttpStatusCode.OK,
                        "Admin deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the delete request.");
            }
        }

        /// <summary>
        /// Changes the email for an admin using the username.
        /// </summary>
        /// <param name="username">Admin username</param>
        /// <param name="password">Admin password</param>
        /// <param name="newEmail">New email</param>
        /// <returns>Ok response</returns>
        public HttpResponseMessage ChangeEmail(string username, string password, string newEmail)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (BLHelper.GetUser(newEmail) != null)
                    {
                        return BLHelper.ResponseMessage(HttpStatusCode.PreconditionFailed,
                            "New email is invalid");
                    }

                    // Retrieve admin details.
                    ADM01 objAdmin = db.Single(db.From<ADM01>()
                        .Where(a => a.M01F03.StartsWith(username) &&
                                    a.M01F04.Equals(password)));

                    USR01 objUser = BLHelper.GetUser(username);

                    // If admin doesn't exist, return Not Found status code.
                    if (objAdmin == null)
                        return new HttpResponseMessage(HttpStatusCode.NotFound);

                    // Update email and username
                    objAdmin.M01F03 = newEmail;
                    objUser.R01F02 = newEmail.Split('@')[0];

                    // Update data in the database.
                    db.Update(objAdmin);
                    db.Update(objUser);

                    // Return success response
                    return BLHelper.ResponseMessage(HttpStatusCode.OK,
                        "Email changed successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request of changing email");
            }
        }

        #endregion
    }
}