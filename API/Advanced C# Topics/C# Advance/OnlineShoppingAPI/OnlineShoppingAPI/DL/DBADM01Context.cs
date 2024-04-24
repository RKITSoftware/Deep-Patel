using MySql.Data.MySqlClient;
using OnlineShoppingAPI.Models.POCO;
using System.Configuration;
using System.Data;

namespace OnlineShoppingAPI.DL
{
    /// <summary>
    /// DB Context for <see cref="ADM01"/>.
    /// </summary>
    public class DBADM01Context
    {
        #region Private Fields

        /// <summary>
        /// <see cref="MySqlConnection"/> for execute MySql Queries.
        /// </summary>
        private MySqlConnection _connection;

        /// <summary>
        /// Connection string for the database connection.
        /// </summary>
        private readonly string _connectionString;

        #endregion Private Fields

        #region Constructor

        /// <summary>
        /// Initialize the <see cref="DBADM01Context"/> class instance.
        /// </summary>
        public DBADM01Context()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Retrieves profit data from the database for a specified date.
        /// </summary>
        /// <param name="date">The date for which profit data is retrieved.</param>
        /// <returns>Datatable containing the profit data.</returns>
        public DataTable GetProfit(string date)
        {
            DataTable dtProfit = new DataTable();

            // SQL query to retrieve profit data for the specified date
            string query = string.Format(@"SELECT
                                                IFNULL(T01F03, 0) AS 'Profit'
                                            FROM
                                                pft01
                                            WHERE
                                                t01f02 = '{0}';", date);

            using (_connection = new MySqlConnection(_connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, _connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                adapter.Fill(dtProfit);
            }

            return dtProfit;
        }

        #endregion Public Methods
    }
}