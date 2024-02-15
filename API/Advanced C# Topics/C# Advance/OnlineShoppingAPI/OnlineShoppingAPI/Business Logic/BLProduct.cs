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
    public class BLProduct
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
        static BLProduct()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _logFolderPath = HttpContext.Current.Application["LogFolderPath"] as string;

            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        /// <summary>
        /// Creating a product 
        /// </summary>
        /// <param name="objNewProduct">Product information</param>
        /// <returns>Create response message</returns>
        internal HttpResponseMessage Create(PRO01 objNewProduct)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.Insert(objNewProduct);

                    return new HttpResponseMessage(HttpStatusCode.Created)
                    {
                        Content = new StringContent("Product created successfully.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLException.SendErrorToTxt(ex, _logFolderPath);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while creating the product.")
                };
            }

        }

        /// <summary>
        /// Getting all the products information
        /// </summary>
        /// <returns>List of Products</returns>
        internal List<PRO01> GetAll()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    List<PRO01> products = db.Select<PRO01>();
                    return products ?? new List<PRO01>(); // Return an empty list if products is null
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLException.SendErrorToTxt(ex, _logFolderPath);
                return new List<PRO01>(); // Return an empty list in case of an exception
            }
        }

        /// <summary>
        /// Creating products from the list of products data
        /// </summary>
        /// <param name="lstNewProducts">New products list</param>
        /// <returns>Create response message</returns>
        public HttpResponseMessage CreateFromList(List<PRO01> lstNewProducts)
        {
            try
            {
                if (lstNewProducts.Count == 0)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Data is empty")
                    };
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.InsertAll(lstNewProducts);
                    return new HttpResponseMessage(HttpStatusCode.Created)
                    {
                        Content = new StringContent("Products created successfully.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLException.SendErrorToTxt(ex, _logFolderPath);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while creating the products.")
                };
            }
        }

        /// <summary>
        /// Deleting products from the database using product id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Delete resposne message</returns>
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
                    PRO01 product = db.SingleById<PRO01>(id);
                    if (product == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent($"Product with ID {id} not found.")
                        };
                    }

                    db.DeleteById<PRO01>(id);
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Product deleted successfully.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLException.SendErrorToTxt(ex, _logFolderPath);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while deleting the product.")
                };
            }
        }

        /// <summary>
        /// Updating product information
        /// </summary>
        /// <param name="objUpdatedProduct">Products updated response</param>
        /// <returns>Update response message</returns>
        internal HttpResponseMessage Update(PRO01 objUpdatedProduct)
        {
            try
            {
                if (objUpdatedProduct.O01F01 <= 0)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("Id can't be zero or negative.")
                    };
                }

                using (var db = _dbFactory.OpenDbConnection())
                {
                    PRO01 existingProduct = db.SingleById<PRO01>(objUpdatedProduct.O01F01);

                    if (existingProduct == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent($"Product with ID {objUpdatedProduct.O01F01} not found.")
                        };
                    }

                    // Update product properties
                    existingProduct.O01F02 = objUpdatedProduct.O01F02;
                    existingProduct.O01F03 = objUpdatedProduct.O01F03;
                    existingProduct.O01F04 = objUpdatedProduct.O01F04;
                    existingProduct.O01F05 = objUpdatedProduct.O01F05;

                    // Perform the database update
                    db.Update(existingProduct);

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Product updated successfully.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLException.SendErrorToTxt(ex, _logFolderPath);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while updating the product.")
                };
            }
        }

        internal HttpResponseMessage UpdateQuantity(int productId, int quantity)
        {
            if (productId <= 0)
            {
                return new HttpResponseMessage(HttpStatusCode.PreconditionFailed)
                {
                    Content = new StringContent("Product Id can't be negative nor zero.")
                };
            }

            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    PRO01 objProduct = db.SingleById<PRO01>(productId);

                    if (objProduct == null)
                    {
                        return new HttpResponseMessage(HttpStatusCode.PreconditionFailed)
                        {
                            Content = new StringContent("Product is not avilable.")
                        };
                    }

                    objProduct.O01F04 += quantity;
                    db.Update(objProduct);

                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Product quantity successfully updated.")
                    };
                }
            }
            catch (Exception ex)
            {
                // Log the exception and return an appropriate response
                BLException.SendErrorToTxt(ex, _logFolderPath);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred while updating the product.")
                };
            }
        }
    }
}