using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace OnlineShoppingAPI.BL.Common
{
    /// <summary>
    /// Helper class for common method of this project.
    /// </summary>
    public class BLHelper
    {
        #region Private Fields

        /// <summary>
        /// _dbFactory is used to store the reference of the database connection.
        /// </summary>
        private static readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Stores the file path where log information of exception want to store.
        /// </summary>
        private static readonly string _logFolderPath;

        /// <summary>
        /// AES (Advanced Encryption Standard) encryption object for secure password handling.
        /// </summary>
        private static readonly Aes _objAes;

        /// <summary>
        /// Initialization Vector (IV) used for AES encryption.
        /// </summary>
        private static readonly string iv = "0123456789ABCDEF";

        /// <summary>
        /// Key used for AES encryption.
        /// </summary>
        private static readonly string key = "0123456789ABCDEF0123456789ABCDEF";

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Cache for storing server-related cache-data.
        /// </summary>
        public static Cache ServerCache;

        #endregion Public Properties

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

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Encrypts a password using AES encryption.
        /// </summary>
        /// <param name="plaintext">The plaintext to be encrypted.</param>
        /// <returns>Encrypted ciphertext.</returns>
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
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Retrieves user details using user id.
        /// </summary>
        /// <param name="username">User id of the user.</param>
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
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Retrieves user details using username.
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
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Retrieves user details using username and password.
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
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// Checks if a user exists using username and password.
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
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Writes exception information to a text file.
        /// </summary>
        /// <param name="exception">The exception that occurred.</param>
        public static void LogError(Exception exception)
        {
            try
            {
                // Checks directory exists or not.
                if (!Directory.Exists(_logFolderPath))
                {
                    Directory.CreateDirectory(_logFolderPath);
                }

                string filePath = Path.Combine(_logFolderPath, $"{DateTime.Today:dd-MM-yy}.txt");

                // Checks the log file exists or not.
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
                Console.WriteLine($"An error occurred while logging: {ex}");
            }
        }

        /// <summary>
        /// Returns the NoContent response with the message.
        /// </summary>
        /// <param name="message">Message to sent to response.</param>
        /// <returns>PreConditionFailed <see cref="Response"/></returns>
        public static Response NoContentResponse(string message = "No data available.")
        {
            return new Response()
            {
                IsError = true,
                StatusCode = HttpStatusCode.NoContent,
                Message = message
            };
        }

        /// <summary>
        /// Returns the Notfound response with specified message.
        /// </summary>
        /// <param name="message">Message to sent to response.</param>
        /// <returns>NotFound <see cref="Response"/></returns>
        public static Response NotFoundResponse(string message)
        {
            return new Response()
            {
                IsError = true,
                StatusCode = HttpStatusCode.NotFound,
                Message = message
            };
        }

        /// <summary>
        /// Retuns the Success response with Success Message.
        /// </summary>
        /// <returns><see cref="Response"/> containing the Success response.</returns>
        public static Response OkResponse(string message = "Success", dynamic data = null)
        {
            return new Response()
            {
                StatusCode = HttpStatusCode.OK,
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// Returns the PreConditionFailed response with specified message.
        /// </summary>
        /// <param name="message">Message to sent to response.</param>
        /// <returns>PreConditionFailed <see cref="Response"/></returns>
        public static Response PreConditionFailedResponse(string message)
        {
            return new Response()
            {
                IsError = true,
                StatusCode = HttpStatusCode.PreconditionFailed,
                Message = message
            };
        }
        /// <summary>
        /// Creates an <see cref="HttpResponseMessage"/> with the specified HTTP status code
        /// and message content.
        /// </summary>
        /// <param name="statusCode">The HTTP status code for the response.</param>
        /// <param name="message">The content message to be included in the response.</param>
        /// <returns>
        /// An <see cref="HttpResponseMessage"/> with the specified status code and message content.
        /// </returns>
        public static HttpResponseMessage ResponseMessage(HttpStatusCode statusCode, string message)
        {
            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(message)
            };
        }

        #endregion Public Methods
    }
}