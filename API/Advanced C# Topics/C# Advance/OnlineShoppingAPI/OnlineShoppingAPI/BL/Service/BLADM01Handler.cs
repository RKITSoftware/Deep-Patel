using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.DL;
using OnlineShoppingAPI.Extension;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Linq;
using System.Web;
using static OnlineShoppingAPI.BL.Common.BLHelper;

namespace OnlineShoppingAPI.BL.Service
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
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Changes the email address associated with the admin account.
        /// </summary>
        /// <param name="username">Username of the admin.</param>
        /// <param name="password">Password of the admin.</param>
        /// <param name="newEmail">New email address to be associated with the admin account.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response ChangeEmail(string username, string password, string newEmail)
        {
            // Check if the new email already exists
            if (GetUser(newEmail) != null)
                return PreConditionFailedResponse("New email is already exists, use another email.");

            using (var db = _dbFactory.OpenDbConnection())
            using (var transaction = db.BeginTransaction())
            {
                USR01 objUser = GetUser(username, password);
                if (objUser == null)
                    return NotFoundResponse("User not found.");

                ADM01 objAdmin = db.Single(db.From<ADM01>()
                    .Where(a => a.M01F03.StartsWith(username)));

                objAdmin.M01F03 = newEmail;
                objUser.R01F02 = newEmail.Split('@')[0];

                try
                {
                    db.Update(objAdmin);
                    db.Update(objUser);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return OkResponse("Email changed successfully.");
        }

        /// <summary>
        /// Changes the password of the admin.
        /// </summary>
        /// <param name="username">Username of the admin.</param>
        /// <param name="oldPassword">Old password of the admin.</param>
        /// <param name="newPassword">New password to be set for the admin.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response ChangePassword(string username, string oldPassword, string newPassword)
        {
            USR01 objUser = GetUser(username, oldPassword);

            if (objUser == null)
                return NotFoundResponse("User not found.");

            objUser.R01F03 = newPassword;
            objUser.R01F05 = GetEncryptPassword(newPassword);

            using (var db = _dbFactory.OpenDbConnection())
                db.Update(objUser);

            return OkResponse("Password changed successfully.");
        }

        /// <summary>
        /// Deletes the admin with the specified ID.
        /// </summary>
        /// <param name="id">ID of the admin to be deleted.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response Delete(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            using (var transaction = db.BeginTransaction())
            {
                ADM01 objADM01 = db.SingleById<ADM01>(id);

                if (objADM01 == null)
                    return NotFoundResponse("Admin not found.");

                string username = objADM01.M01F03.Split('@')[0];

                try
                {
                    db.DeleteById<ADM01>(objADM01.M01F01);
                    db.Delete<USR01>(u => u.R01F02 == username);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return OkResponse("Admin deleted successfully.");
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
                return NoContentResponse();

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
            _objUSR01.R01F05 = GetEncryptPassword(_objUSR01.R01F03);
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
            using (var db = _dbFactory.OpenDbConnection())
            {
                db.Insert(_objADM01);
                db.Insert(_objUSR01);
            }

            return OkResponse("Customer created successfully.");
        }

        /// <summary>
        /// Validates admin data.
        /// </summary>
        /// <returns>Success response if no error occur else response according to error.</returns>
        public Response Validation()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                // Check if the email already exists in the database
                if (db.Exists<USR01>(u => u.R01F02 == _objUSR01.R01F02))
                    return PreConditionFailedResponse("Username already exists");
            }

            return OkResponse();
        }

        #endregion Public Methods
    }
}