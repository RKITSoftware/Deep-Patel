using ServiceStack.Data;
using System.Web;
using System;
using System.Net.Http;
using OnlineShoppingAPI.Models;
using ServiceStack.OrmLite;
using System.Net;
using System.Linq;

namespace OnlineShoppingAPI.Business_Logic
{
    public class BLAdmin
    {
        private static readonly IDbConnectionFactory _dbFactory;

        static BLAdmin()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        internal static HttpResponseMessage ChangePassword(string username, string newPassword)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var objAdmin = db.Single(db.From<ADM01>().Where(a => a.M01F03.StartsWith(username)));
                var objUser = db.Single(db.From<USR01>().Where(u => u.R01F02.StartsWith(username)));

                objAdmin.M01F04 = newPassword;
                objUser.R01F03 = newPassword;

                db.Update(objAdmin);
                db.Update(objUser);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
        }

        internal static HttpResponseMessage Create(ADM01 objAdmin)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                bool tableExists = db.TableExists<ADM01>();

                db.Insert(objAdmin);
                db.Insert(new USR01
                {
                    R01F02 = objAdmin.M01F03.Split('@')[0],
                    R01F03 = objAdmin.M01F04,
                    R01F04 = "Admin"
                });

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Admin created successfully.")
                };
            }
        }

        internal static HttpResponseMessage Delete(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var lstAdmin = db.Select<ADM01>();

                if (lstAdmin.Count == 1)
                    return new HttpResponseMessage(HttpStatusCode.Forbidden)
                    {
                        Content = new StringContent("Server can't fullfill request because there is only one admin.")
                    };

                string username = lstAdmin.FirstOrDefault(a => a.M01F01 == id).M01F03.Split('@')[0];
                db.DeleteById<ADM01>(id);
                db.DeleteWhere<USR01>("R01F02 = {0}", new object[] { username });

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Admin deleted successfully.")
                };
            }
        }
    }
}