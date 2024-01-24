using OnlineShoppingAPI.Models;
using ServiceStack.Data;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web;
using System;
using ServiceStack.OrmLite;

namespace OnlineShoppingAPI.Business_Logic
{
    public class BLProduct
    {
        private static readonly IDbConnectionFactory _dbFactory;

        static BLProduct()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        public static HttpResponseMessage Create(PRO01 objNewProduct)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                bool tableExists = db.TableExists<PRO01>();

                if (!tableExists)
                    db.CreateTable<PRO01>();

                db.Insert<PRO01>(objNewProduct);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Product created successfully.")
                };
            }
        }

        public static List<PRO01> GetData()
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

        public static HttpResponseMessage CreateFromList(List<PRO01> lstNewProducts)
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

                db.InsertAll<PRO01>(lstNewProducts);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Products created successfully.")
                };
            }
        }

        public static HttpResponseMessage DeleteData(int id)
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

                db.Delete<PRO01>(id);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Product deleted successfully.")
                };
            }
        }

        public static HttpResponseMessage UpdateData(PRO01 objUpdatedProduct)
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

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Product updated successfully.")
                };
            }
        }
    }
}