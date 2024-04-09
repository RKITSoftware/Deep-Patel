using OnlineShoppingAPI.BL.Common;
using System;
using System.Net;
using System.Net.Http;

namespace OnlineShoppingAPI.Extension
{
    public static class OnlineShoppingExtensions
    {
        public static bool IsSuccessStatusCode(this HttpResponseMessage response)
        {
            return response.StatusCode >= HttpStatusCode.OK &&
                response.StatusCode < HttpStatusCode.Ambiguous;
        }

        public static void LogException(this Exception exception)
        {
            BLHelper.LogError(exception);
        }
    }
}