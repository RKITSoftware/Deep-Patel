using BookMyShowAPI.Dto;
using BookMyShowAPI.Interface;
using BookMyShowAPI.Model;
using MySql.Data.MySqlClient;
using System.Data;

namespace BookMyShowAPI.Business_Logic
{
    /// <summary>
    /// Business Logic implementation of the <see cref="ITheatreService"/>
    /// </summary>
    public class BLTheatre : ITheatreService
    {
        /// <summary>
        /// Instance of a <see cref="IDatabaseService"/> for handling database related operations.
        /// </summary>
        private readonly IDatabaseService _databaseService;

        /// <summary>
        /// Initializing the <see cref="BLTheatre"/> fields and properties/
        /// </summary>
        /// <param name="databaseService"></param>
        public BLTheatre(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// Adds a new theatre based on the provided DTO object.
        /// </summary>
        /// <param name="objDtoTRE01">The DTO object containing theatre information.</param>
        /// <returns>True if the theatre is successfully added; otherwise, false.</returns>
        public bool Add(DtoTRE01 objDtoTRE01)
        {
            MySqlCommand command = new MySqlCommand(
                @"INSERT INTO TRE01 (E01F02, E01F03) VALUES (@Name, @Location)");

            MySqlParameter[] parameters = {
                new MySqlParameter("@Name", objDtoTRE01.E01101),
                new MySqlParameter("@Location", objDtoTRE01.E01102)
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
        /// Retrieves a list of all theatres.
        /// </summary>
        /// <returns>A list of TRE01 objects representing theatres.</returns>
        public List<TRE01> GetTheatres()
        {
            DataTable? dtTheatres = _databaseService.ExecuteQuery("SELECT * FROM tre01;");
            List<TRE01> lstTheatre = new List<TRE01>();

            foreach (DataRow dr in dtTheatres.Rows)
            {
                lstTheatre.Add(new TRE01()
                {
                    E01F01 = (int)dr[0],
                    E01F02 = (string)dr[1],
                    E01F03 = (string)dr[2],
                });
            }

            return lstTheatre;
        }

        /// <summary>
        /// Retrieves a list of theatres based on the specified city.
        /// </summary>
        /// <param name="city">The city for which theatres are to be retrieved.</param>
        /// <returns>A list of TRE01 objects representing theatres in the specified city.</returns>
        public List<TRE01> GetTheatresByCity(string city)
        {
            DataTable? dtTheatres = _databaseService.ExecuteQuery("SELECT * FROM tre01;");
            List<TRE01> lstTheatre = new List<TRE01>();

            foreach (DataRow dr in dtTheatres.Rows)
            {
                if (dr[2].ToString()?.ToLower() == city.ToLower())
                {
                    lstTheatre.Add(new TRE01()
                    {
                        E01F01 = (int)dr[0],
                        E01F02 = (string)dr[1],
                        E01F03 = (string)dr[2],
                    });
                }
            }

            return lstTheatre;
        }
    }
}
