using OnlineShoppingAPI.BL.Common.Interface;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.POCO;

namespace OnlineShoppingAPI.BL.Master.Interface
{
    /// <summary>
    /// Interface for handling <see cref="CRT01"/> related services.
    /// </summary>
    public interface ICRT01Service : ICommonDataHandlerService<DTOCRT01>
    {
        #region Public Methods

        /// <summary>
        /// For buying one single item from the cart.
        /// </summary>
        /// <param name="id">Cart id</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response BuySingleItem(int id);

        /// <summary>
        /// Delete the specified cart items from the database.
        /// </summary>
        /// <param name="id">Cart Id.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response Delete(int id);

        /// <summary>
        /// Generates a OTP for the 2-Factor Authentication process of buying all items.
        /// </summary>
        /// <param name="id">Customer ID.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response Generate(int id);

        /// <summary>
        /// Gets the customer's cart details by using id.
        /// </summary>
        /// <param name="id">Customer Id.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response GetCUS01CRT01Details(int id);

        /// <summary>
        /// Gets the full infomration of customer's cart items with product name.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response GetFullCRT01InfoOfCUS01(int id);

        /// <summary>
        /// Verifies the OTP and Buy items from the cart of customer's.
        /// </summary>
        /// <param name="id">Customer Id.</param>
        /// <param name="otp">OTP (One Time Password) for the verification of buying process.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response VerifyAndBuy(int id, string otp);

        #endregion Public Methods
    }
}