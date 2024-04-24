using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace OnlineShoppingAPI.DL
{
    /// <summary>
    /// DB Context for CRT01 model.
    /// </summary>
    public class DBCRT01Context
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
        /// Initialize the <see cref="DBCRT01Context"/>.
        /// </summary>
        public DBCRT01Context()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Gets the full cart information with product name details also.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <returns>Datatable containing the details of customer's cart.</returns>
        public DataTable GetFullDetailsOfCart(int id)
        {
            DataTable dtResult = new DataTable();
            string query = string.Format(@"SELECT
                                                crt01.T01F01 AS 'Id',
                                                pro02.O02F02 AS 'Product Name',
                                                crt01.T01F04 AS 'Quantity',
                                                (crt01.T01F05 * crt01.T01F04) AS 'Price'
                                            FROM
                                                crt01
                                                    INNER JOIN
                                                pro02 ON crt01.T01F03 = pro02.O02F01
                                            WHERE
                                                crt01.T01F02 = {0};", id);

            using (_connection = new MySqlConnection(_connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, _connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                adapter.Fill(dtResult);
            }

            return dtResult;
        }

        #endregion Public Methods
    }
}