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
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Changes the email address of a customer.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="newEmail">The new email address to set.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response ChangeEmail(string username, string password, string newEmail)
        {
            if (GetUser(newEmail) != null)
                return NotFoundResponse("Use another email, this email is already exists.");

            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve admin details.
                    CUS01 objCustomer = db.Single(db.From<CUS01>()
                        .Where(c => c.S01F03.StartsWith(username) && c.S01F04.Equals(password)));

                    if (objCustomer == null)
                        return NotFoundResponse("Customer doesn't exist");

                    USR01 objUser = GetUser(username);

                    // Update email and username
                    objCustomer.S01F03 = newEmail;
                    objUser.R01F02 = newEmail.Split('@')[0];

                    // Update data in the database.
                    db.Update(objCustomer);
                    db.Update(objUser);
                }
            }
            catch (Exception ex) { throw ex; }

            return OkResponse("Email changed successfully.");
        }

        /// <summary>
        /// Changes the password of a customer.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="oldPassword">The old password of the user.</param>
        /// <param name="newPassword">The new password to set.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve existing customer and user by username.
                    CUS01 existingCustomer = db.SingleWhere<CUS01>("S01F03", username + "@gmail.com");

                    if (existingCustomer == null)
                        return NotFoundResponse("Customer not found.");

                    USR01 existingUser = db.SingleWhere<USR01>("R01F02", username);

                    // Check if the provided old password matches the existing customer password.
                    if (!existingCustomer.S01F04.Equals(oldPassword))
                        return PreConditionFailedResponse("Password doesn't match.");

                    // Update customer and user passwords with the new password.
                    existingCustomer.S01F04 = newPassword;
                    existingUser.R01F03 = newPassword;
                    existingUser.R01F05 = GetEncryptPassword(newPassword);

                    // Perform the database updates.
                    db.Update(existingCustomer);
                    db.Update(existingUser);
                }
            }
            catch (Exception ex) { throw ex; }

            return OkResponse("Password changed successfully.");
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response Delete(int id)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve the customer by id.
                    CUS01 customer = db.SingleById<CUS01>(id);

                    // Check if the customer exists.
                    if (customer == null)
                        return NotFoundResponse("Customer not found.");

                    // Extract information for USR01.
                    string username = customer.S01F03.Split('@')[0];

                    // Delete the customer and the related USR01 record.
                    db.DeleteById<CUS01>(id);
                    db.Delete<USR01>(u => u.R01F02 == username);
                }
            }
            catch (Exception ex) { throw ex; }

            return OkResponse("Customer deleted successfully.");
        }

        /// <summary>
        /// Retrieves all customers information.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetAll()
        {
            List<CUS01> lstCUS01;

            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                    lstCUS01 = db.Select<CUS01>();
            }
            catch (Exception ex) { throw ex; }

            if (lstCUS01 == null || lstCUS01.Count == 0)
                return NoContentResponse();

            return OkResponse("", lstCUS01);
        }

        /// <summary>
        /// Retrieves a customer by their ID.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        public Response GetById(int id)
        {
            CUS01 objCUS01;

            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                    objCUS01 = db.SingleById<CUS01>(id);
            }
            catch (Exception ex) { throw ex; }

            if (objCUS01 == null)
                return NotFoundResponse("Customer not found.");

            return OkResponse("", objCUS01);
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
                    R01F05 = GetEncryptPassword(_objCUS01.S01F04)
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

                try
                {
                    using (var db = _dbFactory.OpenDbConnection())
                    {
                        if (db.SingleById<CUS01>(objDTOCUS01.S01F01) == null)
                            return NotFoundResponse("Customer not found.");
                    }
                }
                catch (Exception ex) { throw ex; }
            }

            return OkResponse();
        }

        /// <summary>
        /// Saves changes according to the operation.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Save()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (Operation == EnmOperation.A)
                    {
                        db.Insert(_objCUS01);
                        db.Insert(_objUSR01);

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
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Validates customer information.
        /// </summary>
        /// <returns>Success response if no error occurs else response with specific statuscode with message.</returns>
        public Response Validation()
        {
            if (Operation == EnmOperation.A)
            {
                try
                {
                    using (var db = _dbFactory.OpenDbConnection())
                    {
                        // Check if the email already exists in the database
                        if (db.Exists<USR01>(u => u.R01F02 == _objUSR01.R01F02))
                            return PreConditionFailedResponse("Email already exists.");
                    }
                }
                catch (Exception ex) { throw ex; }
            }

            return OkResponse();
        }

        #endregion Public Methods
    }
}