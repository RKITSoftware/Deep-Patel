using OnlineShoppingAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace OnlineShoppingAPI.Business_Logic
{
    public class BLUser
    {
        /// <summary>
        /// _dbFactory is used to store the reference of database connection.
        /// </summary>
        private static readonly IDbConnectionFactory _dbFactory;

        private static Aes objAes;
        public static readonly string key = "0123456789ABCDEF0123456789ABCDEF";
        public static readonly string iv = "0123456789ABCDEF";

        /// <summary>
        /// Static constructor is used to initialize _dbfactory for future reference.
        /// </summary>
        /// <exception cref="ApplicationException">If database can't connect then this exception shows.</exception>
        static BLUser()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            objAes = Aes.Create();

            objAes.Key = Encoding.UTF8.GetBytes(key);
            objAes.IV = Encoding.UTF8.GetBytes(iv);

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

        internal static string GetEncryptPassword(string plaintext)
        {
            ICryptoTransform encryptor = objAes.CreateEncryptor(objAes.Key, objAes.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plaintext);
                    }
                }

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }
}