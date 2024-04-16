using PlacementCellManagementAPI.Models;
using PlacementCellManagementAPI.Models.Dtos;

namespace PlacementCellManagementAPI.Business_Logic.Interface
{
    /// <summary>
    /// Interface defining operations related to JWT token generation and user existence checks.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates a JWT token for the authenticated user.
        /// </summary>
        /// <returns>A response object containing the generated token.</returns>
        Response GenerateToken();

        /// <summary>
        /// Checks if the user exists in the database.
        /// </summary>
        /// <returns>A response object indicating whether the user exists or not.</returns>
        Response IsExist();

        /// <summary>
        /// Sets the user object before saving.
        /// </summary>
        /// <param name="objUserDto">The DTO object representing the user.</param>
        void PreSave(DTOUSR01 objUserDto);
    }
}
