using BookMyShowAPI.Dto;
using BookMyShowAPI.Interface;
using BookMyShowAPI.Model;
using MySql.Data.MySqlClient;
using System.Data;

namespace BookMyShowAPI.Business_Logic
{
    /// <summary>
    /// Busines Logic implementations of the <see cref="IMovieService"/>
    /// </summary>
    public class BLMovie : IMovieService
    {
        /// <summary>
        /// Instance of a <see cref="IDatabaseService"/> for handling database related operations.
        /// </summary>
        private readonly IDatabaseService _databaseService;

        /// <summary>
        /// Initialize the <see cref="BLMovie"/> fields and properties.
        /// </summary>
        /// <param name="databaseService"></param>
        public BLMovie(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// Adds a new movie based on the provided DTO object.
        /// </summary>
        /// <param name="objDtoMOV01">The DTO object containing movie information.</param>
        /// <returns>True if the movie is successfully added; otherwise, false.</returns>
        public bool Add(DtoMOV01 objDtoMOV01)
        {
            MySqlCommand command = new MySqlCommand(
                @"INSERT INTO MOV01 (V01F02, V01F03, V01F04) VALUES (@Name, @ReleasedDate, @Duration)");

            MySqlParameter[] parameters = {
                new MySqlParameter("@Name", objDtoMOV01.V01101),
                new MySqlParameter("@ReleasedDate", objDtoMOV01.V01102),
                new MySqlParameter("@Duration", objDtoMOV01.V01103)
            };

            command.Parameters.AddRange(parameters);

            try
            {
                _databaseService.ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Retrieves all movies.
        /// </summary>
        /// <returns>An enumerable collection of MOV01 objects representing all movies.</returns>
        public IEnumerable<MOV01> GetAll()
        {
            DataTable dtMovies = _databaseService.ExecuteQuery("SELECT * FROM MOV01;");

            List<MOV01> lstMovies = new List<MOV01>();

            foreach (DataRow row in dtMovies.Rows)
            {
                DateTime currentTime = DateTime.Now;

                DateTime movieTime = (DateTime)row["V01F03"];
                DateTime oneWeekBeforeTime = movieTime.AddDays(-7);
                DateTime threeWeeksAfter = movieTime.AddDays(21);

                if (oneWeekBeforeTime < currentTime && currentTime < threeWeeksAfter)
                {
                    lstMovies.Add(new MOV01()
                    {
                        V01F01 = (int)row["V01F01"],
                        V01F02 = (string)row["V01F02"],
                        V01F03 = (DateTime)row["V01F03"],
                        V01F04 = (int)row["V01F04"]
                    });
                }
            }

            return lstMovies;
        }
    }
}
