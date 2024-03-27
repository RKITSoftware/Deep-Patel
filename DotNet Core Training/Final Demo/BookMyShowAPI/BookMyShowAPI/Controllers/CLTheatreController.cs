using BookMyShowAPI.Dto;
using BookMyShowAPI.Interface;
using BookMyShowAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShowAPI.Controllers
{
    /// <summary>
    /// Theare controller for handling the theatre related api's
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CLTheatreController : ControllerBase
    {
        /// <summary>
        /// Represents a class with a read-only field for accessing an instance of an theatre service.
        /// </summary>
        private readonly ITheatreService _theatreService;

        /// <summary>
        /// Initialize the <see cref="CLTheatreController"/> private fields and properties.
        /// </summary>
        /// <param name="theatreService">The service responsible for managing theatre-related operations.</param>
        public CLTheatreController(ITheatreService theatreService)
        {
            _theatreService = theatreService;
        }

        /// <summary>
        /// GEt all theatre list.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> where T is <see cref="TRE01"/></returns>
        [HttpGet("")]
        public ActionResult GetTheatres()
        {
            return Ok(_theatreService.GetTheatres());
        }

        /// <summary>
        /// Get all the theatre list according to the city.
        /// </summary>
        /// <param name="city">City name that theatre user wants it.</param>
        /// <returns>theatre list of city</returns>
        [HttpGet("{city}")]
        public ActionResult GetTheatresByCity(string city)
        {
            List<TRE01> lstTheatre = _theatreService.GetTheatresByCity(city);

            if (lstTheatre == null && lstTheatre.Count == 0)
                return NotFound();

            return Ok(lstTheatre);
        }

        /// <summary>
        /// Adding theatre information to the database.
        /// </summary>
        /// <param name="objDtoTRE01">DTO for theatre</param>
        /// <returns>Ok response if theatre is successfully added. Otherwise, BadRequest Response</returns>
        [HttpPost("Add")]
        public ActionResult AddTheatre(DtoTRE01 objDtoTRE01)
        {
            bool isAdded = _theatreService.Add(objDtoTRE01);

            if (isAdded)
            {
                return Ok("Theatre created successfully.");
            }

            return BadRequest();
        }
    }
}
