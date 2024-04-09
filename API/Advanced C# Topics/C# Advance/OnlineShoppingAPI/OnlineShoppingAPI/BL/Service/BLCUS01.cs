﻿using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.Extension;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using OnlineShoppingAPI.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using static OnlineShoppingAPI.BL.Common.BLHelper;

namespace OnlineShoppingAPI.BL.Service
{
    /// <summary>
    /// Service implementation of <see cref="ICUS01Service"/>.
    /// </summary>
    public class BLCUS01 : ICUS01Service
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
        /// The operation being performed create or update.
        /// </summary>
        private EnmOperation _operation;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BLCUS01"/>.
        /// </summary>
        public BLCUS01()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Changes the email address of a customer.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="newEmail">The new email address to set.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void ChangeEmail(string username, string password, string newEmail,
            out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (GetUser(newEmail) != null)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.PreconditionFailed,
                            Message = "Use another email, this email is already exists."
                        };
                        return;
                    }

                    // Retrieve admin details.
                    CUS01 objCustomer = db.Single(db.From<CUS01>()
                        .Where(c => c.S01F03.StartsWith(username) &&
                                    c.S01F04.Equals(password)));

                    if (objCustomer == null)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Customer doesn't exist"
                        };
                        return;
                    }

                    USR01 objUser = GetUser(username);

                    // Update email and username
                    objCustomer.S01F03 = newEmail;
                    objUser.R01F02 = newEmail.Split('@')[0];

                    // Update data in the database.
                    db.Update(objCustomer);
                    db.Update(objUser);

                    response = OkResponse();
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        /// <summary>
        /// Changes the password of a customer.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="oldPassword">The old password of the user.</param>
        /// <param name="newPassword">The new password to set.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void ChangePassword(string username, string oldPassword, string newPassword,
            out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve existing customer and user by username.
                    CUS01 existingCustomer = db.SingleWhere<CUS01>("S01F03", username + "@gmail.com");
                    USR01 existingUser = db.SingleWhere<USR01>("R01F02", username);

                    // Check if the customer and user exist.
                    if (existingCustomer == null && existingUser == null)
                    {
                        response = new Response()
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Customer not found."
                        };
                        return;
                    }

                    // Check if the provided old password matches the existing customer password.
                    if (!existingCustomer.S01F04.Equals(oldPassword))
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.PreconditionFailed,
                            Message = "Password doesn't match."
                        };
                        return;
                    }

                    // Update customer and user passwords with the new password.
                    existingCustomer.S01F04 = newPassword;
                    existingUser.R01F03 = newPassword;
                    existingUser.R01F05 = GetEncryptPassword(newPassword);

                    // Perform the database updates.
                    db.Update(existingCustomer);
                    db.Update(existingUser);

                    // Return a success response.
                    response = OkResponse();
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response.
                ex.LogException();
                response = ISEResponse();
            }
        }

        /// <summary>
        /// Prepares the objects for create or update operation.
        /// </summary>
        /// <param name="objCUS01DTO">The DTO object representing the customer.</param>
        /// <param name="operation">The operation type for the save action.</param>
        public void PreSave(DTOCUS01 objCUS01DTO, EnmOperation operation)
        {
            _objCUS01 = objCUS01DTO.Convert<CUS01>();

            if (operation == EnmOperation.Create)
            {
                _objUSR01 = new USR01()
                {
                    R01F02 = _objCUS01.S01F03.Split('@')[0],
                    R01F03 = _objCUS01.S01F04,
                    R01F04 = Roles.Customer,
                    R01F05 = GetEncryptPassword(_objCUS01.S01F04)
                };
            }

            _operation = operation;
        }

        /// <summary>
        /// Validates customer information.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        /// <returns>True if the user information is valid, otherwise false.</returns>
        public bool Validation(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (_operation == EnmOperation.Create)
                    {
                        // Check if the email already exists in the database
                        if (db.Exists<USR01>(u => u.R01F02 == _objUSR01.R01F02))
                        {
                            response = new Response()
                            {
                                IsError = true,
                                StatusCode = HttpStatusCode.PreconditionFailed,
                                Message = "Email already exists."
                            };
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
                return false;
            }

            response = null;
            return true;
        }

        /// <summary>
        /// Saves changes according to the operation.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void Save(out Response response)
        {
            if (_operation == EnmOperation.Create)
                Create(out response);
            else
                Update(out response);
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to be deleted.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void Delete(int id, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve the customer by id.
                    CUS01 customer = db.SingleById<CUS01>(id);

                    // Check if the customer exists.
                    if (customer == null)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Customer not found."
                        };
                        return;
                    }

                    // Extract information for USR01.
                    string username = customer.S01F03.Split('@')[0];

                    // Delete the customer and the related USR01 record.
                    db.DeleteById<CUS01>(id);
                    db.Delete<USR01>(u => u.R01F02 == username);

                    // Return a success response.
                    response = OkResponse();
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        /// <summary>
        /// Retrieves all customers information.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void GetAll(out Response response)
        {
            try
            {
                List<CUS01> lstCUS01;

                using (var db = _dbFactory.OpenDbConnection())
                {
                    lstCUS01 = db.Select<CUS01>();

                    if (lstCUS01 == null || lstCUS01.Count == 0)
                    {
                        response = new Response()
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "There are no customer data found"
                        };
                    }
                    else
                    {
                        response = OkResponse();
                        response.Data = lstCUS01;
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
        /// Retrieves a customer by their ID.
        /// </summary>
        /// <param name="id">The ID of the customer to retrieve.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        public void GetById(int id, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    CUS01 objCUS01 = db.SingleById<CUS01>(id);

                    if (objCUS01 == null)
                    {
                        response = new Response()
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Customer not found."
                        };
                    }
                    else
                    {
                        response = OkResponse();
                        response.Data = objCUS01;
                    }
                }
            }
            catch (Exception exception)
            {
                exception.LogException();
                response = ISEResponse();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates an existing customer in the database.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        private void Update(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve the existing customer by id.
                    CUS01 existingCustomer = db.SingleById<CUS01>(_objCUS01.S01F01);

                    // Check if the customer exists.
                    if (existingCustomer == null)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Customer not found."
                        };
                        return;
                    }

                    // Update customer properties with the provided data.
                    existingCustomer.S01F02 = _objCUS01.S01F02;
                    existingCustomer.S01F05 = _objCUS01.S01F05;
                    existingCustomer.S01F06 = _objCUS01.S01F06;

                    // Perform the database update.
                    db.Update(existingCustomer);
                    response = OkResponse();
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        /// <summary>
        /// Creates a new customer in the database.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        private void Create(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.Insert(_objCUS01);
                    db.Insert(_objUSR01);

                    response = OkResponse();
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