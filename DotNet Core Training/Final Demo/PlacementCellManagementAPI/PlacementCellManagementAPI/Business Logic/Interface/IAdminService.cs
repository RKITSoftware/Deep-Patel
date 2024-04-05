using PlacementCellManagementAPI.Models;
using PlacementCellManagementAPI.Models.Dtos;
using PlacementCellManagementAPI.Models.POCO;

namespace PlacementCellManagementAPI.Business_Logic.Interface
{
    /// <summary>
    /// Interface for Admin service operations.
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        /// Creates a new admin based on the provided DTO.
        /// </summary>
        /// <returns>True if admin creation is successful, false otherwise.</returns>
        bool CreateAdmin();

        /// <summary>
        /// Deletes an admin with the specified ID.
        /// </summary>
        /// <param name="id">ID of the admin to be deleted.</param>
        /// <returns>True if admin deletion is successful, false otherwise.</returns>
        bool DeleteAdmin(int id);

        /// <summary>
        /// Retrieves all instances of ADM01.
        /// </summary>
        /// <returns>An IEnumerable collection of ADM01 instances.</returns>
        IEnumerable<ADM01> GetAll();

        /// <summary>
        /// Performs pre-save operations on the admin DTO object.
        /// </summary>
        /// <param name="objAdminDto">The DTO object representing the admin.</param>
        void PreSave(DtoADM01 objAdminDto);

        /// <summary>
        /// Performs validation checks on the admin data.
        /// </summary>
        /// <param name="response">Response object for returning api response.</param>
        /// <returns><see langword="true"/> if the admin data is valid, otherwise <see langword="false"/>.</returns>
        bool Validation(out BaseResponse response);
    }
}
