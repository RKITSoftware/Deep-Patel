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

                db.Insert(objNewCustomer);
                db.Insert(new USR01
                {
                    R01F02 = objNewCustomer.S01F03.Split('@')[0],
                    R01F03 = objNewCustomer.S01F04,
                    R01F04 = "Customer"
                });

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Customer created successfully.")
                };
            }
        }

        public static List<CUS01> GetAll()
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
                db.InsertAll(lstNewCustomers);
                foreach(var item in lstNewCustomers)
                {
                    db.Insert(new USR01
                    {
                        R01F02 = item.S01F03.Split('@')[0],
                        R01F03 = item.S01F04,
                        R01F04 = "Customer"
                    });
                }

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Customers Created successfully.")
                };
            }
        }

        public static HttpResponseMessage Delete(int id)
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

        public static HttpResponseMessage Update(CUS01 objUpdatedCustomer)
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