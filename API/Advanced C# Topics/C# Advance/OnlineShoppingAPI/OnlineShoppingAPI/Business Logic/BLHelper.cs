using OnlineShoppingAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace OnlineShoppingAPI.Business_Logic
{
    public class BLHelper
    {
        public static Cache ServerCache;

        /// <summary>
        /// _dbFactory is used to store the reference of database connection.
        /// </summary>
        private static readonly IDbConnectionFactory _dbFactory;

        private static Aes objAes;
        private static readonly string key = "0123456789ABCDEF0123456789ABCDEF";
        private static readonly string iv = "0123456789ABCDEF";

        static BLHelper()
        {
            ServerCache = new Cache();
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
            using (var db = _dbFactory.OpenDbConnection())
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

        /// <summary>
        /// Write exception information to file.
        /// </summary>
        /// <param name="exception">Exception which occured</param>
        /// <param name="directoryPath">Log directory path</param>
        internal static void SendErrorToTxt(Exception exception, string directoryPath)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string filePath = Path.Combine(directoryPath, $"{DateTime.Today:dd-MM-yy}.txt");

                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();
                }

                string line = Environment.NewLine;
                string _errorMsg = exception.GetType().Name;
                string _exType = exception.GetType().ToString();

                using (StreamWriter writer = File.AppendText(filePath))
                {
                    // Error message creation
                    string error = $"Time :- {DateTime.Now:HH:mm:ss}{line}" +
                                   $"Error Message :- {_errorMsg}{line}" +
                                   $"Exception Type :- {_exType}{line}" +
                                   $"Error Stack Trace :- {exception.StackTrace}{line}";

                    writer.WriteLine(error);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                // Log the exception, e.g., print to console or use a dedicated logging framework
                Console.WriteLine($"An error occurred while logging: {ex}");
            }
        }
    }
}