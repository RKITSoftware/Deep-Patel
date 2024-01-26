using OnlineShoppingAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Web;

namespace OnlineShoppingAPI.Business_Logic
{
    public class BLUser
    {
        /// <summary>
        /// _dbFactory is used to store the reference of database connection.
        /// </summary>
        private static readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Static constructor is used to initialize _dbfactory for future reference.
        /// </summary>
        /// <exception cref="ApplicationException">If database can't connect then this exception shows.</exception>
        static BLUser()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        /// <summary>
        /// Getting user detail for authentication
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <returns>User detail</returns>
        internal static USR01 GetUser(string username)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.Single<USR01>(u => u.R01F02.Equals(username));
            }
        }

        /// <summary>
        /// Checking user exist or not
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <param name="password">Password of user</param>
        /// <returns>True is user exist false if not.</returns>
        internal static bool IsExist(string username, string password)
        {
            using(var db = _dbFactory.OpenDbConnection())
            {
                return db.Exists<USR01>(u => u.R01F02.Equals(username) && u.R01F03.Equals(password));
            }
        }
    }
}