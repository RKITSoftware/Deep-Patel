using Microsoft.AspNetCore.Mvc;
using PlacementCellManagementAPI.Business_Logic.Interface;
using PlacementCellManagementAPI.Models;
using PlacementCellManagementAPI.Models.Dtos;

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
        public IActionResult GenerateToken(DTOUSR01 objUserDto)
        {
            _tokenService.PreSave(objUserDto);

            Response response = _tokenService.IsExist();
            if (!response.IsError)
            {
                response = _tokenService.GenerateToken();
            }

            return Ok(response);
        }
    }
}
