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
    public class BLCustomers
    {
        /// <summary>
        /// _dbFactory is used to store the reference of database connection.
        /// </summary>
        private static readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Static constructor is used to initialize _dbfactory for future reference.
        /// </summary>
        /// <exception cref="ApplicationException">If database can't connect then this exception shows.</exception>
        static BLCustomers()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        /// <summary>
        /// Changing the customer password using the Customer username
        /// </summary>
        /// <param name="username">Customer username</param>
        /// <param name="newPassword">Customer new password</param>
        /// <returns>Change response</returns>
        internal static HttpResponseMessage ChangePassword(string username, string newPassword)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                CUS01 existingCustomer = db.SingleWhere<CUS01>("S01F03", username + "@gmail.com");
                USR01 existingUser = db.SingleWhere<USR01>("R01F02", username);

                if (existingCustomer == null && existingCustomer == null)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                existingCustomer.S01F04 = newPassword;
                existingUser.R01F03 = newPassword;

                db.Update(existingCustomer);
                db.Update(existingUser);

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Password changed successfully.")
                };
            }
        }

        /// <summary>
        /// Create a customer and adding that customer details into the customer and user table.
        /// </summary>
        /// <param name="objNewCustomer">Customer data</param>
        /// <returns>Create response message</returns>
        internal static HttpResponseMessage Create(CUS01 objNewCustomer)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                bool tableExists = db.TableExists<CUS01>();

                if (!tableExists)
                    db.CreateTable<CUS01>();

                db.Insert(objNewCustomer);
                db.Insert(new USR01
                {
                    R01F02 = objNewCustomer.S01F03.Split('@')[0],
                    R01F03 = objNewCustomer.S01F04,
                    R01F04 = "Customer"
                });

                return new HttpResponseMessage(HttpStatusCode.Created)
                {
                    Content = new StringContent("Customer created successfully.")
                };
            }
        }

        /// <summary>
        /// Getting all customer details from database
        /// </summary>
        /// <returns>List of Customer data</returns>
        internal static List<CUS01> GetAll()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                bool tableExists = db.TableExists<CUS01>();

                if (!tableExists)
                    return null;

                var customers = db.Select<CUS01>();
                return customers;
            }
        }

        /// <summary>
        /// Creating customers from list
        /// </summary>
        /// <param name="lstNewCustomers">List of customer data to added into database.</param>
        /// <returns>Create response message</returns>
        internal static HttpResponseMessage CreateFromList(List<CUS01> lstNewCustomers)
        {
            if (lstNewCustomers.Count == 0)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Data is empty")
                };

            using (var db = _dbFactory.OpenDbConnection())
            {
                db.InsertAll(lstNewCustomers);
                foreach (var item in lstNewCustomers)
                {
                    db.Insert(new USR01
                    {
                        R01F02 = item.S01F03.Split('@')[0],
                        R01F03 = item.S01F04,
                        R01F04 = "Customer"
                    });
                }

                return new HttpResponseMessage(HttpStatusCode.Created)
                {
                    Content = new StringContent("Customers Created successfully.")
                };
            }
        }

        /// <summary>
        /// Deleting customer data
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns>Delete response message</returns>
        internal static HttpResponseMessage Delete(int id)
        {
            if (id <= 0)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Id can't be zero or negative.")
                };

            using (var db = _dbFactory.OpenDbConnection())
            {
                var customer = db.SingleById<CUS01>(id);

                if (customer == null)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                string username = customer.S01F03.Split('@')[0];
                db.DeleteById<CUS01>(id);
                db.DeleteWhere<USR01>("R01F02 = {0}", new object[] { username });

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Customer deleted successfully.")
                };
            }
        }

        /// <summary>
        /// Updating customer information
        /// </summary>
        /// <param name="objUpdatedCustomer">Updated information of customer</param>
        /// <returns>Update response message</returns>
        internal static HttpResponseMessage Update(CUS01 objUpdatedCustomer)
        {
            if (objUpdatedCustomer.S01F01 <= 0)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Id can't be zero or negative.")
                };

            using (var db = _dbFactory.OpenDbConnection())
            {
                CUS01 existingCustomer = db.SingleById<CUS01>(objUpdatedCustomer.S01F01);

                if (existingCustomer == null)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                existingCustomer.S01F02 = objUpdatedCustomer.S01F02;
                existingCustomer.S01F05 = objUpdatedCustomer.S01F05;
                existingCustomer.S01F06 = objUpdatedCustomer.S01F06;

                db.Update(existingCustomer);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Customer updated successfully.")
                };
            }
        }
    }
}