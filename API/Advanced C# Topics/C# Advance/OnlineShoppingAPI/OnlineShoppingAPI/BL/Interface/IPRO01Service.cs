using OnlineShoppingAPI.BL.Common.Interface;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.POCO;

namespace OnlineShoppingAPI.BL.Interface
{
    /// <summary>
    /// Interface service for <see cref="PRO01"/>.
    /// </summary>
    public interface IPRO01Service : IOperationService, IPreDataHandlerService<DTOPRO01>,
        IDataHandlerService
    {
        #region Public Methods

        /// <summary>
        /// Deletes the customer specified by Id.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void Delete(int id, out Response response);

        /// <summary>
        /// Retrieves all customer information.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void GetAll(out Response response);

        /// <summary>
        /// Update the quantity of product.
        /// </summary>
        /// <param name="id">Product Id.</param>
        /// <param name="quantity">Quantity that user wants to add.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void UpdateQuantity(int id, int quantity, out Response response);

        #endregion
    }
}