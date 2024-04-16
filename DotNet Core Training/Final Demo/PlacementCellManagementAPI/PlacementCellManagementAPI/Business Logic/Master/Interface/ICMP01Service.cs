using PlacementCellManagementAPI.Business_Logic.Common.Interface;
using PlacementCellManagementAPI.Models;
using PlacementCellManagementAPI.Models.Dtos;

namespace PlacementCellManagementAPI.Business_Logic.Interface
{
    /// <summary>
    /// Interface defining operations for company services.
    /// </summary>
    public interface ICMP01Service : ICommonDataHandlerService<DTOCMP01>
    {
        /// <summary>
        /// Retrieves a company by its ID.
        /// </summary>
        /// <param name="id">The ID of the company to retrieve.</param>
        /// <returns>Response object</returns>
        Response Get(int id);

        /// <summary>
        /// Retrieves all companies.
        /// </summary>
        /// <returns>Response object</returns>
        Response GetAll();
    }
}
