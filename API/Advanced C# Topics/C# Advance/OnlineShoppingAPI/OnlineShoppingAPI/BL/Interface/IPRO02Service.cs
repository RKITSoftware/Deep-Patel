using OnlineShoppingAPI.BL.Common.Interface;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.POCO;

namespace OnlineShoppingAPI.BL.Interface
{
    /// <summary>
    /// Interface service for <see cref="PRO02"/>.
    /// </summary>
    public interface IPRO02Service : IOperationService, IPreDataHandlerService<DTOPRO02>,
        IDataHandlerService
    {
        #region Public Methods

        /// <summary>
        /// Deletes the product specified by id.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void Delete(int id, out Response response);

        /// <summary>
        /// Retrieves all products information from the database.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void GetAll(out Response response);

        /// <summary>
        /// Gets the product's full information using DB.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void GetInformation(out Response response);

        /// <summary>
        /// Updates the sell price of the specified product which id is given.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="sellPrice">Updates sell price.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void UpdateSellPrice(int id, int sellPrice, out Response response);

        #endregion
    }
}