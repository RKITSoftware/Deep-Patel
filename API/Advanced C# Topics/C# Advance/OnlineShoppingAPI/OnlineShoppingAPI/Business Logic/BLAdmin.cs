using OnlineShoppingAPI.Dto;
using OnlineShoppingAPI.Extension;
using OnlineShoppingAPI.Mappers;
using OnlineShoppingAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
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
        /// Changes the password for an admin using the username and oldPassword
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
                    USR01 objUser = BLHelper.GetUser(username, oldPassword);

                    // If user doesn't exist, return Not Found status code.
                    if (objUser == null)
                        return new HttpResponseMessage(HttpStatusCode.NotFound);

                    // Update passwords
                    objUser.R01F03 = newPassword;
                    objUser.R01F05 = BLHelper.GetEncryptPassword(newPassword);

                    // Update data in the database.
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
        public HttpResponseMessage Create(DtoADM01 objAdminDto)
        {
            // PreSave
            ADMUSR objAdminUser = PreSaveAdmin(objAdminDto);

            // Validation
            if (!IsValidAdminUser(objAdminUser))
                return BLHelper.ResponseMessage(HttpStatusCode.PreconditionFailed,
                    "Details are incorrect or false.");

            // Save
            if (SaveAdmin(objAdminUser))
                return BLHelper.ResponseMessage(HttpStatusCode.Created,
                    "Admin Created Successfully.");

            return BLHelper.ResponseMessage(HttpStatusCode.OK,
                "An error occurred while saving the admin");
        }

        /// <summary>
        /// Deletes admin details using admin id. If only one admin exists, it won't delete the admin.
        /// </summary>
        /// <param name="id">Admin id</param>
        /// <returns>Delete response message</returns>
        public HttpResponseMessage Delete(int id)
        {
            ADM01 objAdmin = GetAdmin(id);

            if (objAdmin == null)
                return BLHelper.ResponseMessage(HttpStatusCode.NotFound,
                            "Sorry, the requested admin could not be found.");

            return DeleteAdmin(objAdmin);
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

                    // Retrieve User Details
                    USR01 objUser = BLHelper.GetUser(username, password);

                    // If user doesn't exist, return Not Found status code.
                    if (objUser == null)
                        return new HttpResponseMessage(HttpStatusCode.NotFound);

                    // Retrieve admin details.
                    ADM01 objAdmin = db.Single(db.From<ADM01>()
                        .Where(a => a.M01F03.StartsWith(username)));

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
                ex.LogException();
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request of changing email");
            }
        }

        /// <summary>
        /// Get the profit of that specific date
        /// </summary>
        /// <param name="time">Date format in dd-MM-yyyy format.</param>
        /// <returns>Profit on that date</returns>
        public decimal GetProfit(string date)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    decimal profit = db.Single<PFT01>(p => p.T01F02.Equals(date)).T01F03;
                    return profit;
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                return -1;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Mapping Admin Dto to Poco Model
        /// </summary>
        /// <param name="objAdminDto">Admin Dto</param>
        /// <returns>Admin user relational model</returns>
        private ADMUSR PreSaveAdmin(DtoADM01 objAdminDto)
        {
            return AdminMapper.ToPoco(objAdminDto);
        }

        /// <summary>
        /// Validates admin user relational model for further processing
        /// </summary>
        /// <param name="objAdminUser">Admin-User relation model</param>
        /// <returns>True if all model's property meet the criteria else returns False.</returns>
        private bool IsValidAdminUser(ADMUSR objAdminUser)
        {
            string namePattern = @"^[a-zA-Z]+(?:\s+[a-zA-Z]+)*$";

            if (!Regex.IsMatch(objAdminUser.M01.M01F02, namePattern))
                return false;

            if (BLHelper.IsExist(username: objAdminUser.R01.R01F02))
                return false;

            return true;
        }

        /// <summary>
        /// Adding Admin details to the database using Ormlite
        /// </summary>
        /// <param name="objAdminUser">Admin User Realtional Model</param>
        /// <returns>True if admin inserted into database else false.</returns>
        private bool SaveAdmin(ADMUSR objAdminUser)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Inserting admin details
                    db.Insert(objAdminUser.M01);
                    db.Insert(objAdminUser.R01);

                    // Return success response
                    return true;
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                return false;
            }
        }

        /// <summary>
        /// Getting admin details from the admin Id
        /// </summary>
        /// <param name="adminId">Admin Id</param>
        /// <returns>Admin if found else null.</returns>
        private ADM01 GetAdmin(int adminId)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve the list of admins
                    List<ADM01> lstAdmin = db.Select<ADM01>();

                    // Check if there is only one admin, in which case, deny deletion
                    if (lstAdmin.Count == 1)
                    {
                        return null;
                    }

                    // Find the admin by id
                    ADM01 objAdmin = lstAdmin.FirstOrDefault(a => a.M01F01 == adminId);
                    return objAdmin;
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                return null;
            }
        }

        /// <summary>
        /// Delete Admin details from the database.
        /// </summary>
        /// <param name="objAdmin">Delete Admin details</param>
        private HttpResponseMessage DeleteAdmin(ADM01 objAdmin)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Extracting information for USR01
                    string username = objAdmin.M01F03.Split('@')[0];

                    // Delete admin and related USR01 record
                    db.DeleteById<ADM01>(objAdmin.M01F01);
                    db.Delete<USR01>(u => u.R01F02 == username);

                    // Return success response
                    return BLHelper.ResponseMessage(HttpStatusCode.OK,
                        "Admin deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the delete request.");
            }
        }

        #endregion
    }
}