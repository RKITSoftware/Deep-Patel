using OnlineShoppingAPI.Interface;
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
    public class BLCustomers : IBasicAPIService<CUS01>
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
        static BLCustomers()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _logFolderPath = HttpContext.Current.Application["LogFolderPath"] as string;

            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        /// <summary>
        /// Create a customer and adding that customer details into the customer and user table.
        /// </summary>
        /// <param name="objNewCustomer">Customer data</param>
        /// <returns>Create customer</returns>
        HttpResponseMessage IBasicAPIService<CUS01>.Create(CUS01 objNewCustomer)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Inserting new customer
                    db.Insert(objNewCustomer);

                    // Extracting information for USR01
                    var emailParts = objNewCustomer.S01F03.Split('@');
                    var username = emailParts[0];
                    var newPassword = objNewCustomer.S01F04;

                    // Inserting related USR01 record
                    db.Insert(new USR01
                    {
                        R01F02 = username,
                        R01F03 = newPassword,
                        R01F04 = "Customer",
                        R01F05 = BLHelper.GetEncryptPassword(newPassword)
                    });

                    return new HttpResponseMessage(HttpStatusCode.Created)
                    {
                        Content = new StringContent("Customer created successfully.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.SendErrorToTxt(ex, _logFolderPath);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while creating the customer.")
                };
            }
        }

        /// <summary>
        /// Deleting customer data
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns>Delete response message</returns>
        HttpResponseMessage IBasicAPIService<CUS01>.Delete(int id)
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
                    // Retrieve customer by id
                    CUS01 customer = db.SingleById<CUS01>(id);

                    // Check if the customer exists
                    if (customer == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent($"Customer with ID {id} not found.")
                        };
                    }

                    // Extracting information for USR01
                    string username = customer.S01F03.Split('@')[0];

                    // Delete customer and related USR01 record
                    db.DeleteById<CUS01>(id);
                    db.Delete<USR01>(u => u.R01F02 == username);

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Customer deleted successfully.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.SendErrorToTxt(ex, _logFolderPath);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while deleting the customer.")
                };
            }

        }

        /// <summary>
        /// Updating customer information
        /// </summary>
        /// <param name="objUpdatedCustomer">Updated information of customer</param>
        /// <returns>Update response message</returns>
        HttpResponseMessage IBasicAPIService<CUS01>.Update(CUS01 objUpdatedCustomer)
        {
            try
            {
                if (objUpdatedCustomer.S01F01 <= 0)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Id can't be zero or negative.")
                    };
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve existing customer by id
                    CUS01 existingCustomer = db.SingleById<CUS01>(objUpdatedCustomer.S01F01);

                    // Check if the customer exists
                    if (existingCustomer == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent($"Customer with ID {objUpdatedCustomer.S01F01} not found.")
                        };
                    }

                    // Update customer properties
                    existingCustomer.S01F02 = objUpdatedCustomer.S01F02;
                    existingCustomer.S01F05 = objUpdatedCustomer.S01F05;
                    existingCustomer.S01F06 = objUpdatedCustomer.S01F06;

                    // Perform the database update
                    db.Update(existingCustomer);

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Customer updated successfully.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.SendErrorToTxt(ex, _logFolderPath);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while updating the customer.")
                };
            }

        }

        /// <summary>
        /// Creating customers from list
        /// </summary>
        /// <param name="lstNewCustomers">List of customer data to added into database.</param>
        /// <returns>Create response message</returns>
        HttpResponseMessage IBasicAPIService<CUS01>.CreateFromList(List<CUS01> lstNewCustomers)
        {
            try
            {
                if (lstNewCustomers.Count == 0)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Data is empty")
                    };
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Insert all new customers
                    db.InsertAll(lstNewCustomers);

                    // Insert related USR01 records for each new customer
                    foreach (CUS01 item in lstNewCustomers)
                    {
                        db.Insert(new USR01
                        {
                            R01F02 = item.S01F03.Split('@')[0],
                            R01F03 = item.S01F04,
                            R01F04 = "Customer",
                            R01F05 = BLHelper.GetEncryptPassword(item.S01F04)
                        });
                    }

                    return new HttpResponseMessage(HttpStatusCode.Created)
                    {
                        Content = new StringContent("Customers created successfully.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.SendErrorToTxt(ex, _logFolderPath);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while creating customers.")
                };
            }

        }

        /// <summary>
        /// Getting all customer details from database
        /// </summary>
        /// <returns>List of Customer data</returns>
        List<CUS01> IBasicAPIService<CUS01>.GetAll()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    List<CUS01> customers = db.Select<CUS01>();
                    return customers ?? new List<CUS01>(); // Return an empty list if customers is null
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.SendErrorToTxt(ex, _logFolderPath);
                return null;
            }
        }

        /// <summary>
        /// Changing the customer password using the Customer username
        /// </summary>
        /// <param name="username">Customer username</param>
        /// <param name="newPassword">Customer new password</param>
        /// <returns>Change response</returns>
        HttpResponseMessage IBasicAPIService<CUS01>.ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    CUS01 existingCustomer = db.SingleWhere<CUS01>("S01F03", username + "@gmail.com");
                    USR01 existingUser = db.SingleWhere<USR01>("R01F02", username);

                    if (existingCustomer == null && existingUser == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }

                    if (existingCustomer.S01F04.Equals(oldPassword))
                    {
                        existingCustomer.S01F04 = newPassword;
                        existingUser.R01F03 = newPassword;
                        existingUser.R01F05 = BLHelper.GetEncryptPassword(newPassword);

                        db.Update(existingCustomer);
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
                BLHelper.SendErrorToTxt(ex, _logFolderPath);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while changing the password.")
                };
            }

        }
    }
}