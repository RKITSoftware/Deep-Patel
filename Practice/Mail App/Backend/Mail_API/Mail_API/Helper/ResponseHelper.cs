using Mail_API.Models;
using System.Net;

namespace Mail_API.Helper
{
    public class ResponseHelper
    {
        public static Response BadRequestResponse() => new()
        {
            IsError = true,
            StatusCode = HttpStatusCode.BadRequest,
            Message = "Please send user details."
        };

        public static Response InternalServerErrorResponse() => new()
        {
            Message = "Internal server error occured during request.",
            IsError = true,
            StatusCode = HttpStatusCode.InternalServerError
        };

        public static Response NotFoundResponse(string message = "Not Found.") => new()
        {
            IsError = true,
            Message = message,
            StatusCode = HttpStatusCode.NotFound
        };

        public static Response OkResponse(string message = "Ok", dynamic? data = null) => new()
        {
            Message = message,
            Data = data,
            StatusCode = HttpStatusCode.OK
        };

        public static Response PreconditionFailedResponse(string message = "PreCondition failed.") => new()
        {
            IsError = true,
            StatusCode = HttpStatusCode.PreconditionFailed,
            Message = message
        };
    }
}