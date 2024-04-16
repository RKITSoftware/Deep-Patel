using PlacementCellManagementAPI.Business_Logic.Common.Interface;
using PlacementCellManagementAPI.Models;
using PlacementCellManagementAPI.Models.Dtos;

namespace PlacementCellManagementAPI.Business_Logic.Interface
{
    /// <summary>
    /// Interface for Admin service operations.
    /// </summary>
    public interface IADM01Service : ICommonDataHandlerService<DTOADM01>
    {
        /// <summary>
        /// Retrieves all instances of ADM01.
        /// </summary>
        /// <returns>Response object.</returns>
        Response GetAll();
    }
}
