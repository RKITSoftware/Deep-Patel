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
        /// Validation checks before the delete operation.
        /// </summary>
        /// <param name="id">Product id.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response DeleteValidation(int id);

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

        /// <summary>
        /// Validation checks the product before updating the sell price.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <param name="sellPrice">Updates sell price.</param>
        /// <returns>Success response if no error occur else response with error message.</returns>
        Response UpdateSellPriceValidation(int id, int sellPrice);

        /// <summary>
        /// Validate the id before getting the product data of that category.
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns>Success response if category exists else notfound response.</returns>
        Response ValidationForGetPRO02ByCAT01(int id);

        /// <summary>
        /// Gets the product of category specified by id.
        /// </summary>
        /// <param name="id">Category Id.</param>
        /// <returns>Ok response containing the data if data exists else no content response.</returns>
        Response GetProductByCategory(int id);

        /// <summary>
        /// Validate the id before getting the product data of that supplier.
        /// </summary>
        /// <param name="id">Supplier Id</param>
        /// <returns>Success response if category exists else notfound response.</returns>
        Response ValidationForGetPRO02BySUP01(int id);

        /// <summary>
        /// Gets the product of supplier specified by id.
        /// </summary>
        /// <param name="id">Supplier Id.</param>
        /// <returns>Ok response containing the data if data exists else no content response.</returns>
        Response GetProductBySupplier(int id);

        #endregion Public Methods
    }
}