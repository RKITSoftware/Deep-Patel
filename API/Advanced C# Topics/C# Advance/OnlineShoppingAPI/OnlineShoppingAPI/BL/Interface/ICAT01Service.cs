using OnlineShoppingAPI.BL.Common.Interface;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.DTO;
using OnlineShoppingAPI.Models.POCO;

namespace OnlineShoppingAPI.BL.Interface
{
    /// <summary>
    /// Interface for service handling of <see cref="CAT01"/> operations.
    /// </summary>
    public interface ICAT01Service : IOperationService, IPreDataHandlerService<DTOCAT01>,
        IDataHandlerService
    {
        #region Public Methods

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to be deleted.</param>
        /// <param name="response">Out parameter containing the response status after deletion.</param>
        void Delete(int id, out Response response);

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <param name="response">Out parameter containing the response with all categories.</param>
        void GetAll(out Response response);

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <param name="response">Out parameter containing the response with the requested category.</param>
        void GetById(int id, out Response response);

        #endregion
    }
}
