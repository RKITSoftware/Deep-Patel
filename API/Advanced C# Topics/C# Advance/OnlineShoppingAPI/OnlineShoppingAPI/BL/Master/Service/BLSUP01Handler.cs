using OnlineShoppingAPI.BL.Common;
using OnlineShoppingAPI.BL.Master.Interface;
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
    /// BL Handler for SUP01 model.
    /// </summary>
    public class BLSUP01Handler : ISUP01Service
    {
        #region Private Fields

        /// <summary>
        /// Orm Lite Connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// USR01 model services.
        /// </summary>
        private readonly IUSR01Service _usr01Service;

        /// <summary>
        /// Instance of <see cref="SUP01"/>.
        /// </summary>
        private SUP01 _objSUP01;

        /// <summary>
        /// Instance of <see cref="usr01"/>.
        /// </summary>
        private USR01 _objUSR01;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Specifies the operation to perform.
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion Public Properties

        #region Constructor

        /// <summary>
        /// Initialize the <see cref="BLSUP01Handler"/> insatnces.
        /// </summary>
        public BLSUP01Handler()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _usr01Service = new BLUSR01Handler();
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Changes the email address of a suplier.
        /// </summary>
        /// <param name="username">The username of the suplier.</param>
        /// <param name="newEmail">The new email address.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response ChangeEmail(string username, string newEmail)
        {
            _objUSR01 = _usr01Service.GetUser(username);

            // Update email and username
            _objSUP01.P01F03 = newEmail;
            _objUSR01.R01F02 = newEmail.Split('@')[0];

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                using (IDbTransaction transaction = db.BeginTransaction())
                {
                    try
                    {
                        db.Update(_objSUP01);
                        db.Update(_objUSR01);

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
        /// Validates the parameters before the changing email.
        /// </summary>
        /// <param name="username">The username of the suplier.</param>
        /// <param name="password">The password of the suplier.</param>
        /// <param name="newEmail">The new email address.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response ChangeEmailValidation(string username, string password, string newEmail)
        {
            if (_usr01Service.GetUser(newEmail) != null)
            {
                return PreConditionFailedResponse("Email already exists.");
            }

            using (var db = _dbFactory.OpenDbConnection())
            {
                _objSUP01 = db.Single(db.From<SUP01>()
                    .Where(s => s.P01F03.StartsWith(username) &&
                                s.P01F04.Equals(password)));
            }

            if (_objSUP01 == null)
            {
                return NotFoundResponse("Supplier not found.");
            }

            return OkResponse();
        }

        /// <summary>
        /// Changes the password of a suplier.
        /// </summary>
        /// <param name="newPassword">The new password.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response ChangePassword(string newPassword)
        {
            _objSUP01.P01F04 = newPassword;
            _objUSR01.R01F03 = newPassword;
            _objUSR01.R01F05 = BLEncryption.GetEncryptPassword(newPassword);

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                using (IDbTransaction transaction = db.BeginTransaction())
                {
                    try
                    {
                        db.Update(_objSUP01);
                        db.Update(_objUSR01);

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return OkResponse("Password changed successfully.");
        }

        /// <summary>
        /// Validation before the change password.
        /// </summary>
        /// <param name="username">The username of the suplier.</param>
        /// <param name="oldPassword">The old password of the suplier.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response ChangePasswordValidation(string username, string oldPassword)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                _objSUP01 = db.SingleWhere<SUP01>("P01F03", username + "@gmail.com");
                _objUSR01 = db.SingleWhere<USR01>("R01F02", username);
            }

            if (_objSUP01 == null || _objUSR01 == null)
            {
                return NotFoundResponse("Suplier doesn't exist.");
            }

            // Verify the old password and update if correct
            if (_objSUP01.P01F04 != oldPassword)
            {
                return PreConditionFailedResponse("Password is incorrect.");
            }

            return OkResponse();
        }

        /// <summary>
        /// Deletes a suplier.
        /// </summary>
        /// <param name="id">The ID of the suplier to delete.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response Delete(int id)
        {
            string r01F02 = _objSUP01.P01F03.Split('@')[0];

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                using (IDbTransaction transaction = db.BeginTransaction())
                {
                    try
                    {
                        db.DeleteById<SUP01>(id);
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

            return OkResponse("Supplier deleted successfully.");
        }

        /// <summary>
        /// Delete Validation
        /// </summary>
        /// <param name="id">The ID of the suplier to delete.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response DeleteValidation(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                _objSUP01 = db.SingleById<SUP01>(id);
            }

            if (_objSUP01 == null)
            {
                return NotFoundResponse("Supplier not found.");
            }

            return OkResponse();
        }

        /// <summary>
        /// Retrieves all suplier.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetAll()
        {
            List<SUP01> lstSUP01;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                lstSUP01 = db.Select<SUP01>();
            }

            if (lstSUP01 == null || lstSUP01.Count == 0)
            {
                return NoContentResponse();
            }

            return OkResponse("", lstSUP01);
        }

        /// <summary>
        /// Retrieves a suplier by its ID.
        /// </summary>
        /// <param name="id">The ID of the suplier to retrieve.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetById(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                SUP01 objSUP01 = db.SingleById<SUP01>(id);
                return objSUP01 != null ? OkResponse("", objSUP01) : NotFoundResponse("Supplier not found.");
            }
        }

        /// <summary>
        /// Prepares a suplier for saving based on the operation.
        /// </summary>
        /// <param name="objSUP01DTO">The DTO object representing the suplier.</param>
        public void PreSave(DTOSUP01 objSUP01DTO)
        {
            _objSUP01 = objSUP01DTO.Convert<SUP01>();

            if (Operation == EnmOperation.A)
            {
                _objUSR01 = new USR01()
                {
                    R01F02 = _objSUP01.P01F03.Split('@')[0],
                    R01F03 = _objSUP01.P01F04,
                    R01F04 = Roles.Supplier,
                    R01F05 = BLEncryption.GetEncryptPassword(_objSUP01.P01F04)
                };
            }
        }

        /// <summary>
        /// Checks the records exists or not for operation.
        /// </summary>
        /// <param name="objDTOSUP01">DTO containing the Supplier information.</param>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response PreValidation(DTOSUP01 objDTOSUP01)
        {
            if (Operation == EnmOperation.E)
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    if (db.SingleById<SUP01>(objDTOSUP01.P01F01) == null)
                        return NotFoundResponse("Supplier not found.");
                }
            }

            return OkResponse();
        }

        /// <summary>
        /// Saves the changes made to a supplier.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Save()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                if (Operation == EnmOperation.A)
                {
                    using (IDbTransaction transaction = db.BeginTransaction())
                    {
                        try
                        {
                            db.Insert(_objSUP01);
                            db.Insert(_objUSR01);

                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }

                        return OkResponse("Supplier created successfully.");
                    }
                }

                db.Update(_objSUP01);
                return OkResponse("Supplier information updated successfully.");
            }
        }

        /// <summary>
        /// Performs validation on the supplier data.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Validation()
        {
            if (Operation == EnmOperation.A)
            {
                if (_usr01Service.IsExist(_objUSR01.R01F02, _objUSR01.R01F03))
                {
                    return PreConditionFailedResponse("Username already exists choose another.");
                }
            }

            return OkResponse();
        }

        #endregion Public Methods
    }
}