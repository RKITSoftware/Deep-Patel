using OnlineShoppingAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace OnlineShoppingAPI.Business_Logic
{
    /// <summary>
    /// BLAdmin class contains the business logic of CLAdminController
    /// </summary>
    public class BLAdmin
    {
        /// <summary>
        /// _dbFactory is used to store the reference of database connection.
        /// </summary>
        private static readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Static constructor is used to initialize _dbfactory for future reference.
        /// </summary>
        /// <exception cref="ApplicationException">If database can't connect then this exception shows.</exception>
        static BLAdmin()
        {
            // Getting data connection from Application state
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            // If database can't be connect.
            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        /// <summary>
        /// Changing the admin password using username.
        /// </summary>
        /// <param name="username">Admin username</param>
        /// <param name="newPassword">New password</param>
        /// <returns>Ok response</returns>
        internal HttpResponseMessage ChangePassword(string username, string oldPassword, string newPassword)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                // Getting admin details.
                ADM01 objAdmin = db.Single(db.From<ADM01>().Where(a => a.M01F03.StartsWith(username) && a.M01F04.Equals(oldPassword)));
                USR01 objUser = db.Single(db.From<USR01>().Where(u => u.R01F02.StartsWith(username)));
                
                // If admin doesn't exist then not found statuscode return.
                if (objAdmin == null)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                // Updating password
                objAdmin.M01F04 = newPassword;
                objUser.R01F03 = newPassword;
                objUser.R01F05 = BLUser.GetEncryptPassword(newPassword);

                db.Update(objAdmin);
                db.Update(objUser);

                return new HttpResponseMessage(HttpStatusCode.OK) 
                {
                    Content = new StringContent("Password changed successfully.")
                };
            }
        }

        /// <summary>
        /// Creating a admin
        /// </summary>
        /// <param name="objAdmin">Admin data</param>
        /// <returns>Create response</returns>
        internal HttpResponseMessage Create(ADM01 objAdmin)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                // db.CreateTable<ADM01>();
                // db.CreateTable<USR01>();
                // db.CreateTable<CUS01>();
                // db.CreateTable<SUP01>();
                // db.CreateTable<PRO01>();
                // db.CreateTable<RCD01>();

                // Inserting admin details
                db.Insert(objAdmin);

                // Extracting information for USR01
                var emailParts = objAdmin.M01F03.Split('@');
                var username = emailParts[0];
                var newPassword = objAdmin.M01F04;

                // Inserting related USR01 record
                db.Insert(new USR01
                {
                    R01F02 = username,
                    R01F03 = newPassword,
                    R01F04 = "Admin",
                    R01F05 = BLUser.GetEncryptPassword(newPassword)
                });

                return new HttpResponseMessage(HttpStatusCode.Created)
                {
                    Content = new StringContent("Admin created successfully.")
                };
            }
        }

        /// <summary>
        /// Delete admin details using admin id
        /// If only one admin exist then it don't delete admin.
        /// </summary>
        /// <param name="id">Admin id</param>
        /// <returns>Delete response message</returns>
        internal HttpResponseMessage Delete(int id)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                List<ADM01> lstAdmin = db.Select<ADM01>();

                if (lstAdmin.Count == 1)
                {
                    return new HttpResponseMessage(HttpStatusCode.Forbidden)
                    {
                        Content = new StringContent("Server can't fulfill the request because there is only one admin.")
                    };
                }

                // Find the admin by id
                ADM01 adminToDelete = lstAdmin.FirstOrDefault(a => a.M01F01 == id);

                // If the admin with the given id is not found, return Not Found status
                if (adminToDelete == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent("Admin not found.")
                    };
                }

                // Extracting information for USR01
                string username = adminToDelete.M01F03.Split('@')[0];

                // Delete admin and related USR01 record
                db.DeleteById<ADM01>(id);
                db.Delete<USR01>(u => u.R01F02 == username);

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("Admin deleted successfully.")
                };
            }
        }
    }
}