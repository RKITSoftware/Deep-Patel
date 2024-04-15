using OnlineShoppingAPI.Models;

namespace OnlineShoppingAPI.BL.Common.Interface
{
    /// <summary>
    /// Common basic data handler services for the Model Handler.
    /// </summary>
    public interface ICommonDataHandlerService<T>
        where T : class
    {
        #region Public Properties

        /// <summary>
        /// Specify the operation to be performed during API request.
        /// </summary>
        EnmOperation Operation { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// DTO to POCO Model conversion and other fields initialization.
        /// </summary>
        /// <param name="dto">DTO object of specific model.</param>
        void PreSave(T dto);

        /// <summary>
        /// PreValidation before the PreSave process like checking data exists or not.
        /// </summary>
        /// <param name="dto">DTO object of specific model.</param>
        /// <returns>Response indicating the outcome of the PreValidation operation.</returns>
        Response PreValidation(T dto);

        /// <summary>
        /// Performs the database add or update operation.
        /// </summary>
        /// <returns>Response indicating the outcome of the Save operation.</returns>
        Response Save();

        /// <summary>
        /// Validate the objects before the save process.
        /// </summary>
        /// <returns>Response indicating the outcome of the Validation operation.</returns>
        Response Validation();

        #endregion Public Methods
    }
}