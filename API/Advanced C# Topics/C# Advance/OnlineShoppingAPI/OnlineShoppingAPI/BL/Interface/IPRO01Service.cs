using OnlineShoppingAPI.BL.Common.Interface;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.POCO;

namespace OnlineShoppingAPI.BL.Interface
{
    /// <summary>
    /// Interface service for <see cref="PRO01"/>.
    /// </summary>
    public interface IPRO01Service : ICommonDataHandlerService<DTOPRO01>
    {
        #region Public Methods

        /// <summary>
        /// Deletes the customer specified by Id.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response Delete(int id);

        /// <summary>
        /// Retrieves all customer information.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response GetAll();

        /// <summary>
        /// Update the quantity of product.
        /// </summary>
        /// <param name="id">Product Id.</param>
        /// <param name="quantity">Quantity that user wants to add.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response UpdateQuantity(int id, int quantity);

        #endregion Public Methods
    }
}