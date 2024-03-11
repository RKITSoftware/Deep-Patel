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
    /// <summary>
    /// Business Logic class for handling operations related to categories.
    /// </summary>
    public class BLCategory
    {
        #region Private Fields

        /// <summary>
        /// _dbFactory is used to store the reference of the database connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Static constructor is used to initialize _dbfactory for future reference.
        /// </summary>
        /// <exception cref="ApplicationException">
        /// If the database can't connect, this exception is thrown.
        /// </exception>
        public BLCategory()
        {
            // Getting data connection from Application state
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            // If the database connection is not found.
            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a new category to the database.
        /// </summary>
        /// <param name="objCategory">The category object to be added.</param>
        /// <returns>HttpResponseMessage indicating success or failure.</returns>
        public HttpResponseMessage Add(CAT01 objCategory)
        {
            try
            {
                if (objCategory == null)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.BadRequest,
                        "Given data is null.");
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Uncomment the line below if table creation is required.
                    // db.CreateTable<CAT01>(true);
                    db.Insert<CAT01>(objCategory);

                    return BLHelper.ResponseMessage(HttpStatusCode.Created,
                        "Category is added successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes.
                BLHelper.LogError(ex);

                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred during the category addition request.");
            }
        }

        /// <summary>
        /// Retrieves all categories from the database.
        /// </summary>
        /// <returns>A list of categories or null if an error occurs.</returns>
        public List<CAT01> GetAll()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    List<CAT01> lstCategory = db.Select<CAT01>();
                    return lstCategory;
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes.
                BLHelper.LogError(ex);

                return null;
            }
        }

        /// <summary>
        /// Deleted the category from the database using the category id which is given when creating that category.
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>
        /// HttpResponseMessage which contains the specific information about what happens
        /// when deletig category.
        /// </returns>
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                // Check if the id is valid
                if (id <= 0)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.BadRequest,
                        "Id can't be zero or negative.");
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    // Retrieve category information by id
                    CAT01 category = db.SingleById<CAT01>(id);

                    // Check if the category exists
                    if (category == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }

                    db.Delete(category);

                    // Return success response
                    return BLHelper.ResponseMessage(HttpStatusCode.OK,
                        "Category deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response in case of an error
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Return the category which is specified by id in database tables.
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns>Category</returns>
        public CAT01 Get(int id)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    CAT01 objCategory = db.Single<CAT01>(c => c.T01F01 == id);
                    return objCategory;
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes.
                BLHelper.LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// Edit the category that user wants to update.
        /// </summary>
        /// <param name="objCategory">Updated information of category</param>
        /// <returns>HttpResponseMessage </returns>
        public HttpResponseMessage Edit(CAT01 objCategory)
        {
            try
            {
                if (objCategory.T01F01 <= 0)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.BadRequest,
                        "Id can't be zero or negative.");
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    CAT01 existingCategory = db.SingleById<CAT01>(objCategory.T01F01);

                    if (existingCategory == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound);
                    }

                    existingCategory.T01F02 = objCategory.T01F02;

                    db.Update(existingCategory);

                    return BLHelper.ResponseMessage(HttpStatusCode.OK,
                        "Category updated successfully.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred while processing the request.");
            }
        }

        #endregion
    }
}
