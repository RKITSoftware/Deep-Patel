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
        private readonly MySqlConnection _connection;

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
            _connection = new MySqlConnection(_connectionString);
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
                                                T01F02
                                            FROM
                                                CAT01;");

            MySqlCommand command = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            try
            {
                _connection.Open();
                adapter.Fill(dtCategories);
            }
            finally { _connection.Dispose(); }

            return dtCategories;
        }

        #endregion Public Methods
    }
}