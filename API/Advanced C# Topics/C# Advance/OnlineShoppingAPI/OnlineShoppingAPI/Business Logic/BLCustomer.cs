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
        private static readonly IDbConnectionFactory _dbFactory;

        static BLCustomers()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        public static HttpResponseMessage Create(CUS01 objNewCustomer)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                bool tableExists = db.TableExists<CUS01>();

                if (!tableExists)
                    db.CreateTable<CUS01>();

                db.Insert<CUS01>(objNewCustomer);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Customer created successfully.")
                };
            }
        }

        public static List<CUS01> GetData()
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

        public static HttpResponseMessage CreateFromList(List<CUS01> lstNewCustomers)
        {
            if (lstNewCustomers.Count == 0)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Data is empty")
                };

            using (var db = _dbFactory.OpenDbConnection())
            {
                bool tableExists = db.TableExists<CUS01>();

                if (!tableExists)
                    db.CreateTable<CUS01>();

                db.InsertAll<CUS01>(lstNewCustomers);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Data is empty")
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
                var customer = db.SingleById<CUS01>(id);

                if (customer == null)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                db.Delete<CUS01>(id);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Customer deleted successfully.")
                };
            }
        }

        public static HttpResponseMessage UpdateData(CUS01 objUpdatedCustomer)
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
                existingCustomer.S01F03 = objUpdatedCustomer.S01F03;
                existingCustomer.S01F04 = objUpdatedCustomer.S01F04;
                existingCustomer.S01F05 = objUpdatedCustomer.S01F05;

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Customer updated successfully.")
                };
            }
        }

        public static bool LogIn(string username, string password)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.Exists<CUS01>(c => c.S01F03.StartsWith(username) && c.S01F05 == password);
            }
        }

        public static CUS01 GetCustomer(string username)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.SingleWhere<CUS01>("S01F03", username + "@gmail.com");
            }
        }
    } 
}