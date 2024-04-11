using OnlineShoppingAPI.BL.Common.Interface;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.POCO;
using System.Collections.Generic;
using System.Net.Http;

namespace OnlineShoppingAPI.BL.Interface
{
    /// <summary>
    /// Service for <see cref="RCD01"/> model.
    /// </summary>
    public interface IRCD01Service : IOperationService, IPreDataHandlerService<DTORCD01>,
        IDataHandlerService
    {
        #region Public Methods

        /// <summary>
        /// Buy all items of customer from the cart.
        /// </summary>
        /// <param name="lstItems">List of items that customer wants to buy.</param>
        /// <param name="s01F03">customer Email Address.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        /// <returns>True for successful completion, else false if error occurs.</returns>
        bool BuyCartItems(List<CRT01> lstItems, string s01F03, out Response response);

        /// <summary>
        /// Deletes the record from the database.
        /// </summary>
        /// <param name="id">Record Id.</param>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void Delete(int id, out Response response);

        /// <summary>
        /// Generate a <see cref="HttpResponseMessage"/> containing the download response as attachment.
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <param name="filetype">File that user wants to download.</param>
        HttpResponseMessage Download(int id, string filetype);

        /// <summary>
        /// Retrieves all records information.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void GetAllRecord(out Response response);

        #endregion
    }
}