using OnlineShoppingAPI.Models;

namespace OnlineShoppingAPI.BL.Common.Interface
{
    /// <summary>
    /// Services for Validation and Database Save process.
    /// </summary>
    public interface IDataHandlerService
    {
        #region Public Methods

        /// <summary>
        /// Performs the database add or update operation.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        void Save(out Response response);

        /// <summary>
        /// Validate the objects before the save process.
        /// </summary>
        /// <param name="response"><see cref="Response"/> indicating the outcome of the operation.</param>
        /// <returns>True if validation is successfully complete, else false.</returns>
        bool Validation(out Response response);

        #endregion
    }
}
