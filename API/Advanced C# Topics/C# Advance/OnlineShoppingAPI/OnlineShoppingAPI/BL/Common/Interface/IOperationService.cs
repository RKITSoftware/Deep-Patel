using OnlineShoppingAPI.Models.Enum;

namespace OnlineShoppingAPI.BL.Common.Interface
{
    /// <summary>
    /// Operation service for the defining of the which operation to used.
    /// </summary>
    public interface IOperationService
    {
        #region Public Properties

        /// <summary>
        /// Operation specify like create or update.
        /// </summary>
        EnmOperation Operation { get; set; }

        #endregion
    }
}
