using PlacementCellManagementAPI.Dtos;

namespace PlacementCellManagementAPI.Interface
{
    /// <summary>
    /// Interface for Admin service operations.
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        /// Creates a new admin based on the provided DTO.
        /// </summary>
        /// <param name="ObjAdminDto">DTO containing admin information.</param>
        /// <returns>True if admin creation is successful, false otherwise.</returns>
        bool CreateAdmin(DtoADM01 ObjAdminDto);

        /// <summary>
        /// Deletes an admin with the specified ID.
        /// </summary>
        /// <param name="id">ID of the admin to be deleted.</param>
        /// <returns>True if admin deletion is successful, false otherwise.</returns>
        bool DeleteAdmin(int id);
    }
}
