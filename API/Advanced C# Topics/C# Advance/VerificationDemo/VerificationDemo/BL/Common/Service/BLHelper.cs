using System.Net;
using VerificationDemo.Models;

namespace VerificationDemo.BL.Common.Service
{
    /// <summary>
    /// Helper claass contains the methods that are common and most usuable.
    /// </summary>
    public static class BLHelper
    {
        #region Public Methods 

        /// <summary>
        /// Returns the Ok Response for the api request.
        /// </summary>
        /// <param name="message">Message to return during api request.</param>
        /// <returns>Ok Response.</returns>
        public static Response OkResponse(string message = "Success")
        {
            return new Response()
            {
                StatusCode = HttpStatusCode.OK,
                Message = message
            };
        }

        /// <summary>
        /// Returns the NotFound Response for the api request.
        /// </summary>
        /// <param name="message">Message to return during api request.</param>
        /// <returns>NotFound Response.</returns>
        public static Response NotFoundResponse(string message = "Not found data.")
        {
            return new Response()
            {
                IsError = true,
                StatusCode = HttpStatusCode.NotFound,
                Message = message
            };
        }

        /// <summary>
        /// Returns the PreConditionFailed Response for the api request.
        /// </summary>
        /// <param name="message">Message to return during api request.</param>
        /// <returns>PreConditionFailed Response.</returns>
        public static Response PreConditionFailedResponse(string message = "Pre condition failed.")
        {
            return new Response()
            {
                IsError = true,
                StatusCode = HttpStatusCode.NotFound,
                Message = message
            };
        }

        /// <summary>
        /// Returns the Internal Server Error Response for the api request.
        /// </summary>
        /// <returns>Internal Server Error Response.</returns>
        public static Response InternalServerErrorResponse()
        {
            return new Response()
            {
                IsError = true,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "An Internal Server Error occur during Request."
            };
        }

        #endregion
    }
}