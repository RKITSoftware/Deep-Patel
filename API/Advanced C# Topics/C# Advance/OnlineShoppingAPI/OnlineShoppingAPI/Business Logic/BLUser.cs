using ServiceStack.Data;
using System.Web;
using System;
using ServiceStack.OrmLite;
using OnlineShoppingAPI.Models;

namespace OnlineShoppingAPI.Business_Logic
{
    public class BLUser
    {
        private static readonly IDbConnectionFactory _dbFactory;

        static BLUser()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        internal static USR01 GetUser(string username)
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                return db.Single<USR01>(u => u.R01F02.Equals(username));
            }
        }

        internal static bool IsExist(string username, string password)
        {
            using(var db = _dbFactory.OpenDbConnection())
            {
                return db.Exists<USR01>(u => u.R01F02.Equals(username) && u.R01F03.Equals(password));
            }
        }
    }
}