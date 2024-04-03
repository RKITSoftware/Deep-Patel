using Microsoft.AspNetCore.Mvc;
using PlacementCellManagementAPI.Dtos;
using PlacementCellManagementAPI.Interface;

namespace PlacementCellManagementAPI.Controllers
{
    /// <summary>
    /// Controller for managing CLAccount related operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CLAccountController : ControllerBase
    {
        /// <summary>
        /// <see cref="ITokenService"/> interface's instance.
        /// </summary>
        private readonly ITokenService _tokenService;

        /// <summary>
        /// Constructor to initialize <see cref="CLAccountController"/>
        /// </summary>
        public CLAccountController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// Generates a token for the provided user
        /// </summary>
        /// <param name="objUserDto">The user data object</param>
        [HttpPost("Generate")]
        public IActionResult GenerateToken(DtoUSR01 objUserDto)
        {
            // Perform pre-save operations, if any
            _tokenService.PreSave(objUserDto);

            // Check if token exists for the user
            if (!_tokenService.IsExist())
            {
                // Return 404 Not Found if token does not exist
                return NotFound();
            }

            // Generate and return the token
            return Ok(_tokenService.GenerateToken());
        }
    }
}
