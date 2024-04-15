using VerificationDemo.BL.Common.Interface;
using VerificationDemo.Models;
using VerificationDemo.Models.DTO;

namespace VerificationDemo.BL.Master.Interface
{
    /// <summary>
    /// Services for the USR01.
    /// </summary>
    public interface IUSR01Service : ICommonDataHandlerService<DTOUSR01>
    {
        #region Public Methods

        /// <summary>
        /// Deleted the record of USR01 for the specified id.
        /// </summary>
        /// <param name="id">Delete record id.</param>
        /// <returns>Response containing the outcome of the operation.</returns>
        Response Delete(int id);

        /// <summary>
        /// Gets all user information
        /// </summary>
        /// <returns>Response containing the outcome of the operation.</returns>
        Response GetAll();

        #endregion Public Methods
    }
}