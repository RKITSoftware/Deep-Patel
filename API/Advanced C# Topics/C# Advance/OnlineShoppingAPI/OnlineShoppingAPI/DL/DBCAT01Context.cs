using MySql.Data.MySqlClient;
using OnlineShoppingAPI.Models.POCO;
using System.Configuration;
using System.Data;

namespace OnlineShoppingAPI.DL
{
    /// <summary>
    /// DB Context for <see cref="CAT01"/>.
    /// </summary>
    public class DBCAT01Context
    {
        #region Private Fields

        /// <summary>
        /// MySqlConnection object for executing MySql Queries.
        /// </summary>
        private MySqlConnection _connection;

        /// <summary>
        /// Connection string for the database connection.
        /// </summary>
        private readonly string _connectionString;

        #endregion Private Fields

        #region Constructor

        /// <summary>
        /// Initialize the instance of <see cref="DBCAT01Context"/>.
        /// </summary>
        public DBCAT01Context()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Retrieves all categories from the database.
        /// </summary>
        /// <returns>Datatable containing the all category names.</returns>
        public DataTable GetAll()
        {
            DataTable dtCategories = new DataTable();
            string query = string.Format(@"SELECT
                                                T01F01,
                                                T01F02
                                            FROM
                                                CAT01;");

            using (_connection = new MySqlConnection(_connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, _connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                adapter.Fill(dtCategories);
            }

            return dtCategories;
        }

        #endregion Public Methods
    }
}