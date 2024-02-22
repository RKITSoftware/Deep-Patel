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

        #endregion
    }
}
