using PlacementCellManagementAPI.Models;

namespace PlacementCellManagementAPI.Business_Logic.Common.Interface
{
    public interface ICommonDataHandlerService<T>
        where T : class
    {
        #region Public Properties

        /// <summary>
        /// Get or set the operation to perform.
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Deletes the record and associated data also.
        /// </summary>
        /// <param name="id">Delete Record id</param>
        /// <returns>Response according to the operation.</returns>
        Response Delete(int id);

        /// <summary>
        /// Validation for delete operation.
        /// </summary>
        /// <param name="id">Delete record id</param>
        /// <returns>Response according to the operation.</returns>
        Response DeleteValidation(int id);

        /// <summary>
        /// Converts the DTO to POCO conversion and initialize the fields which are neccessary.
        /// </summary>
        /// <param name="objDto">DTO cntaining the model information.</param>
        void PreSave(T objDto);

        /// <summary>
        /// Performs the prevalidation before the presave like record cheking exists or not.
        /// </summary>
        /// <param name="objDto">DTO cntaining the model information.</param>
        /// <returns>Response object</returns>
        Response PreValidation(T objDto);

        /// <summary>
        /// Performs the add or update operation specified by Operation.
        /// </summary>
        /// <returns>Response according to the operation.</returns>
        Response Save();

        /// <summary>
        /// Validate the poco model's properties.
        /// </summary>
        /// <returns>Response according to the operation.</returns>
        Response Validation();

        #endregion Public Methods
    }
}