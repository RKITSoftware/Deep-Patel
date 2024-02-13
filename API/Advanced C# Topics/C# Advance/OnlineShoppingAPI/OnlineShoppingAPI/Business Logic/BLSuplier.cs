using OnlineShoppingAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;

namespace OnlineShoppingAPI.Business_Logic
{
    public class BLSuplier
    {
        /// <summary>
        /// _dbFactory is used to store the reference of database connection.
        /// </summary>
        private static readonly IDbConnectionFactory _dbFactory;
        private static string _logFolderPath;

        /// <summary>
        /// Static constructor is used to initialize _dbfactory for future reference.
        /// </summary>
        /// <exception cref="ApplicationException">If database can't connect then this exception shows.</exception>
        static BLSuplier()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _logFolderPath = HttpContext.Current.Application["LogFolderPath"] as string;

            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        internal HttpResponseMessage ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    SUP01 existingSupplier = db.SingleWhere<SUP01>("P01F03", username + "@gmail.com");
                    USR01 existingUser = db.SingleWhere<USR01>("R01F02", username);

                    if (existingSupplier == null || existingUser == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }

                    if (existingSupplier.P01F04 == oldPassword)
                    {
                        existingSupplier.P01F04 = newPassword;
                        existingUser.R01F03 = newPassword;
                        existingUser.R01F05 = BLUser.GetEncryptPassword(newPassword);

                        db.Update(existingSupplier);
                        db.Update(existingUser);

                        return new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StringContent("Password changed successfully.")
                        };
                    }
                    else
                    {
                        return new HttpResponseMessage(HttpStatusCode.PreconditionFailed)
                        {
                            Content = new StringContent("Password is incorrect.")
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLException.SendErrorToTxt(ex, _logFolderPath);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while processing the request.")
                };
            }

        }

        internal HttpResponseMessage Create(SUP01 objNewSuplier)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.Insert(objNewSuplier);

                    db.Insert(new USR01
                    {
                        R01F02 = objNewSuplier.P01F03.Split('@')[0],
                        R01F03 = objNewSuplier.P01F04,
                        R01F04 = "Supplier",
                        R01F05 = BLUser.GetEncryptPassword(objNewSuplier.P01F04)
                    });

                    return new HttpResponseMessage(HttpStatusCode.Created)
                    {
                        Content = new StringContent("Supplier created successfully.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLException.SendErrorToTxt(ex, _logFolderPath);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while processing the request.")
                };
            }

        }

        internal HttpResponseMessage CreateFromList(List<SUP01> lstNewSupliers)
        {
            try
            {
                if (lstNewSupliers.Count == 0)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Data is empty")
                    };
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.InsertAll(lstNewSupliers);

                    foreach (SUP01 item in lstNewSupliers)
                    {
                        db.Insert(new USR01
                        {
                            R01F02 = item.P01F03.Split('@')[0],
                            R01F03 = item.P01F04,
                            R01F04 = "Supplier",
                            R01F05 = BLUser.GetEncryptPassword(item.P01F04)
                        });
                    }

                    return new HttpResponseMessage(HttpStatusCode.Created)
                    {
                        Content = new StringContent("Suppliers created successfully.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLException.SendErrorToTxt(ex, _logFolderPath);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while processing the request.")
                };
            }
        }

        internal HttpResponseMessage Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Id can't be zero or negative.")
                    };
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    SUP01 supplier = db.SingleById<SUP01>(id);

                    if (supplier == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }

                    string username = supplier.P01F03.Split('@')[0];
                    db.DeleteById<SUP01>(id);
                    db.DeleteWhere<USR01>("R01F02 = {0}", new object[] { username });

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Supplier deleted successfully.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLException.SendErrorToTxt(ex, _logFolderPath);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while processing the request.")
                };
            }

        }

        internal object GetAll()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    List<SUP01> suppliers = db.Select<SUP01>();
                    return suppliers ?? new List<SUP01>(); // Return an empty list if suppliers is null
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLException.SendErrorToTxt(ex, _logFolderPath);
                return null;
            }
        }

        internal HttpResponseMessage Update(SUP01 objUpdatedSuplier)
        {
            try
            {
                if (objUpdatedSuplier.P01F01 <= 0)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Id can't be zero or negative.")
                    };
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    SUP01 existingSuplier = db.SingleById<SUP01>(objUpdatedSuplier.P01F01);

                    if (existingSuplier == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }

                    existingSuplier.P01F02 = objUpdatedSuplier.P01F02;
                    existingSuplier.P01F05 = objUpdatedSuplier.P01F05;
                    existingSuplier.P01F06 = objUpdatedSuplier.P01F06;

                    db.Update(existingSuplier);

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Supplier updated successfully.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLException.SendErrorToTxt(ex, _logFolderPath);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while processing the request.")
                };
            }

        }
    }
}