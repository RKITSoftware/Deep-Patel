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
        /// Buys all items from the customer's cart.
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns>Success response.</returns>
        Response BuyAllItems(int id);

        /// <summary>
        /// For buying one single item from the cart.
        /// </summary>
        /// <param name="id">Cart id</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response BuySingleItem(int id);

        /// <summary>
        /// Checks the cart item if exists or not.
        /// </summary>
        /// <param name="id">Cart item id.</param>
        /// <returns>Ok Response if item exists else NotFound Response</returns>
        Response BuySingleItemValidation(int id);

        /// <summary>
        /// Delete the specified cart items from the database.
        /// </summary>
        /// <param name="id">Cart Id.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response Delete(int id);

        /// <summary>
        /// Checks the item exists or not for delete operation.
        /// </summary>
        /// <param name="id">Cart id</param>
        /// <returns>Ok Response if item exists else NotFound Response.</returns>
        Response DeleteValidation(int id);

        /// <summary>
        /// Generates a OTP for the 2-Factor Authentication process of buying all items.
        /// </summary>
        /// <param name="id">Customer ID.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response GenerateOTP(int id);

        /// <summary>
        /// Validation checks before the generating otp for the customer.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <returns>Ok response if customer exist else notfound response.</returns>
        Response GenerateOTPValidation(int id);

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
        /// Verifies the OTP.
        /// </summary>
        /// <param name="id">Customer Id.</param>
        /// <param name="otp">OTP (One Time Password) for the verification of buying process.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response VerifyOTP(int id, string otp);

        #endregion Public Methods
    }
}