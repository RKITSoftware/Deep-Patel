using PlacementCellManagementAPI.Dtos;
using PlacementCellManagementAPI.Models;

namespace PlacementCellManagementAPI.Interface
{
    /// <summary>
    /// Interface defining operations for company services.
    /// </summary>
    public interface ICompanyService
    {
        /// <summary>
        /// Adds a new company.
        /// </summary>
        /// <returns>True if addition succeeds, false otherwise.</returns>
        bool Add();

        /// <summary>
        /// Deletes a company by its ID.
        /// </summary>
        /// <param name="id">The ID of the company to delete.</param>
        /// <returns>True if deletion succeeds, false otherwise.</returns>
        bool Delete(int id);

        /// <summary>
        /// Retrieves a company by its ID.
        /// </summary>
        /// <param name="id">The ID of the company to retrieve.</param>
        /// <returns>The company object if found, null otherwise.</returns>
        CMP01 Get(int id);

        /// <summary>
        /// Retrieves all companies.
        /// </summary>
        /// <returns>A collection of all companies.</returns>
        IEnumerable<CMP01> GetAll();

        /// <summary>
        /// Prepares the company object for saving.
        /// </summary>
        /// <param name="objCompanyDto">The DTO containing company information.</param>
        void PreSave(DtoCMP01 objCompanyDto);

        /// <summary>
        /// Validates the company data.
        /// </summary>
        /// <returns>True if data is valid, false otherwise.</returns>
        bool Validation();
    }
}
