using PlacementCellManagementAPI.Dtos;

namespace PlacementCellManagementAPI.Interface
{
    /// <summary>
    /// Interface defining operations related to JWT token generation and user existence checks.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates a JWT token.
        /// </summary>
        /// <returns>The generated JWT token.</returns>
        string GenerateToken();

        /// <summary>
        /// Sets the user object before saving.
        /// </summary>
        /// <param name="objUserDto">The DTO object representing the user.</param>
        void PreSave(DtoUSR01 objUserDto);

        /// <summary>
        /// Checks if a user exists.
        /// </summary>
        /// <returns><c>true</c> if the user exists; otherwise, <c>false</c>.</returns>
        bool IsExist();
    }
}
