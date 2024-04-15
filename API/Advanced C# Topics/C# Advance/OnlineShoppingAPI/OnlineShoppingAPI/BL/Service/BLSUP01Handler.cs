using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.Extension;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Web;
using static OnlineShoppingAPI.BL.Common.BLHelper;

namespace OnlineShoppingAPI.BL.Service
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
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Changes the email address of a suplier.
        /// </summary>
        /// <param name="username">The username of the suplier.</param>
        /// <param name="password">The password of the suplier.</param>
        /// <param name="newEmail">The new email address.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response ChangeEmail(string username, string password, string newEmail)
        {
            if (GetUser(newEmail) != null)
            {
                return PreConditionFailedResponse("Email already exists.");
            }

            using (var db = _dbFactory.OpenDbConnection())
            using (var transaction = db.BeginTransaction())
            {
                // Retrieve admin details.
                SUP01 objSuplier = db.Single(db.From<SUP01>()
                    .Where(s => s.P01F03.StartsWith(username) &&
                                s.P01F04.Equals(password)));

                // If suplier doesn't exist, return Not Found status code.
                if (objSuplier == null)
                {
                    return NotFoundResponse("Supplier not found.");
                }

                USR01 objUser = GetUser(username);

                // Update email and username
                objSuplier.P01F03 = newEmail;
                objUser.R01F02 = newEmail.Split('@')[0];

                try
                {
                    // Update data in the database.
                    db.Update(objSuplier);
                    db.Update(objUser);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

            return OkResponse("Email changed successfully.");
        }

        /// <summary>
        /// Changes the password of a suplier.
        /// </summary>
        /// <param name="username">The username of the suplier.</param>
        /// <param name="oldPassword">The old password of the suplier.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response ChangePassword(string username, string oldPassword, string newPassword)
        {
            using (var db = _dbFactory.OpenDbConnection())
            using (var transaction = db.BeginTransaction())
            {
                // Check if the user is a supplier or a regular user
                SUP01 existingSupplier = db.SingleWhere<SUP01>("P01F03", username + "@gmail.com");
                USR01 existingUser = db.SingleWhere<USR01>("R01F02", username);

                if (existingSupplier == null || existingUser == null)
                    return NotFoundResponse("Suplier doesn't exist.");

                // Verify the old password and update if correct
                if (existingSupplier.P01F04 != oldPassword)
                    return PreConditionFailedResponse("Password is incorrect.");

                existingSupplier.P01F04 = newPassword;
                existingUser.R01F03 = newPassword;
                existingUser.R01F05 = GetEncryptPassword(newPassword);

                try
                {
                    // Update supplier and user records
                    db.Update(existingSupplier);
                    db.Update(existingUser);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

            return OkResponse("Password changed successfully.");
        }

        /// <summary>
        /// Deletes a suplier.
        /// </summary>
        /// <param name="id">The ID of the suplier to delete.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response Delete(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            using (var transaction = db.BeginTransaction())
            {
                // Retrieve supplier information by id
                SUP01 supplier = db.SingleById<SUP01>(id);

                // Check if the supplier exists
                if (supplier == null)
                    return NotFoundResponse("Supplier not found.");

                // Extract username from email
                string username = supplier.P01F03.Split('@')[0];

                try
                {
                    // Delete supplier and associated user account
                    db.DeleteById<SUP01>(id);
                    db.DeleteWhere<USR01>("R01F02 = {0}", new object[] { username });

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

            return OkResponse("Supplier deleted successfully.");
        }

        /// <summary>
        /// Retrieves all suplier.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetAll()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                List<SUP01> lstSUP01 = db.Select<SUP01>();

                if (lstSUP01 == null || lstSUP01.Count == 0)
                    return NoContentResponse();

                return OkResponse("", lstSUP01);
            }
        }

        /// <summary>
        /// Retrieves a suplier by its ID.
        /// </summary>
        /// <param name="id">The ID of the suplier to retrieve.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetById(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
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
                    R01F05 = GetEncryptPassword(_objSUP01.P01F04)
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
                using (var db = _dbFactory.OpenDbConnection())
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
            using (var db = _dbFactory.OpenDbConnection())
            using (var transaction = db.BeginTransaction())
            {
                if (Operation == EnmOperation.A)
                {
                    try
                    {
                        db.Insert(_objSUP01);
                        db.Insert(_objUSR01);

                        transaction.Commit();
                        return OkResponse("Supplier created successfully.");
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
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
                using (var db = _dbFactory.OpenDbConnection())
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