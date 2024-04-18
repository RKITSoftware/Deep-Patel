using OnlineShoppingAPI.Models;
using System.Net;
using System.Net.Http;
using System.Web.Caching;

namespace OnlineShoppingAPI.BL.Common
{
    /// <summary>
    /// Helper class for common method of this project.
    /// </summary>
    public class BLHelper
    {
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
            ServerCache = new Cache();
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Checks the id for add and edit operation.
        /// </summary>
        /// <param name="operation">Add or Edit operation.</param>
        /// <param name="id">Create or Update id</param>
        /// <returns>True if id is valid else False.</returns>
        public static bool IsIDValid(EnmOperation operation, int id)
        {
            if (operation == EnmOperation.A)
            {
                if (id > 0)
                {
                    return false;
                }
            }

            if (operation == EnmOperation.E)
            {
                if (id == 0)
                {
                    return false;
                }
            }

            return true;
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