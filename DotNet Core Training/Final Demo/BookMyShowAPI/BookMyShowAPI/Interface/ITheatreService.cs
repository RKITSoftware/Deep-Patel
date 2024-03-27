using BookMyShowAPI.Dto;
using BookMyShowAPI.Model;

namespace BookMyShowAPI.Interface
{
    /// <summary>
    /// Represents a service interface for managing theatre-related operations.
    /// </summary>
    public interface ITheatreService
    {
        /// <summary>
        /// Adds a new theatre based on the provided DTO object.
        /// </summary>
        /// <param name="objDtoTRE01">The DTO object containing theatre information.</param>
        /// <returns>True if the theatre is successfully added; otherwise, false.</returns>
        bool Add(DtoTRE01 objDtoTRE01);

        /// <summary>
        /// Retrieves a list of all theatres.
        /// </summary>
        /// <returns>A list of TRE01 objects representing theatres.</returns>
        List<TRE01> GetTheatres();

        /// <summary>
        /// Retrieves a list of theatres based on the specified city.
        /// </summary>
        /// <param name="city">The city for which theatres are to be retrieved.</param>
        /// <returns>A list of TRE01 objects representing theatres in the specified city.</returns>
        List<TRE01> GetTheatresByCity(string city);
    }
}
