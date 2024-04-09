using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShoppingAPI.BL.Common.Interface
{
    /// <summary>
    /// Email sent realted services interface.
    /// </summary>
    public interface IEmailService
    {
        #region Public Methods

        /// <summary>
        /// Send the order attachment to the user.
        /// </summary>
        /// <param name="customerEmail">Receiver email address.</param>
        /// <param name="lstItems">List of items that user purchased.</param>
        Task SendAsync(string customerEmail, List<dynamic> lstItems);

        /// <summary>
        /// Send the OTP to the user.
        /// </summary>
        /// <param name="email">Customer email address.</param>
        /// <param name="otp">OTP(One Time Password) for the user.</param>
        void Send(string email, string otp);

        #endregion
    }
}