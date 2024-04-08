using OnlineShoppingAPI.BL.Interface;
using OnlineShoppingAPI.Business_Logic;
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

namespace OnlineShoppingAPI.BL.Service
{
    public class BLSUP01 : ISUP01Service
    {
        /// <summary>
        /// Orm Lite Connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;
        private EnmOperation _operation;
        private SUP01 _objSUP01;
        private USR01 _objUSR01;

        public BLSUP01()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
        }

        public void ChangeEmail(string username, string password, string newEmail,
            out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (BLHelper.GetUser(newEmail) != null)
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

                    USR01 objUser = BLHelper.GetUser(username);

                    // Update email and username
                    objSuplier.P01F03 = newEmail;
                    objUser.R01F02 = newEmail.Split('@')[0];

                    // Update data in the database.
                    db.Update(objSuplier);
                    db.Update(objUser);

                    // Return success response
                    response = BLHelper.OkResponse();
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
            }
        }

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
                        existingUser.R01F05 = BLHelper.GetEncryptPassword(newPassword);

                        // Update supplier and user records
                        db.Update(existingSupplier);
                        db.Update(existingUser);

                        response = BLHelper.OkResponse();
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
                response = BLHelper.ISEResponse();
            }
        }

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
                    R01F05 = BLHelper.GetEncryptPassword(_objSUP01.P01F04)
                };
            }
        }

        public void Save(out Response response)
        {
            if (_operation == EnmOperation.Create)
                Create(out response);
            else
                Update(out response);
        }

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
                response = BLHelper.ISEResponse();
                return false;
            }

            response = null;
            return true;
        }

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

                    BLHelper.ServerCache.Remove("lstSuplier");

                    response = BLHelper.OkResponse();
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
            }
        }

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
                        response = BLHelper.OkResponse();
                        response.Data = lstSUP01;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
            }
        }

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
                        response = BLHelper.OkResponse();
                        response.Data = objSUP01;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
            }
        }

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

                        response = BLHelper.OkResponse();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
            }
        }

        private void Create(out Response response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.Insert(_objSUP01);
                    db.Insert(_objUSR01);

                    response = BLHelper.OkResponse();
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