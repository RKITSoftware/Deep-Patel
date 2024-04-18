using OnlineShoppingAPI.BL.Common.Interface;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.POCO;

namespace OnlineShoppingAPI.BL.Master.Interface
{
    /// <summary>
    /// Interface service for <see cref="PRO02"/>.
    /// </summary>
    public interface IPRO02Service : ICommonDataHandlerService<DTOPRO02>
    {
        #region Public Methods

        /// <summary>
        /// Deletes the product specified by id.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response Delete(int id);

        /// <summary>
        /// Retrieves all products information from the database.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response GetAll();

        /// <summary>
        /// Gets the product's full information using DB.
        /// </summary>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response GetInformation();

        /// <summary>
        /// Updates the sell price of the specified product which id is given.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="sellPrice">Updates sell price.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response UpdateSellPrice(int id, int sellPrice);

        #endregion Public Methods
    }
}