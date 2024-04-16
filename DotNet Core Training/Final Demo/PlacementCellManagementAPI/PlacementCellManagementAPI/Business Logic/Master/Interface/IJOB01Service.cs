using PlacementCellManagementAPI.Business_Logic.Common.Interface;
using PlacementCellManagementAPI.Models;
using PlacementCellManagementAPI.Models.Dtos;

namespace PlacementCellManagementAPI.Business_Logic.Interface
{
    /// <summary>
    /// Interface defining operations for job services.
    /// </summary>
    public interface IJOB01Service : ICommonDataHandlerService<DTOJOB01>
    {
        /// <summary>
        /// Retrieves all jobs.
        /// </summary>
        /// <returns>The Response containing all jobs.</returns>
        Response GetAll();
    }
}
