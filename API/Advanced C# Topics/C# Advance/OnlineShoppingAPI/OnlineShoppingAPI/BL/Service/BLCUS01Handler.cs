using OnlineShoppingAPI.BL.Common;
using OnlineShoppingAPI.BL.Interface;
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

namespace OnlineShoppingAPI.BL.Service
{
    /// <summary>
    /// Service implementation of <see cref="ICUS01Service"/>.
    /// </summary>
    public class BLCUS01Handler : ICUS01Service
    {
        #region Private Fields

        /// <summary>
        /// OrmLite Connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Insatnce of <see cref="CUS01"/>.
        /// </summary>
        private CUS01 _objCUS01;

        /// <summary>
        /// Instance of <see cref="USR01"/>.
        /// </summary>
        private USR01 _objUSR01;

        /// <summary>
        /// USR01 model services.
        /// </summary>
        private readonly IUSR01Service _usr01Service;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Specifies the operation to perform.
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BLCUS01Handler"/>.
        /// </summary>
        public BLCUS01Handler()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _usr01Service = new BLUSR01Handler();
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Changes the email address of a customer.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="newEmail">The new email address to set.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response ChangeEmail(string username, string newEmail)
        {
            USR01 objUser = _usr01Service.GetUser(username);

            _objCUS01.S01F03 = newEmail;
            objUser.R01F02 = newEmail.Split('@')[0];

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                using (IDbTransaction transaction = db.BeginTransaction())
                {
                    try
                    {
                        db.Update(_objCUS01);
                        db.Update(objUser);

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
        /// Validation for changing email.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="newEmail">The new email address to set.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response ChangeEmailValidation(string username, string password, string newEmail)
        {
            if (_usr01Service.GetUser(newEmail) != null)
                return NotFoundResponse("Use another email, this email is already exists.");

            using (var db = _dbFactory.OpenDbConnection())
            {
                _objCUS01 = db.Single(db.From<CUS01>()
                    .Where(c => c.S01F03.StartsWith(username) && c.S01F04.Equals(password)));
            }

            if (_objCUS01 == null)
                return NotFoundResponse("Customer doesn't exist");

            return OkResponse();
        }

        /// <summary>
        /// Changes the password of a customer.
        /// </summary>
        /// <param name="newPassword">The new password to set.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response ChangePassword(string newPassword)
        {
            _objCUS01.S01F04 = newPassword;
            _objUSR01.R01F03 = newPassword;
            _objUSR01.R01F05 = BLEncryption.GetEncryptPassword(newPassword);

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                using (IDbTransaction transaction = db.BeginTransaction())
                {
                    try
                    {
                        db.Update(_objCUS01);
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
        /// Validation for change password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="oldPassword">The old password of the user.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response ChangePasswordValidation(string username, string oldPassword)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                _objCUS01 = db.SingleWhere<CUS01>("S01F03", username + "@gmail.com");
                _objUSR01 = db.SingleWhere<USR01>("R01F02", username);
            }

            if (_objCUS01 == null)
            {
                return NotFoundResponse("Customer not found.");
            }

            if (_objCUS01.S01F04 != oldPassword)
            {
                return PreConditionFailedResponse("Password doesn't match.");
            }

            return OkResponse();
        }

        /// <summary>
        /// Deletes a customer.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response Delete()
        {
            string r01F02 = _objCUS01.S01F03.Split('@')[0];

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                using (IDbTransaction transaction = db.BeginTransaction())
                {
                    try
                    {
                        db.Delete(_objCUS01);
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

            return OkResponse("Customer deleted successfully.");
        }

        /// <summary>
        /// Validate delete record id.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response DeleteValidation(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                _objCUS01 = db.SingleById<CUS01>(id);
            }

            if (_objCUS01 == null)
            {
                return NotFoundResponse("Customer not found.");
            }

            return OkResponse();
        }

        /// <summary>
        /// Retrieves all customers information.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetAll()
        {
            List<CUS01> lstCUS01;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                lstCUS01 = db.Select<CUS01>();
            }

            if (lstCUS01 == null)
            {
                return NoContentResponse();
            }

            return OkResponse("", lstCUS01);
        }

        /// <summary>
        /// Retrieves a customer by their ID.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetById(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                _objCUS01 = db.SingleById<CUS01>(id);
            }

            if (_objCUS01 == null)
            {
                return NotFoundResponse("Customer not found.");
            }

            return OkResponse("", _objCUS01);
        }

        /// <summary>
        /// Prepares the objects for create or update operation.
        /// </summary>
        /// <param name="objCUS01DTO">The DTO object representing the customer.</param>
        public void PreSave(DTOCUS01 objCUS01DTO)
        {
            _objCUS01 = objCUS01DTO.Convert<CUS01>();

            if (Operation == EnmOperation.A)
            {
                _objUSR01 = new USR01()
                {
                    R01F02 = _objCUS01.S01F03.Split('@')[0],
                    R01F03 = _objCUS01.S01F04,
                    R01F04 = Roles.Customer,
                    R01F05 = BLEncryption.GetEncryptPassword(_objCUS01.S01F04)
                };
            }
        }

        /// <summary>
        /// Checks the model records exists or not.
        /// </summary>
        /// <param name="objDTOCUS01">DTO containing the customer information.</param>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response PreValidation(DTOCUS01 objDTOCUS01)
        {
            if (Operation == EnmOperation.A)
            {
                if (objDTOCUS01.S01F01 != 0)
                    return PreConditionFailedResponse("Id needs to be zero for add operation.");
            }
            else
            {
                if (objDTOCUS01.S01F01 <= 0)
                    return PreConditionFailedResponse("Id needs to be greater than zero update operation.");

                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    if (db.SingleById<CUS01>(objDTOCUS01.S01F01) == null)
                        return NotFoundResponse("Customer not found.");
                }
            }

            return OkResponse();
        }

        /// <summary>
        /// Saves changes according to the operation.
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
                            db.Insert(_objCUS01);
                            db.Insert(_objUSR01);

                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }

                    return OkResponse("Customer created successfully.");
                }

                CUS01 existingCUS01 = db.SingleById<CUS01>(_objCUS01.S01F01);

                // Update customer properties with the provided data.
                existingCUS01.S01F02 = _objCUS01.S01F02;
                existingCUS01.S01F05 = _objCUS01.S01F05;
                existingCUS01.S01F06 = _objCUS01.S01F06;

                // Perform the database update.
                db.Update(existingCUS01);
                return OkResponse("Data updated successfully.");
            }
        }

        /// <summary>
        /// Validates customer information.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Validation()
        {
            if (Operation == EnmOperation.A)
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    // Check if the email already exists in the database
                    if (db.Exists<USR01>(u => u.R01F02 == _objUSR01.R01F02))
                        return PreConditionFailedResponse("Email already exists.");
                }
            }

            return OkResponse();
        }

        #endregion Public Methods
    }
}