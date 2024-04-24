using OnlineShoppingAPI.BL.Common;
using OnlineShoppingAPI.BL.Master.Interface;
using OnlineShoppingAPI.DL;
using OnlineShoppingAPI.Extension;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using static OnlineShoppingAPI.BL.Common.BLHelper;

namespace OnlineShoppingAPI.BL.Master.Service
{
    /// <summary>
    /// Service for <see cref="ADM01"/> model.
    /// </summary>
    public class BLADM01Handler : IADM01Service
    {
        #region Private Fields

        /// <summary>
        /// DB context for <see cref="BLADM01Handler"/>.
        /// </summary>
        private readonly DBADM01Context _dbADM01Context;

        /// <summary>
        /// Orm Lite Connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// USR01 model services.
        /// </summary>
        private readonly IUSR01Service _usr01Service;

        /// <summary>
        /// Instance of <see cref="ADM01"/>.
        /// </summary>
        private ADM01 _objADM01;

        /// <summary>
        /// Instance of <see cref="USR01"/>
        /// </summary>
        private USR01 _objUSR01;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Specifies Create or Delete Operation.
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion Public Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BLADM01Handler"/> class.
        /// </summary>
        public BLADM01Handler()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _dbADM01Context = new DBADM01Context();
            _usr01Service = new BLUSR01Handler();
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Changes the email address associated with the admin account.
        /// </summary>
        /// <param name="username">Username of the admin.</param>
        /// <param name="newEmail">New email address to be associated with the admin account.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response ChangeEmail(string username, string newEmail)
        {
            string newR01F02 = newEmail.Split('@')[0];
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                using (IDbTransaction transaction = db.BeginTransaction())
                {
                    try
                    {
                        db.UpdateOnly<ADM01>(
                            updateFields: new Dictionary<string, object>
                                {
                                    { "M01F03", newEmail}
                                },
                            obj: m => m.M01F03.StartsWith(username));

                        db.UpdateOnly<USR01>(
                          updateFields: new Dictionary<string, object>
                            {
                                {"R01F02", newR01F02 }
                            },
                          obj: u => u.R01F02 == username);

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return OkResponse("Email changed successfully.");
        }

        /// <summary>
        /// Validate the data is correct or not for the change email process.
        /// </summary>
        /// <param name="username">Username of the admin.</param>
        /// <param name="password">Password of the admin.</param>
        /// <param name="newEmail">New email address to set.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response ChangeEmailValidation(string username, string password, string newEmail)
        {
            if (_usr01Service.GetUser(newEmail) != null)
            {
                return PreConditionFailedResponse("New email is already exists, use another email.");
            }

            bool isUSR01Exist = _usr01Service.IsExist(username, password);
            if (!isUSR01Exist)
            {
                return NotFoundResponse("User not found.");
            }

            return OkResponse();
        }

        /// <summary>
        /// Changes the password of the admin.
        /// </summary>
        /// <param name="newPassword">New password to be set for the admin.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response ChangePassword(string newPassword)
        {
            string encryptedPassword = BLEncryption.GetEncryptPassword(newPassword);

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.UpdateOnly<USR01>(
                    updateFields: new Dictionary<string, object>
                    {
                        { "R01F03", newPassword },
                        { "R01F05", encryptedPassword }
                    },
                    obj: u => u.R01F02 == _objUSR01.R01F02);
            }

            return OkResponse("Password changed successfully.");
        }

        /// <summary>
        /// Valiation before the change password
        /// </summary>
        /// <param name="username">Username of the admin.</param>
        /// <param name="oldPassword">Current password of admin.</param>
        /// <returns>Success response if validation succesful else error response.</returns>
        public Response ChangePasswordValidation(string username, string oldPassword)
        {
            _objUSR01 = _usr01Service.GetUser(username, oldPassword);

            if (_objUSR01 == null)
            {
                return NotFoundResponse("User not found.");
            }

            return OkResponse();
        }

        /// <summary>
        /// Deletes the admin with the specified ID.
        /// </summary>
        /// <param name="id">ID of the admin to be deleted.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response Delete(int id)
        {
            string r01F02 = _objADM01.M01F03.Split('@')[0];

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                using (IDbTransaction transaction = db.BeginTransaction())
                {
                    try
                    {
                        db.DeleteById<ADM01>(_objADM01.M01F01);
                        db.Delete<USR01>(u => u.R01F02 == r01F02);

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return OkResponse("Admin deleted successfully.");
        }

        /// <summary>
        /// Checks the record is exists for deletion or not.
        /// </summary>
        /// <param name="id">ID of the admin to delete.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response DeleteValidation(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                _objADM01 = db.SingleById<ADM01>(id);
            }

            if (_objADM01 == null)
            {
                return NotFoundResponse("Admin not found.");
            }

            return OkResponse();
        }

        /// <summary>
        /// Retrieves the profit for the specified date.
        /// </summary>
        /// <param name="date">Date for which profit is to be retrieved.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetProfit(string date)
        {
            DataTable dtProfit = _dbADM01Context.GetProfit(date);

            if (dtProfit.Rows.Count == 0)
            {
                return NoContentResponse();
            }

            return OkResponse("", dtProfit);
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
            _objUSR01.R01F05 = BLEncryption.GetEncryptPassword(_objUSR01.R01F03);
        }

        /// <summary>
        /// Basic prevalidation for cheking the admin exists or not.
        /// </summary>
        /// <param name="objDTOADM01">DTO of ADM01 model.</param>
        /// <returns>Response according to the operation.</returns>
        public Response PreValidation(DTOADM01 objDTOADM01)
        {
            return OkResponse();
        }

        /// <summary>
        /// Saves the admin and user objects to the database.
        /// </summary>
        /// <returns>Success response if data saved successfully else response according to error.</returns>
        public Response Save()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                using (IDbTransaction transaction = db.BeginTransaction())
                {
                    try
                    {
                        db.Save(_objADM01);
                        db.Save(_objUSR01);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return OkResponse("Customer created successfully.");
        }

        /// <summary>
        /// Validates admin data.
        /// </summary>
        /// <returns>Success response if no error occur else response according to error.</returns>
        public Response Validation()
        {
            bool isUSR01Exist;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                isUSR01Exist = db.Exists<USR01>(u => u.R01F02 == _objUSR01.R01F02);
            }

            if (isUSR01Exist)
            {
                return PreConditionFailedResponse("Username already exists");
            }

            return OkResponse();
        }

        #endregion Public Methods
    }
}