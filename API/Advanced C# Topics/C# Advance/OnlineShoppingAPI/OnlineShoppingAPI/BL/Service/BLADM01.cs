using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.DL;
using OnlineShoppingAPI.Extension;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using OnlineShoppingAPI.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Linq;
using System.Net;
using System.Web;

namespace OnlineShoppingAPI.BL.Service
{
    /// <summary>
    /// Service implementation for admin-related operations.
    /// </summary>
    public class BLADM01 : IADM01Service
    {
        /// <summary>
        /// <see cref="ADM01"/> object for request.
        /// </summary>
        private ADM01 _objADM01;

        /// <summary>
        /// <see cref="USR01"/> object for request.
        /// </summary>
        private USR01 _objUSR01;

        /// <summary>
        /// DB context for <see cref="BLADM01"/>.
        /// </summary>
        private readonly DBADM01 _context;

        /// <summary>
        /// Orm Lite Connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Initializes a new instance of the BLADM01 class.
        /// </summary>
        public BLADM01()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _context = new DBADM01();
        }

        /// <summary>
        /// Prepares admin and user objects before saving.
        /// </summary>
        /// <param name="objDTOADM01">DTO containing admin data.</param>
        /// <param name="create">Operation to be performed (create/update).</param>
        public void PreSave(DTOADM01 objDTOADM01, EnmOperation create)
        {
            _objADM01 = objDTOADM01.Convert<ADM01>();
            _objUSR01 = objDTOADM01.Convert<USR01>();

            _objUSR01.R01F02 = _objADM01.M01F03.Split('@')[0];
            _objUSR01.R01F04 = Roles.Admin;
            _objUSR01.R01F05 = BLHelper.GetEncryptPassword(_objUSR01.R01F03);
        }

        /// <summary>
        /// Validates admin data.
        /// </summary>
        /// <param name="response">Response indicating the outcome of the validation.</param>
        /// <returns>True if validation succeeds, otherwise false.</returns>
        public bool Validation(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Check if the email already exists in the database
                    if (db.Exists<USR01>(u => u.R01F02 == _objUSR01.R01F02))
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.PreconditionFailed,
                            Message = "Email is already exists"
                        };
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
                return false;
            }

            // Validation successful, set response to null
            response = null;
            return true;
        }

        /// <summary>
        /// Saves the admin and user objects to the database.
        /// </summary>
        /// <param name="response">Response indicating the outcome of the save operation.</param>
        public void Save(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Insert admin and user records into the database
                    db.Insert(_objADM01);
                    db.Insert(_objUSR01);
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
                return;
            }

            // Set success response
            response = new Response()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Admin created successfully."
            };
        }

        /// <summary>
        /// Changes the email address associated with the admin account.
        /// </summary>
        /// <param name="username">Username of the admin.</param>
        /// <param name="password">Password of the admin.</param>
        /// <param name="newEmail">New email address to be associated with the admin account.</param>
        /// <param name="response">Response indicating the outcome of the operation.</param>
        public void ChangeEmail(string username, string password, string newEmail,
            out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Check if the new email already exists
                    if (BLHelper.GetUser(newEmail) != null)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.PreconditionFailed,
                            Message = "New email is already exists, use another email."
                        };
                        return;
                    }

                    // Retrieve User Details
                    USR01 objUser = BLHelper.GetUser(username, password);

                    // If user doesn't exist, return Not Found Response.
                    if (objUser == null)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "User can't be found."
                        };
                        return;
                    }

                    // Retrieve admin details.
                    ADM01 objAdmin = db.Single(db.From<ADM01>()
                        .Where(a => a.M01F03.StartsWith(username)));

                    // Update email and username
                    objAdmin.M01F03 = newEmail;
                    objUser.R01F02 = newEmail.Split('@')[0];

                    // Update data in the database.
                    db.Update(objAdmin);
                    db.Update(objUser);
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
                return;
            }

            // Set success response
            response = new Response()
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Email Changed Successfully."
            };
        }

        /// <summary>
        /// Changes the password of the admin.
        /// </summary>
        /// <param name="username">Username of the admin.</param>
        /// <param name="oldPassword">Old password of the admin.</param>
        /// <param name="newPassword">New password to be set for the admin.</param>
        /// <param name="response">Response indicating the outcome of the operation.</param>
        public void ChangePassword(string username, string oldPassword, string newPassword,
            out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve user details using username and old password
                    USR01 objUser = BLHelper.GetUser(username, oldPassword);

                    // If user doesn't exist, return Not Found status code.
                    if (objUser == null)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "User not found."
                        };
                        return;
                    }

                    // Update passwords
                    objUser.R01F03 = newPassword;
                    objUser.R01F05 = BLHelper.GetEncryptPassword(newPassword);

                    // Update data in the database.
                    db.Update(objUser);

                    // Set success response
                    response = new Response()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Password changed successfully."
                    };
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
            }
        }

        /// <summary>
        /// Deletes the admin with the specified ID.
        /// </summary>
        /// <param name="id">ID of the admin to be deleted.</param>
        /// <param name="response">Response indicating the outcome of the operation.</param>
        public void Delete(int id, out Response response)
        {
            // Retrieve admin details based on ID
            ADM01 objAdmin = GetAdmin(id);

            // If admin doesn't exist, return Not Found response
            if (objAdmin == null)
            {
                response = new Response()
                {
                    IsError = true,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Admin not found."
                };
                return;
            }

            // Delete admin record
            Delete(objAdmin, out response);
        }

        /// <summary>
        /// Deletes the specified admin record and its related user record.
        /// </summary>
        /// <param name="objAdmin">Admin object to be deleted.</param>
        /// <param name="response">Response indicating the outcome of the operation.</param>
        private void Delete(ADM01 objAdmin, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Extract username from admin email
                    string username = objAdmin.M01F03.Split('@')[0];

                    // Delete admin and related USR01 record
                    db.DeleteById<ADM01>(objAdmin.M01F01);
                    db.Delete<USR01>(u => u.R01F02 == username);

                    // Set success response
                    response = new Response()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Admin deleted successfully."
                    };
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
            }
        }

        /// <summary>
        /// Retrieves the admin record with the specified ID.
        /// </summary>
        /// <param name="id">ID of the admin to retrieve.</param>
        /// <returns>The admin record if found, otherwise null.</returns>
        private ADM01 GetAdmin(int id)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    return db.SingleById<ADM01>(id);
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                return null;
            }
        }

        /// <summary>
        /// Retrieves the profit for the specified date.
        /// </summary>
        /// <param name="date">Date for which profit is to be retrieved.</param>
        /// <param name="response">Response indicating the outcome of the operation.</param>
        public void GetProfit(string date, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve profit data using DB context
                    _context.GetProfit(date, out response);
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
            }
        }
    }
}