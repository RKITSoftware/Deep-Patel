using VerificationDemo.Models;
using VerificationDemo.Models.Enum;

namespace VerificationDemo.BL.Common.Interface
{
    /// <summary>
    /// Contains the services which are common and must implement for all the BL Files.
    /// </summary>
    /// <typeparam name="T">DTO specific to the model.</typeparam>
    public interface ICommonDataHandlerService<T>
        where T : class
    {
        #region Public Properties

        /// <summary>
        /// Set or Get the operation to be performed during api request.
        /// </summary>
        EnmOperation Operation { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts the dto to poco model conversion and other sets the date-time related fields.
        /// </summary>
        /// <param name="objDto">DTO of the model.</param>
        void PreSave(T objDto);

        /// <summary>
        /// Performs the cheking operation of Primary key and Foreign Keys of the given DTO.
        /// </summary>
        /// <param name="objDto">DTO of the model.</param>
        /// <returns>Response indicating the outcome of the operation.</returns>
        Response PreValidation(T objDto);

        /// <summary>
        /// Saves the models according to specified operation to the database.
        /// </summary>
        /// <returns>Response indicating the outcome of the operation.</returns>
        Response Save();

        /// <summary>
        /// Performs the validation checks on the data.
        /// </summary>
        /// <returns>Response indicating the outcome of the operation.</returns>
        Response Validation();

        #endregion
    }
}
