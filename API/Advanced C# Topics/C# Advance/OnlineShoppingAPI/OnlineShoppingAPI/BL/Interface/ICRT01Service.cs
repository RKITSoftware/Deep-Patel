using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.Enum;
using OnlineShoppingAPI.Models.POCO;

namespace OnlineShoppingAPI.BL.Interface
{
    /// <summary>
    /// Interface for handling <see cref="CRT01"/> related services.
    /// </summary>
    public interface ICRT01Service
    {
        /// <summary>
        /// For buying one single item from the cart.
        /// </summary>
        /// <param name="id">Cart id</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void BuySingleItem(int id, out Response response);

        /// <summary>
        /// Delete the specified cart items from the database.
        /// </summary>
        /// <param name="id">Cart Id.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void Delete(int id, out Response response);

        /// <summary>
        /// Generates a OTP for the 2-Factor Authentication process of buying all items.
        /// </summary>
        /// <param name="id">Customer ID.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void Generate(int id, out Response response);

        /// <summary>
        /// Gets the customer's cart details by using id.
        /// </summary>
        /// <param name="id">Customer Id.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void GetCUS01CRT01Details(int id, out Response response);

        /// <summary>
        /// Gets the full infomration of customer's cart items with product name.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void GetFullCRT01InfoOfCUS01(int id, out Response response);

        /// <summary>
        /// Initialize objects for the creating or updating related operations.
        /// </summary>
        /// <param name="objDTOCRT01">DTO for CRT01.</param>
        /// <param name="operation">Specifies which operation Create or Update.</param>
        void PreSave(DTOCRT01 objDTOCRT01, EnmOperation operation);

        /// <summary>
        /// Performs the create or update changes to the database.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void Save(out Response response);

        /// <summary>
        /// Validates the object for creating or updated related operations.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        /// <returns><see langword="true"/> if validation successful, else <see langword="false"/>.</returns>
        bool Validation(out Response response);

        /// <summary>
        /// Verifies the OTP and Buy items from the cart of customer's.
        /// </summary>
        /// <param name="id">Customer Id.</param>
        /// <param name="otp">OTP (One Time Password) for the verification of buying process.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void VerifyAndBuy(int id, string otp, out Response response);
    }
}