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

        /// <summary>
        /// Static constructor is used to initialize _dbfactory for future reference.
        /// </summary>
        /// <exception cref="ApplicationException">If database can't connect then this exception shows.</exception>
        static BLProduct()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

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
            using (var db = _dbFactory.OpenDbConnection())
            {
                bool tableExists = db.TableExists<PRO01>();

                if (!tableExists)
                    db.CreateTable<PRO01>();

                db.Insert(objNewProduct);
                return new HttpResponseMessage(HttpStatusCode.Created)
                {
                    Content = new StringContent("Product created successfully.")
                };
            }
        }

        /// <summary>
        /// Getting all the products information
        /// </summary>
        /// <returns>List of Products</returns>
        internal List<PRO01> GetAll()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                bool tableExists = db.TableExists<PRO01>();

                if (!tableExists)
                    return null;

                var products = db.Select<PRO01>();
                return products;
            }
        }

        /// <summary>
        /// Creating products from the list of products data
        /// </summary>
        /// <param name="lstNewProducts">New products list</param>
        /// <returns>Create response message</returns>
        public HttpResponseMessage CreateFromList(List<PRO01> lstNewProducts)
        {
            if (lstNewProducts.Count == 0)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Data is empty")
                };

            using (var db = _dbFactory.OpenDbConnection())
            {
                bool tableExists = db.TableExists<PRO01>();

                if (!tableExists)
                    db.CreateTable<PRO01>();

                db.InsertAll(lstNewProducts);
                return new HttpResponseMessage(HttpStatusCode.Created)
                {
                    Content = new StringContent("Products created successfully.")
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
            if (id <= 0)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Id can't be zero or negative.")
                };

            using (var db = _dbFactory.OpenDbConnection())
            {
                var product = db.SingleById<PRO01>(id);

                if (product == null)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                db.DeleteById<PRO01>(id);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Product deleted successfully.")
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
            if (objUpdatedProduct.O01F01 <= 0)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Id can't be zero or negative.")
                };

            using (var db = _dbFactory.OpenDbConnection())
            {
                PRO01 existingProduct = db.SingleById<PRO01>(objUpdatedProduct.O01F01);

                if (existingProduct == null)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                existingProduct.O01F02 = objUpdatedProduct.O01F02;
                existingProduct.O01F03 = objUpdatedProduct.O01F03;
                existingProduct.O01F04 = objUpdatedProduct.O01F04;
                existingProduct.O01F05 = objUpdatedProduct.O01F05;

                db.Update(existingProduct);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Product updated successfully.")
                };
            }
        }
    }
}