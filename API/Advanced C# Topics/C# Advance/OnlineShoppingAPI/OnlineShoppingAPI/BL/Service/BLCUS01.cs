using ServiceStack.Data;
using System;
using System.Web;

namespace OnlineShoppingAPI.BL.Service
{
    public class BLCUS01
    {
        /// <summary>
        /// _dbFactory is used to store the reference of database connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        public BLCUS01()
        {
            // Getting data connection from Application state
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;

            // If database can't be connect.
            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }
    }
}