using MySql.Data.MySqlClient;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.Enum;
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
                        BLHelper.ServerCache.Remove("lstProductsV2");

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
                List<PRO02> lstProductsV2 = BLHelper.ServerCache.Get("lstProductsV2") as List<PRO02>;

                if (lstProductsV2 != null)
                    return lstProductsV2;

                using (var db = _dbFactory.OpenDbConnection())
                {
                    lstProductsV2 = db.Select<PRO02>();
                    return lstProductsV2; // Return an empty list if products is null
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
                    BLHelper.ServerCache.Remove("lstProductsV2");

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
                    BLHelper.ServerCache.Remove("lstProductsV2");

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

        /// <summary>
        /// An Sql query that returns the product information with specific category name and suplier details.
        /// </summary>
        /// <returns>Product information</returns>
        public dynamic GetInfo()
        {
            try
            {
                dynamic lstProducts = new List<dynamic>();

                // Creating a MySqlConnection to connect to the database
                using (MySqlConnection _connection = new MySqlConnection(
                    HttpContext.Current.Application["MySQLConnection"] as string))
                {

                    // Using MySqlCommand to execute SQL command
                    using (MySqlCommand cmd = new MySqlCommand(@"SELECT 
	                                                                pro02.O02F01 AS 'Id',
                                                                    pro02.O02F02 AS 'Name',
                                                                    pro02.O02F03 AS 'Buy Price',
                                                                    pro02.O02F04 AS 'Sell Price',
                                                                    pro02.O02F05 AS 'Quantity',
                                                                    pro02.O02F06 AS 'Image Link',
                                                                    cat01.T01F02 AS 'Category Name',
                                                                    sup01.P01F02 AS 'Suplier Name'
                                                                FROM
                                                                    pro02
                                                                        INNER JOIN
                                                                    cat01 ON pro02.O02F09 = cat01.T01F01
                                                                        INNER JOIN
                                                                    sup01 ON pro02.O02F10 = sup01.P01F01;",
                                                                    _connection))
                    {
                        _connection.Open();

                        // Using MySqlDataReader to read data from the executed command
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Mapping the data from reader to PRO02 object and adding it to the list
                                lstProducts.Add(new
                                {
                                    Id = (int)reader[0],
                                    Name = (string)reader[1],
                                    BuyPrice = (int)reader[2],
                                    SellPrice = (int)reader[3],
                                    Quantity = (decimal)reader[4],
                                    ImageLink = (string)reader[5],
                                    CategoryName = (string)reader[6],
                                    SuplierName = (string)reader[7]
                                });
                            }
                        }
                    }
                }

                return lstProducts;
            }
            catch (Exception ex)
            {
                BLHelper.LogError(ex);
                return null;
            }
        }
    }
}
