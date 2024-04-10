using OnlineShoppingAPI.Models;

namespace OnlineShoppingAPI.BL.Common.Interface
{
    /// <summary>
    /// Service for pre data handling process.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPreDataHandlerService<T>
        where T : class
    {
        /// <summary>
        /// DTO to POCO Model conversion and other fields initialization.
        /// </summary>
        /// <param name="data">DTO object of specific model.</param>
        void PreSave(T data);

        /// <summary>
        /// Validation before the PreSave process like checking data exists or not.
        /// </summary>
        /// <param name="data">DTO object of specific model.</param>
        /// <param name="response">Response containing the outcome of the operation.</param>
        /// <returns></returns>
        bool PreValidation(T data, out Response response);
    }
}
