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
        #region Private Fields

        /// <summary>
        /// _dbFactory is used to store the reference of database connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Static constructor is used to initialize _dbfactory for future reference.
        /// </summary>
        /// <exception cref="ApplicationException">If database can't connect then this exception shows.</exception>
        public BLCustomers()
        {
            // Getting data connection from Application state
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            // If database can't be connect.
            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new customer and adds the customer details to the customer and user tables.
        /// </summary>
        /// <param name="objNewCustomer">Customer data.</param>
        /// <returns>
        /// HttpResponseMessage indicating the success or failure of the customer creation process.
        /// </returns>
        HttpResponseMessage IBasicAPIService<CUS01>.Create(CUS01 objNewCustomer)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Insert the new customer into the CUS01 table.
                    db.Insert(objNewCustomer);

                    // Extract information for USR01.
                    var emailParts = objNewCustomer.S01F03.Split('@');
                    var username = emailParts[0];
                    var newPassword = objNewCustomer.S01F04;

                    // Insert the related USR01 record.
                    db.Insert(new USR01
                    {
                        R01F02 = username,
                        R01F03 = newPassword,
                        R01F04 = "Customer",
                        R01F05 = BLHelper.GetEncryptPassword(newPassword)
                    });

                    // Return a success response.
                    return new HttpResponseMessage(HttpStatusCode.Created)
                    {
                        Content = new StringContent("Customer created successfully.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.LogError(ex);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while creating the customer.")
                };
            }
        }

        /// <summary>
        /// Deletes customer data based on the specified customer id.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <returns>
        /// HttpResponseMessage indicating the success or failure of the customer deletion process.
        /// </returns>
        HttpResponseMessage IBasicAPIService<CUS01>.Delete(int id)
        {
            try
            {
                // Check if the provided id is invalid (zero or negative).
                if (id <= 0)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Id can't be zero or negative.")
                    };
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve the customer by id.
                    CUS01 customer = db.SingleById<CUS01>(id);

                    // Check if the customer exists.
                    if (customer == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent($"Customer with ID {id} not found.")
                        };
                    }

                    // Extract information for USR01.
                    string username = customer.S01F03.Split('@')[0];

                    // Delete the customer and the related USR01 record.
                    db.DeleteById<CUS01>(id);
                    db.Delete<USR01>(u => u.R01F02 == username);

                    // Return a success response.
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Customer deleted successfully.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response.
                BLHelper.LogError(ex);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while deleting the customer.")
                };
            }
        }

        /// <summary>
        /// Updates customer information based on the provided updated customer data.
        /// </summary>
        /// <param name="objUpdatedCustomer">Updated information of the customer.</param>
        /// <returns>
        /// HttpResponseMessage indicating the success or failure of the customer update process.
        /// </returns>
        HttpResponseMessage IBasicAPIService<CUS01>.Update(CUS01 objUpdatedCustomer)
        {
            try
            {
                // Check if the provided customer id is invalid (zero or negative).
                if (objUpdatedCustomer.S01F01 <= 0)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Id can't be zero or negative.")
                    };
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve the existing customer by id.
                    CUS01 existingCustomer = db.SingleById<CUS01>(objUpdatedCustomer.S01F01);

                    // Check if the customer exists.
                    if (existingCustomer == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent($"Customer with ID {objUpdatedCustomer.S01F01} not found.")
                        };
                    }

                    // Update customer properties with the provided data.
                    existingCustomer.S01F02 = objUpdatedCustomer.S01F02;
                    existingCustomer.S01F05 = objUpdatedCustomer.S01F05;
                    existingCustomer.S01F06 = objUpdatedCustomer.S01F06;

                    // Perform the database update.
                    db.Update(existingCustomer);

                    // Return a success response.
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Customer updated successfully.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response.
                BLHelper.LogError(ex);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while updating the customer.")
                };
            }
        }

        /// <summary>
        /// Creates customers from a list and adds the customer data to the database.
        /// </summary>
        /// <param name="lstNewCustomers">List of customer data to be added into the database.</param>
        /// <returns>
        /// HttpResponseMessage indicating the success or failure of the customer creation process.
        /// </returns>
        HttpResponseMessage IBasicAPIService<CUS01>.CreateFromList(List<CUS01> lstNewCustomers)
        {
            try
            {
                // Check if the provided list of customers is empty.
                if (lstNewCustomers.Count == 0)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Data is empty.")
                    };
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Insert all new customers into the CUS01 table.
                    db.InsertAll(lstNewCustomers);

                    // Insert related USR01 records for each new customer.
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

                    // Return a success response.
                    return new HttpResponseMessage(HttpStatusCode.Created)
                    {
                        Content = new StringContent("Customers created successfully.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response.
                BLHelper.LogError(ex);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while creating customers.")
                };
            }
        }

        /// <summary>
        /// Retrieves all customer details from the database.
        /// </summary>
        /// <returns>List of customer data.</returns>
        List<CUS01> IBasicAPIService<CUS01>.GetAll()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve all customer details from the CUS01 table.
                    List<CUS01> customers = db.Select<CUS01>();

                    // Return an empty list if 'customers' is null.
                    return customers ?? new List<CUS01>();
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response.
                BLHelper.LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// Changes the customer password using the customer username.
        /// </summary>
        /// <param name="username">Customer username.</param>
        /// <param name="oldPassword">Customer old password.</param>
        /// <param name="newPassword">Customer new password.</param>
        /// <returns>Change response.</returns>
        HttpResponseMessage IBasicAPIService<CUS01>.ChangePassword(string username, string oldPassword, string newPassword)
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
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }

                    // Check if the provided old password matches the existing customer password.
                    if (!existingCustomer.S01F04.Equals(oldPassword))
                    {
                        // Return a precondition failed response if the old password is incorrect.
                        return new HttpResponseMessage(HttpStatusCode.PreconditionFailed)
                        {
                            Content = new StringContent("Password is incorrect.")
                        };
                    }

                    // Update customer and user passwords with the new password.
                    existingCustomer.S01F04 = newPassword;
                    existingUser.R01F03 = newPassword;
                    existingUser.R01F05 = BLHelper.GetEncryptPassword(newPassword);

                    // Perform the database updates.
                    db.Update(existingCustomer);
                    db.Update(existingUser);

                    // Return a success response.
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Password changed successfully.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response.
                BLHelper.LogError(ex);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while changing the password.")
                };
            }
        }

        #endregion
    }
}