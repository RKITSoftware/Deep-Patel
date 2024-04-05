using OnlineShoppingAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace OnlineShoppingAPI.Business_Logic
{
    /// <summary>
    /// Helper class containing various utility methods for the online shooping solution.
    /// </summary>
    public class BLHelper
    {
        #region Private Fields

        /// <summary>
        /// _dbFactory is used to store the reference of the database connection.
        /// </summary>
        private static readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// AES (Advanced Encryption Standard) encryption object for secure password handling.
        /// </summary>
        private static Aes _objAes;

        /// <summary>
        /// Key used for AES encryption. It should be a 32-character hexadecimal string.
        /// </summary>
        private static readonly string key = "0123456789ABCDEF0123456789ABCDEF";

        /// <summary>
        /// Initialization Vector (IV) used for AES encryption. 
        /// </summary>
        private static readonly string iv = "0123456789ABCDEF";

        /// <summary>
        /// Stores the file path where log information of exception want to store.
        /// </summary>
        private static readonly string _logFolderPath;

        #endregion

        #region Public Properties

        /// <summary>
        /// Cache for storing server-related data.
        /// </summary>
        public static Cache ServerCache;

        #endregion

        #region Constructors

        /// <summary>
        /// Static constructor to initialize static members of the BLHelper class.
        /// </summary>
        static BLHelper()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
            _objAes = Aes.Create();

            _objAes.Key = Encoding.UTF8.GetBytes(key);
            _objAes.IV = Encoding.UTF8.GetBytes(iv);

            _logFolderPath = HttpContext.Current.Application["LogFolderPath"] as string;

            ServerCache = new Cache();

            if (_dbFactory == null)
            {
                throw new ApplicationException("IDbConnectionFactory not found in Application state.");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves user details for authentication.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <returns>User details. Null if the user is not found.</returns>
        public static USR01 GetUser(string username)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    return db.Single<USR01>(u => u.R01F02.Equals(username));
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// Retrieves user details for authentication.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <returns>User details. Null if the user is not found.</returns>
        public static USR01 GetUser(int id)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    return db.SingleById<USR01>(id);
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// Retrieves user details.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <returns>User details. Null if the user is not found.</returns>
        public static USR01 GetUser(string username, string password)
        {
            try
            {
                string encryptedPassword = GetEncryptPassword(password);
                using (var db = _dbFactory.OpenDbConnection())
                {
                    return db.Single<USR01>(u =>
                        u.R01F02.Equals(username) &&
                        u.R01F05.Equals(encryptedPassword));
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                return null;
            }
        }

        /// <summary>
        /// Checks if a user exists.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        public static bool IsExist(string username)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    return db.Exists<USR01>(u => u.R01F02.Equals(username));
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                LogError(ex);
                return false;
            }
        }

        /// <summary>
        /// Checks if a user exists.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        public static bool IsExist(string username, string password)
        {
            try
            {
                string encryptedPassword = GetEncryptPassword(password);
                using (var db = _dbFactory.OpenDbConnection())
                {
                    return db.Exists<USR01>(u =>
                        u.R01F02.Equals(username) &&
                        u.R01F05.Equals(encryptedPassword));
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                LogError(ex);
                return false;
            }
        }

        /// <summary>
        /// Encrypts a password using AES encryption.
        /// </summary>
        /// <param name="plaintext">The plaintext password to be encrypted.</param>
        /// <returns>Encrypted password as a Base64-encoded string.</returns>
        public static string GetEncryptPassword(string plaintext)
        {
            try
            {
                ICryptoTransform encryptor = _objAes.CreateEncryptor(_objAes.Key, _objAes.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor,
                        CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plaintext);
                        }
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                LogError(ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// Writes exception information to a text file.
        /// </summary>
        /// <param name="exception">The exception that occurred.</param>
        /// <param name="directoryPath">The directory path for storing log files.</param>
        public static void LogError(Exception exception)
        {
            try
            {
                if (!Directory.Exists(_logFolderPath))
                {
                    Directory.CreateDirectory(_logFolderPath);
                }

                string filePath = Path.Combine(_logFolderPath, $"{DateTime.Today:dd-MM-yy}.txt");

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
                    string error = $"Time: {DateTime.Now:HH:mm:ss}{line}" +
                                   $"Error Message: {_errorMsg}{line}" +
                                   $"Exception Type: {_exType}{line}" +
                                   $"Error Stack Trace: {exception.StackTrace}{line}";

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

        /// <summary>
        /// Creates an HttpResponseMessage with the specified HTTP status code and message content.
        /// </summary>
        /// <param name="statusCode">The HTTP status code for the response.</param>
        /// <param name="message">The content message to be included in the response.</param>
        /// <returns>An HttpResponseMessage with the specified status code and message content.</returns>
        public static HttpResponseMessage ResponseMessage(HttpStatusCode statusCode, string message)
        {
            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(message)
            };
        }

        /// <summary>
        /// When a product is bought at that time this method calculates the profit and 
        /// add it to that day's profit.
        /// </summary>
        /// <param name="objProduct">Products buy price and Sell price information</param>
        /// <param name="quantity">Quantity that user bought.</param>
        public static void UpdateProfit(PRO02 objProduct, int quantity)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.CreateTableIfNotExists<PFT01>();

                    PFT01 objProfit = db.Single<PFT01>(p =>
                        p.T01F02 == "14-03-2024");

                    if (objProfit != null)
                    {
                        objProfit.T01F03 += (objProduct.O02F04 - objProduct.O02F03) * quantity;
                        db.Update(objProfit);
                        return;
                    }

                    db.Insert(new PFT01()
                    {
                        T01F02 = "15-03-2024",
                        T01F03 = (objProduct.O02F04 - objProduct.O02F03) * quantity
                    });
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        /// <summary>
        /// Gets the year data of this running like how much profit company earns this year.
        /// </summary>
        /// <returns>A list of decimal which contains month wise profit.</returns>
        public static List<decimal> GetMonthData()
        {
            try
            {
                List<decimal> lstData = new List<decimal>();
                using (var db = _dbFactory.OpenDbConnection())
                {
                    int year = DateTime.Now.Year;
                    for (int month = 1; month <= 12; month++)
                    {
                        decimal profit = db.SqlScalar<decimal>(
                            @"SELECT 
                                SUM(pft01.T01F03) AS 'Profit' 
                            FROM 
                                pft01 
                            WHERE pft01.T01F02 " +
                                $"LIKE '__-{month.ToString("00")}-{year.ToString("0000")}'");

                        lstData.Add(profit);
                    }
                }

                return lstData;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new List<decimal>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }
        }

        /// <summary>
        /// Gets the last 10 years profit data for analysis.
        /// </summary>
        /// <returns>A list of last 10 year profit.</returns>
        public static List<decimal> GetPreviousYearData()
        {
            try
            {
                List<decimal> lstData = new List<decimal>();
                using (var db = _dbFactory.OpenDbConnection())
                {
                    for (int year = DateTime.Now.Year - 9; year <= DateTime.Now.Year; year++)
                    {
                        decimal profit = db.SqlScalar<decimal>(
                            @"SELECT 
                                SUM(pft01.T01F03) AS 'Profit' 
                            FROM 
                                pft01 
                            WHERE pft01.T01F02 " +
                                $"LIKE '__-__-{year.ToString("0000")}'");

                        lstData.Add(profit);
                    }
                }

                return lstData;
            }
            catch (Exception ex)
            {
                LogError(ex);
                return new List<decimal>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            }
        }

        /// <summary>
        /// Returns the day wise profit of running month.
        /// </summary>
        /// <returns></returns>
        public static List<decimal> GetDayWiseProfit()
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int days = DateTime.DaysInMonth(year, month);

            try
            {
                List<decimal> lstData = new List<decimal>();
                using (var db = _dbFactory.OpenDbConnection())
                {
                    for (int day = 1; day <= days; day++)
                    {
                        decimal profit = db.SqlScalar<decimal>(
                            @"SELECT 
                                pft01.T01F03 AS 'Profit' 
                            FROM 
                                pft01 
                            WHERE pft01.T01F02 " +
                                $"LIKE '{day.ToString("00")}-{month.ToString("00")}-{year.ToString("0000")}'");

                        lstData.Add(profit);
                    }
                }

                return lstData;
            }
            catch (Exception ex)
            {
                LogError(ex);

                List<decimal> lstData = new List<decimal>();
                for (int day = 1; day <= days; day++)
                    lstData.Add(0);

                return lstData;
            }
        }

        public static Response ISEResponse()
        {
            return new Response()
            {
                IsError = true,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "An internal error occured during request."
            };
        }
        #endregion
    }
}