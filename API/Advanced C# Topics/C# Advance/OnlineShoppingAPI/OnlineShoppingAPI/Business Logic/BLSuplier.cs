using OnlineShoppingAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Caching;

namespace OnlineShoppingAPI.Business_Logic
{
    public class BLSuplier
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
        /// <exception cref="ApplicationException">If database can't connect then 
        /// this exception shows.</exception>
        public BLSuplier()
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
        /// Changes the password for a user with the specified username.
        /// </summary>
        /// <param name="username">Username of the user</param>
        /// <param name="oldPassword">Old password for verification</param>
        /// <param name="newPassword">New password to set</param>
        /// <returns>
        /// Response indicating the success or failure of the password change operation
        /// </returns>
        public HttpResponseMessage ChangePassword(string username, string oldPassword,
            string newPassword)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    BLHelper.ServerCache.Remove("lstSuplier");

                    // Check if the user is a supplier or a regular user
                    SUP01 existingSupplier =
                        db.SingleWhere<SUP01>("P01F03", username + "@gmail.com");
                    USR01 existingUser = db.SingleWhere<USR01>("R01F02", username);

                    if (existingSupplier == null || existingUser == null)
                    {
                        // Return Not Found response if the user is not found
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }

                    // Verify the old password and update if correct
                    if (existingSupplier.P01F04 == oldPassword)
                    {
                        existingSupplier.P01F04 = newPassword;
                        existingUser.R01F03 = newPassword;
                        existingUser.R01F05 = BLHelper.GetEncryptPassword(newPassword);

                        // Update supplier and user records
                        db.Update(existingSupplier);
                        db.Update(existingUser);

                        // Return success response
                        return BLHelper.ResponseMessage(HttpStatusCode.OK,
                            "Password changed successfully.");
                    }
                    else
                    {
                        // Return Precondition Failed response if the old password is incorrect
                        return BLHelper.ResponseMessage(HttpStatusCode.PreconditionFailed,
                            "Password is incorrect.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response in case of an error
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Creates a new supplier record and associated user account in the database.
        /// </summary>
        /// <param name="objNewSuplier">Supplier information</param>
        /// <returns>Create response message indicating success or failure</returns>
        public HttpResponseMessage Create(SUP01 objNewSuplier)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Insert new supplier record
                    db.Insert(objNewSuplier);

                    // Insert corresponding user account for the supplier
                    db.Insert(new USR01
                    {
                        R01F02 = objNewSuplier.P01F03.Split('@')[0], // Extract username from email
                        R01F03 = objNewSuplier.P01F04, // Use supplier's password
                        R01F04 = "Supplier",
                        R01F05 = BLHelper.GetEncryptPassword(objNewSuplier.P01F04)
                    });

                    BLHelper.ServerCache.Remove("lstSuplier");

                    // Return success response
                    return BLHelper.ResponseMessage(HttpStatusCode.Created,
                        "Supplier created successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response in case of an error
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Creates multiple supplier records and associated user accounts from a list in the database.
        /// </summary>
        /// <param name="lstNewSupliers">List of new supplier information</param>
        /// <returns>Create response message indicating success or failure</returns>
        public HttpResponseMessage CreateFromList(List<SUP01> lstNewSupliers)
        {
            try
            {
                // Check if the list is empty
                if (lstNewSupliers.Count == 0)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.BadRequest,
                        "Data is empty");
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Insert all new supplier records
                    db.InsertAll(lstNewSupliers);

                    // Insert corresponding user accounts for the suppliers
                    foreach (SUP01 item in lstNewSupliers)
                    {
                        db.Insert(new USR01
                        {
                            R01F02 = item.P01F03.Split('@')[0], // Extract username from email
                            R01F03 = item.P01F04, // Use supplier's password
                            R01F04 = "Supplier",
                            R01F05 = BLHelper.GetEncryptPassword(item.P01F04) // Encrypt the password
                        });
                    }

                    BLHelper.ServerCache.Remove("lstSuplier");

                    // Return success response
                    return BLHelper.ResponseMessage(HttpStatusCode.Created,
                        "Suppliers created successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response in case of an error
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Deletes a supplier and its associated user account from the database 
        /// based on the supplied id.
        /// </summary>
        /// <param name="id">Supplier id</param>
        /// <returns>Delete response message indicating success or failure</returns>
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                // Check if the id is valid
                if (id <= 0)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.BadRequest,
                        "Id can't be zero or negative.");
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve supplier information by id
                    SUP01 supplier = db.SingleById<SUP01>(id);

                    // Check if the supplier exists
                    if (supplier == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }

                    // Extract username from email
                    string username = supplier.P01F03.Split('@')[0];

                    // Delete supplier and associated user account
                    db.DeleteById<SUP01>(id);
                    db.DeleteWhere<USR01>("R01F02 = {0}", new object[] { username });

                    BLHelper.ServerCache.Remove("lstSuplier");

                    // Return success response
                    return BLHelper.ResponseMessage(HttpStatusCode.OK,
                        "Supplier deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response in case of an error
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Retrieves a list of all suppliers from the database.
        /// </summary>
        /// <returns>List of suppliers or an empty list if no suppliers are found.</returns>
        public List<SUP01> GetAll()
        {
            try
            {
                List<SUP01> lstSuplier = BLHelper.ServerCache.Get("lstSuplier") as List<SUP01>;

                if (lstSuplier != null)
                {
                    return lstSuplier;
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    lstSuplier = db.Select<SUP01>();

                    BLHelper.ServerCache.Add("lstSuplier",
                        lstSuplier, null,
                        DateTime.MaxValue, TimeSpan.FromMinutes(20),
                        CacheItemPriority.Default, null);

                    return lstSuplier;
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// Updates supplier information in the database.
        /// </summary>
        /// <param name="objUpdatedSupplier">Updated supplier information</param>
        /// <returns>Update response message</returns>
        public HttpResponseMessage Update(SUP01 objUpdatedSupplier)
        {
            try
            {
                if (objUpdatedSupplier.P01F01 <= 0)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.BadRequest,
                        "Id can't be zero or negative.");
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    SUP01 existingSupplier = db.SingleById<SUP01>(objUpdatedSupplier.P01F01);

                    if (existingSupplier == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }

                    existingSupplier.P01F02 = objUpdatedSupplier.P01F02;
                    existingSupplier.P01F05 = objUpdatedSupplier.P01F05;
                    existingSupplier.P01F06 = objUpdatedSupplier.P01F06;

                    db.Update(existingSupplier);
                    BLHelper.ServerCache.Remove("lstSuplier");

                    return BLHelper.ResponseMessage(HttpStatusCode.OK,
                        "Supplier updated successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Changes the email for an suplier using the username.
        /// </summary>
        /// <param name="username">Suplier username</param>
        /// <param name="password">Suplier password</param>
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
                    SUP01 objSuplier = db.Single(db.From<SUP01>()
                        .Where(s => s.P01F03.StartsWith(username) &&
                                    s.P01F04.Equals(password)));

                    USR01 objUser = BLHelper.GetUser(username);

                    // If admin doesn't exist, return Not Found status code.
                    if (objSuplier == null)
                        return new HttpResponseMessage(HttpStatusCode.NotFound);

                    // Update email and username
                    objSuplier.P01F03 = newEmail;
                    objUser.R01F02 = newEmail.Split('@')[0];

                    // Update data in the database.
                    db.Update(objSuplier);
                    db.Update(objUser);

                    BLHelper.ServerCache.Remove("lstSuplier");

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

        /// <summary>
        /// Gets the suplier data from the cache if exist otherwise from the database.
        /// </summary>
        /// <param name="id">Suplier id for finding suplier information.</param>
        /// <returns>Suplier information</returns>
        public SUP01 Get(int id)
        {
            try
            {
                List<SUP01> lstSuplier = BLHelper.ServerCache.Get("lstSuplier") as List<SUP01>;
                if (lstSuplier != null)
                {
                    return lstSuplier.FirstOrDefault(s => s.P01F01 == id);
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    SUP01 objSuplier = db.Single<SUP01>(s => s.P01F01 == id);
                    return objSuplier;
                }
            }
            catch (Exception ex)
            {
                BLHelper.LogError(ex);
                return null;
            }
        }

        #endregion
    }
}