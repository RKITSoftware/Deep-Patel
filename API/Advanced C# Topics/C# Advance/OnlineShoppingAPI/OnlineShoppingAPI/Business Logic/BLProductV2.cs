using OnlineShoppingAPI.Enums;
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
    /// Business Logic class for managing product-related operations (version 2).
    /// </summary>
    public class BLProductV2
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
        /// If the database connection cannot be established, this exception is thrown.
        /// </exception>
        public BLProductV2()
        {
            // Getting data connection from Application state
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            // If the database connection cannot be established.
            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        #endregion

        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="objProduct">The product object to be added.</param>
        /// <returns>HttpResponseMessage indicating the result of the operation.</returns>
        public HttpResponseMessage Add(PRO02 objProduct)
        {
            try
            {
                if (objProduct == null)
                    return BLHelper.ResponseMessage(HttpStatusCode.BadRequest,
                        "Product data is null.");

                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (objProduct.O02F03 >= 0 && objProduct.O02F04 >= 0 &&
                        objProduct.O02F05 >= 0)
                    {
                        objProduct.O02F07 = (int)EnmProductStatus.InStock;
                        objProduct.O02F08 = DateTime.Now;

                        db.Insert(objProduct);
                        return BLHelper.ResponseMessage(HttpStatusCode.Created,
                            "Product Added Successfully.");
                    }

                    return BLHelper.ResponseMessage(HttpStatusCode.PreconditionFailed,
                        "Product buy price, sell price, or quantity is negative.");
                }
            }
            catch (Exception ex)
            {
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred during creating product.");
            }
        }

        /// <summary>
        /// Retrieves a list of all products from the database.
        /// </summary>
        /// <returns>List of PRO02 representing all products.</returns>
        public List<PRO02> GetAll()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    List<PRO02> products = db.Select<PRO02>();
                    return products ?? new List<PRO02>(); // Return an empty list if products is null
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLHelper.LogError(ex);
                return new List<PRO02>(); // Return an empty list in case of an exception
            }
        }

        /// <summary>
        /// Deletes a product from the database based on the given product ID.
        /// </summary>
        /// <param name="productId">The ID of the product to be deleted.</param>
        /// <returns>HttpResponseMessage indicating the result of the operation.</returns>
        public HttpResponseMessage Delete(int productId)
        {
            try
            {
                if (productId <= 0)
                {
                    return BLHelper.ResponseMessage(HttpStatusCode.BadRequest,
                        "Product id can't be negative nor zero.");
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    PRO02 existingProduct = db.SingleById<PRO02>(productId);

                    if (existingProduct == null)
                    {
                        return BLHelper.ResponseMessage(HttpStatusCode.NotFound,
                            "Product can't be found.");
                    }

                    db.DeleteById<PRO02>(productId);
                    return BLHelper.ResponseMessage(HttpStatusCode.OK,
                        "Product deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred during deleting product.");
            }
        }

        /// <summary>
        /// Updates the sell price of a product in the database.
        /// </summary>
        /// <param name="productId">The ID of the product to be updated.</param>
        /// <param name="sellPrice">The new sell price for the product.</param>
        /// <returns>HttpResponseMessage indicating the result of the operation.</returns>
        public HttpResponseMessage UpdateSellPrice(int productId, int sellPrice)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    PRO02 existingProduct = db.SingleById<PRO02>(productId);

                    if (existingProduct == null)
                        return BLHelper.ResponseMessage(HttpStatusCode.NotFound,
                            "Product can't be found.");

                    existingProduct.O02F04 = sellPrice;
                    db.Update(existingProduct);

                    return BLHelper.ResponseMessage(HttpStatusCode.OK,
                        "Product updated successfully.");
                }
            }
            catch (Exception ex)
            {
                BLHelper.LogError(ex);
                return BLHelper.ResponseMessage(HttpStatusCode.InternalServerError,
                    "An error occurred during updating product.");
            }
        }
    }
}
