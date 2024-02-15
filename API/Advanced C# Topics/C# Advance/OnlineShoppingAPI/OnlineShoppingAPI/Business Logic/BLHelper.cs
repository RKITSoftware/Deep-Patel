﻿using OnlineShoppingAPI.Models;
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
        /// Initialization Vector (IV) used for AES encryption. It should be a 32-character hexadecimal string.
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
        /// Checks if a user exists.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        public static bool IsExist(string username, string password)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    return db.Exists<USR01>(u => u.R01F02.Equals(username) && u.R01F03.Equals(password));
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

        #endregion
    }
}