using OnlineShoppingAPI.BL.Interface;
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
    public class BLSUP01 : ISUP01Service
    {
        #region Private Fields

        /// <summary>
        /// Orm Lite Connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Operation to perform.
        /// </summary>
        private EnmOperation _operation;

        /// <summary>
        /// Instance of <see cref="SUP01"/>.
        /// </summary>
        private SUP01 _objSUP01;

        /// <summary>
        /// Instance of <see cref="usr01"/>.
        /// </summary>
        private USR01 _objUSR01;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize the <see cref="BLSUP01"/> insatnces.
        /// </summary>
        public BLSUP01()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Changes the email address of a suplier.
        /// </summary>
        /// <param name="username">The username of the suplier.</param>
        /// <param name="password">The password of the suplier.</param>
        /// <param name="newEmail">The new email address.</param>
        /// <param name="response">The response containing the result of the operation.</param>
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
                            Message = "New email is already exist, choose other email."
                        };
                        return;
                    }

                    // Retrieve admin details.
                    SUP01 objSuplier = db.Single(db.From<SUP01>()
                        .Where(s => s.P01F03.StartsWith(username) &&
                                    s.P01F04.Equals(password)));

                    // If suplier doesn't exist, return Not Found status code.
                    if (objSuplier == null)
                    {
                        response = new Response()
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Suplier not found."
                        };
                        return;
                    }

                    USR01 objUser = GetUser(username);

                    // Update email and username
                    objSuplier.P01F03 = newEmail;
                    objUser.R01F02 = newEmail.Split('@')[0];

                    // Update data in the database.
                    db.Update(objSuplier);
                    db.Update(objUser);

                    // Return success response
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
        /// Changes the password of a suplier.
        /// </summary>
        /// <param name="username">The username of the suplier.</param>
        /// <param name="oldPassword">The old password of the suplier.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="response">The response containing the result of the operation.</param>
        public void ChangePassword(string username, string oldPassword, string newPassword,
            out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Check if the user is a supplier or a regular user
                    SUP01 existingSupplier = db.SingleWhere<SUP01>("P01F03", username + "@gmail.com");
                    USR01 existingUser = db.SingleWhere<USR01>("R01F02", username);

                    if (existingSupplier == null || existingUser == null)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Suplier not found."
                        };
                        return;
                    }

                    // Verify the old password and update if correct
                    if (existingSupplier.P01F04 == oldPassword)
                    {
                        existingSupplier.P01F04 = newPassword;
                        existingUser.R01F03 = newPassword;
                        existingUser.R01F05 = GetEncryptPassword(newPassword);

                        // Update supplier and user records
                        db.Update(existingSupplier);
                        db.Update(existingUser);

                        response = OkResponse();
                    }
                    else
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = "Password is incorrect."
                        };
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
        /// Prepares a suplier for saving based on the operation.
        /// </summary>
        /// <param name="objSUP01DTO">The DTO object representing the suplier.</param>
        /// <param name="operation">The type of operation (e.g., add, update).</param>
        public void PreSave(DTOSUP01 objSUP01DTO, EnmOperation operation)
        {
            _operation = operation;
            _objSUP01 = objSUP01DTO.Convert<SUP01>();

            if (operation == EnmOperation.Create)
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
        /// Saves the changes made to a supplier.
        /// </summary>
        /// <param name="response">The response containing the result of the operation.</param>
        public void Save(out Response response)
        {
            if (_operation == EnmOperation.Create)
                Create(out response);
            else
                Update(out response);
        }

        /// <summary>
        /// Performs validation on the supplier data.
        /// </summary>
        /// <param name="response">The response containing the result of the operation.</param>
        /// <returns>True if validation succeeds, otherwise false.</returns>
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
        /// Deletes a suplier.
        /// </summary>
        /// <param name="id">The ID of the suplier to delete.</param>
        /// <param name="response">The response containing the result of the operation.</param>
        public void Delete(int id, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve supplier information by id
                    SUP01 supplier = db.SingleById<SUP01>(id);

                    // Check if the supplier exists
                    if (supplier == null)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Suplier not found."
                        };
                        return;
                    }

                    // Extract username from email
                    string username = supplier.P01F03.Split('@')[0];

                    // Delete supplier and associated user account
                    db.DeleteById<SUP01>(id);
                    db.DeleteWhere<USR01>("R01F02 = {0}", new object[] { username });

                    ServerCache.Remove("lstSuplier");

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
        /// Retrieves all suplier.
        /// </summary>
        /// <param name="response">The response containing the result of the operation.</param>
        public void GetAll(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    List<SUP01> lstSUP01 = db.Select<SUP01>();

                    if (lstSUP01 == null || lstSUP01.Count == 0)
                    {
                        response = new Response()
                        {
                            StatusCode = HttpStatusCode.NoContent,
                            Message = "No suplier data available."
                        };
                    }
                    else
                    {
                        response = OkResponse();
                        response.Data = lstSUP01;
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
        /// Retrieves a suplier by its ID.
        /// </summary>
        /// <param name="id">The ID of the suplier to retrieve.</param>
        /// <param name="response">The response containing the result of the operation.</param>
        public void GetById(int id, out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    SUP01 objSUP01 = db.SingleById<SUP01>(id);

                    if (objSUP01 == null)
                    {
                        response = new Response()
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "Supplier not found."
                        };
                    }
                    else
                    {
                        response = OkResponse();
                        response.Data = objSUP01;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the existing suplier information.
        /// </summary>
        /// <param name="response">The response containing the result of the operation.</param>
        private void Update(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    SUP01 existingSupplier = db.SingleById<SUP01>(_objSUP01.P01F01);

                    if (existingSupplier == null)
                    {
                        response = new Response()
                        {
                            IsError = true,
                            StatusCode = HttpStatusCode.PreconditionFailed,
                            Message = "Suplier doesn't exist"
                        };
                    }
                    else
                    {

                        existingSupplier.P01F02 = _objSUP01.P01F02;
                        existingSupplier.P01F05 = _objSUP01.P01F05;
                        existingSupplier.P01F06 = _objSUP01.P01F06;

                        db.Update(existingSupplier);

                        response = OkResponse();
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
        /// Creates a new suplier.
        /// </summary>
        /// <param name="response">The response containing the result of the operation.</param>
        private void Create(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.Insert(_objSUP01);
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