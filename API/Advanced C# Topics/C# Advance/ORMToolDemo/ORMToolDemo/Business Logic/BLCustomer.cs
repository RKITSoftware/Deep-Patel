using ORMToolDemo.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace ORMToolDemo.Business_Logic
{
    /// <summary>
    /// Business Logic class for performing CRUD operations on Customer entities.
    /// </summary>
    public class BLCustomer
    {
        /// <summary>
        /// Database connection interface for CRUD operation
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Constructs a new instance of the BLCustomer class.
        /// </summary>
        /// <exception cref="ApplicationException">Thrown if connection with the database is not established.</exception>
        public BLCustomer()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        /// <summary>
        /// Retrieves all customers from the database.
        /// </summary>
        /// <returns>A list of all customers.</returns>
        public List<Customer> GetAll()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                List<Customer> customers = db.Select<Customer>();
                return customers;
            }
        }

        /// <summary>
        /// Retrieves a customer by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>The customer with the specified identifier.</returns>
        public Customer GetById(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                Customer customer = db.SingleById<Customer>(id);
                return customer;
            }
        }

        /// <summary>
        /// Adds a new customer to the database.
        /// </summary>
        /// <param name="objCustomer">The customer object to be added.</param>
        /// <returns>A message indicating the result of the operation.</returns>
        public string Add(Customer objCustomer)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.Insert(objCustomer);
                return "Added Successfully";
            }
        }

        /// <summary>
        /// Deletes a customer from the database by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to be deleted.</param>
        /// <returns>A message indicating the result of the operation.</returns>
        public string Delete(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<Customer>(id);
                return "Deleted Successfully";
            }
        }

        /// <summary>
        /// Updates an existing customer in the database.
        /// </summary>
        /// <param name="objUpdatedCustomer">The updated customer object.</param>
        /// <returns>A message indicating the result of the operation.</returns>
        public string Update(Customer objUpdatedCustomer)
        {
            if (objUpdatedCustomer == null)
            {
                return "Invalid customer data.";
            }

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                Customer objExistingCustomer = db.SingleById<Customer>(objUpdatedCustomer.Id);

                if (objExistingCustomer == null)
                    return "Customer not found";

                objExistingCustomer.FirstName = objUpdatedCustomer.FirstName;
                objExistingCustomer.LastName = objUpdatedCustomer.LastName;

                db.Update(objExistingCustomer);

                return "Customer updated successfully";
            }
        }
    }
}
