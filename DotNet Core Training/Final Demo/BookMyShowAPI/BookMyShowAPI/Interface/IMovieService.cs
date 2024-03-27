using BookMyShowAPI.Dto;
using BookMyShowAPI.Model;

namespace BookMyShowAPI.Interface
{
    /// <summary>
    /// Represents a service interface for managing movie-related operations.
    /// </summary>
    public interface IMovieService
    {
        /// <summary>
        /// Adds a new movie based on the provided DTO object.
        /// </summary>
        /// <param name="objDtoMOV01">The DTO object containing movie information.</param>
        /// <returns>True if the movie is successfully added; otherwise, false.</returns>
        bool Add(DtoMOV01 objDtoMOV01);

        /// <summary>
        /// Retrieves all movies.
        /// </summary>
        /// <returns>An enumerable collection of MOV01 objects representing all movies.</returns>
        IEnumerable<MOV01> GetAll();
    }
}
