using PlacementCellManagementAPI.Models;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace PlacementCellManagementAPI.Business_Logic.Common
{
    /// <summary>
    /// Helper class for containing the common methods.
    /// </summary>
    public static class Helper
    {
        #region Private Fields

        /// <summary>
        /// AES (Advanced Encryption Standard) encryption object for secure password handling.
        /// </summary>
        private static readonly Aes _objAes;

        /// <summary>
        /// Initialization Vector (IV) used for AES encryption.
        /// </summary>
        private static readonly string _iv = "0123456789ABCDEF";

        /// <summary>
        /// Key used for AES encryption.
        /// </summary>
        private static readonly string _key = "0123456789ABCDEF0123456789ABCDEF";

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes static members of the <see cref="Helper"/> class.
        /// </summary>
        static Helper()
        {
            _objAes = Aes.Create();

            _objAes.Key = Encoding.UTF8.GetBytes(_key);
            _objAes.IV = Encoding.UTF8.GetBytes(_iv);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Encrypts a password using AES encryption.
        /// </summary>
        /// <param name="plaintext">The plaintext to be encrypted.</param>
        /// <returns>Encrypted ciphertext.</returns>
        public static string GetEncryptPassword(string? plaintext)
        {
            ICryptoTransform encryptor = _objAes.CreateEncryptor(_objAes.Key, _objAes.IV);

            using MemoryStream msEncrypt = new();
            using (CryptoStream csEncrypt = new(msEncrypt,
                                                encryptor,
                                                CryptoStreamMode.Write))
            {
                using StreamWriter swEncrypt = new(csEncrypt);
                swEncrypt.Write(plaintext);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        /// <summary>
        /// Generates a NotFoundResponse.
        /// </summary>
        /// <param name="message">The message to include in the response.</param>
        /// <returns>A NotFoundResponse with the specified message.</returns>
        public static Response NotFoundResponse(string message = "Not found.") => new()
        {
            IsError = true,
            StatusCode = HttpStatusCode.NotFound,
            Message = message
        };

        /// <summary>
        /// Generates an OkResponse.
        /// </summary>
        /// <param name="message">The message to include in the response.</param>
        /// <param name="data">The data to include in the response.</param>
        /// <returns>An OkResponse with the specified message and data.</returns>
        public static Response OkResponse(string message = "Success", dynamic? data = null) => new()
        {
            StatusCode = HttpStatusCode.OK,
            Message = message,
            Data = data
        };

        /// <summary>
        /// Generates a NoContentResponse.
        /// </summary>
        /// <param name="message">The message to include in the response.</param>
        /// <returns>A NoContentResponse with the specified message.</returns>
        public static Response NoContentResponse(string message = "No Content") => new()
        {
            IsError = true,
            StatusCode = HttpStatusCode.NoContent,
            Message = message
        };

        /// <summary>
        /// Generates a PreConditionFailedResponse
        /// </summary>
        /// <param name="message">The message to include in the response.</param>
        /// <returns>A PreConditionFailedResponse with the specified message.</returns>
        public static Response PreConditionFailedResponse(string message = "Pre Condition Failed") => new()
        {
            IsError = true,
            StatusCode = HttpStatusCode.PreconditionFailed,
            Message = message
        };

        #endregion
    }
}
