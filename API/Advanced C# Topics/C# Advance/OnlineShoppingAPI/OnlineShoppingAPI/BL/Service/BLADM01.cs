using OnlineShoppingAPI.BL.Common;
using OnlineShoppingAPI.BL.Interface;
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
using static OnlineShoppingAPI.BL.Common.BLHelper;

namespace OnlineShoppingAPI.BL.Service
{
    /// <summary>
    /// Service implementation of <see cref="IADM01Service"/>.
    /// </summary>
    public class BLADM01 : IADM01Service
    {
        #region Private Fields

        /// <summary>
        /// Instance of <see cref="ADM01"/>.
        /// </summary>
        private ADM01 _objADM01;

        /// <summary>
        /// Instance of <see cref="USR01"/>
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

        #endregion

        #region Public Properties

        /// <summary>
        /// Specifies Create or Delete Operation.
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BLADM01"/> class.
        /// </summary>
        public BLADM01()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _context = new DBADM01();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Basic prevalidation for cheking the admin exists or not.
        /// </summary>
        /// <param name="objDTOADM01">DTO of ADM01 model.</param>
        /// <param name="response">Response indictaing outcome of the operation.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool PreValidation(DTOADM01 objDTOADM01, out Response response)
        {
            response = null;
            return true;
        }

        /// <summary>
        /// Prepares admin and user objects before saving.
        /// </summary>
        /// <param name="objDTOADM01">DTO containing admin data.</param>
        public void PreSave(DTOADM01 objDTOADM01)
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
                response = ISEResponse();
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
                    db.Insert(_objADM01);
                    db.Insert(_objUSR01);
                }

                response = OkResponse("Customer created successfully.");
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
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
                    if (GetUser(newEmail) != null)
                    {
                        response = PreConditionFailedResponse("New email is already exists, use another email.");
                        return;
                    }

                    USR01 objUser = GetUser(username, password);
                    if (objUser == null)
                    {
                        response = NotFoundResponse("User not found.");
                        return;
                    }

                    ADM01 objAdmin = db.Single(db.From<ADM01>()
                        .Where(a => a.M01F03.StartsWith(username)));

                    objAdmin.M01F03 = newEmail;
                    objUser.R01F02 = newEmail.Split('@')[0];

                    db.Update(objAdmin);
                    db.Update(objUser);
                }

                response = OkResponse("Email changed successfully.");
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
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
                    USR01 objUser = GetUser(username, oldPassword);

                    if (objUser == null)
                    {
                        response = NotFoundResponse("User not found.");
                        return;
                    }

                    objUser.R01F03 = newPassword;
                    objUser.R01F05 = GetEncryptPassword(newPassword);

                    db.Update(objUser);
                    response = OkResponse("Password changed successfully.");
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        /// <summary>
        /// Deletes the admin with the specified ID.
        /// </summary>
        /// <param name="id">ID of the admin to be deleted.</param>
        /// <param name="response">Response indicating the outcome of the operation.</param>
        public void Delete(int id, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    ADM01 objADM01 = db.SingleById<ADM01>(id);

                    if (objADM01 == null)
                    {
                        response = NotFoundResponse("Admin not found.");
                    }
                    else
                    {
                        string username = objADM01.M01F03.Split('@')[0];

                        db.DeleteById<ADM01>(objADM01.M01F01);
                        db.Delete<USR01>(u => u.R01F02 == username);

                        response = OkResponse("Admin deleted successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
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
                    _context.GetProfit(date, out response);
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        #endregion
    }
}