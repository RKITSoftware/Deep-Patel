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
    public class BLSuplier
    {
        /// <summary>
        /// _dbFactory is used to store the reference of database connection.
        /// </summary>
        private static readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Static constructor is used to initialize _dbfactory for future reference.
        /// </summary>
        /// <exception cref="ApplicationException">If database can't connect then this exception shows.</exception>
        static BLSuplier()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        internal HttpResponseMessage ChangePassword(string username, string oldPassword, string newPassword)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                SUP01 existingSuplier = db.SingleWhere<SUP01>("P01F03", username + "@gmail.com");
                USR01 existingUser = db.SingleWhere<USR01>("R01F02", username);

                if (existingSuplier == null && existingUser == null)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                if (existingSuplier.P01F04 == oldPassword)
                {
                    existingSuplier.P01F04 = newPassword;
                    existingUser.R01F03 = newPassword;
                    existingUser.R01F05 = BLUser.GetEncryptPassword(newPassword);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.PreconditionFailed)
                    {
                        Content = new StringContent("Password is incorrect.")
                    };
                }

                db.Update(existingSuplier);
                db.Update(existingUser);

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Password changed successfully.")
                };
            }
        }

        internal HttpResponseMessage Create(SUP01 objNewSuplier)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                db.Insert(objNewSuplier);
                db.Insert(new USR01
                {
                    R01F02 = objNewSuplier.P01F03.Split('@')[0],
                    R01F03 = objNewSuplier.P01F04,
                    R01F04 = "Suplier",
                    R01F05 = BLUser.GetEncryptPassword(objNewSuplier.P01F04)
                });

                return new HttpResponseMessage(HttpStatusCode.Created)
                {
                    Content = new StringContent("Suplier created successfully.")
                };
            }
        }

        internal HttpResponseMessage CreateFromList(List<SUP01> lstNewSupliers)
        {
            if (lstNewSupliers.Count == 0)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Data is empty")
                };

            using (var db = _dbFactory.OpenDbConnection())
            {
                db.InsertAll(lstNewSupliers);
                foreach (var item in lstNewSupliers)
                {
                    db.Insert(new USR01
                    {
                        R01F02 = item.P01F03.Split('@')[0],
                        R01F03 = item.P01F04,
                        R01F04 = "Suplier",
                        R01F05 = BLUser.GetEncryptPassword(item.P01F04)
                    });
                }

                return new HttpResponseMessage(HttpStatusCode.Created)
                {
                    Content = new StringContent("Supliers Created successfully.")
                };
            }
        }

        internal HttpResponseMessage Delete(int id)
        {
            if (id <= 0)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Id can't be zero or negative.")
                };

            using (var db = _dbFactory.OpenDbConnection())
            {
                var suplier = db.SingleById<SUP01>(id);

                if (suplier == null)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                string username = suplier.P01F03.Split('@')[0];
                db.DeleteById<SUP01>(id);
                db.DeleteWhere<USR01>("R01F02 = {0}", new object[] { username });

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Suplier deleted successfully.")
                };
            }
        }

        internal object GetAll()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                var supliers = db.Select<SUP01>();
                return supliers;
            }
        }

        internal HttpResponseMessage Update(SUP01 objUpdatedSuplier)
        {
            if (objUpdatedSuplier.P01F01 <= 0)
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Id can't be zero or negative.")
                };

            using (var db = _dbFactory.OpenDbConnection())
            {
                SUP01 existingSuplier = db.SingleById<SUP01>(objUpdatedSuplier.P01F01);

                if (existingSuplier == null)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                existingSuplier.P01F02 = objUpdatedSuplier.P01F02;
                existingSuplier.P01F05 = objUpdatedSuplier.P01F05;
                existingSuplier.P01F06 = objUpdatedSuplier.P01F06;

                db.Update(existingSuplier);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Suplier updated successfully.")
                };
            }
        }
    }
}