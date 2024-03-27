using BookMyShowAPI.Dto;
using BookMyShowAPI.Interface;
using BookMyShowAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShowAPI.Controllers
{
    /// <summary>
    /// Controller for handling the movie related api's.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CLMovieController : ControllerBase
    {
        /// <summary>
        /// Represents a class with a read-only field for accessing an instance of an movie service.
        /// </summary>
        private readonly IMovieService _movieService;

        /// <summary>
        /// Initialize the <see cref="CLMovieController"/> private fields and properties.
        /// </summary>
        /// <param name="movieService">The service responsible for managing movie-related operations.</param>
        public CLMovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Adding new movie data to the table.
        /// </summary>
        /// <param name="objDtoMOV01">DTO for Movie</param>
        /// <returns>Ok response if movie is successfully added. Otherwise, BadREquest response.</returns>
        [HttpPost("Add")]
        public ActionResult Add(DtoMOV01 objDtoMOV01)
        {
            bool isAdded = _movieService.Add(objDtoMOV01);

            if (isAdded)
            {
                return Ok("Movie added successfully.");
            }

            return BadRequest();
        }

        /// <summary>
        /// Get all movies which are in the range of last week to nextx 3 weeks.
        /// </summary>
        /// <returns>A list of movies.</returns>
        [HttpGet("")]
        public ActionResult<IEnumerable<MOV01>> GetAll()
        {
            return Ok(_movieService.GetAll());
        }
    }
}
