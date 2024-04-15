using MySql.Data.MySqlClient;
using OnlineShoppingAPI.Models.POCO;
using System.Configuration;
using System.Data;

namespace OnlineShoppingAPI.DL
{
    /// <summary>
    /// DB context for <see cref="RCD01"/>.
    /// </summary>
    public class DBRCD01Context
    {
        #region Private Fields

        /// <summary>
        /// Connection string for the database connection.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// <see cref="MySqlConnection"/> for execute MySql Queries.
        /// </summary>
        private readonly MySqlConnection _connection;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the DBRCD01 class with default connection settings.
        /// </summary>
        public DBRCD01Context()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            _connection = new MySqlConnection(_connectionString);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the customer records data.
        /// </summary>
        /// <param name="id">Customer id</param>
        /// <returns><see cref="DataTable"/> containing the information about customer records.</returns>
        public DataTable GetCUS01Data(int id)
        {
            DataTable dtResult = new DataTable();

            string query = string.Format(@"SELECT 
                                                    d01.D01F01 AS 'OrderId',
                                                    s01.S01F02 AS 'CustomerName',
                                                    o02.O02F02 AS 'ProductName',
                                                    d01.D01F05 AS 'ProductPrice',
                                                    d01.D01F04 AS 'Quantity',
                                                    d01.D01F06 AS 'InvoiceId',
                                                    d01.D01F07 AS 'Purchase Time'
                                                FROM
                                                    rcd01 AS d01
                                                        INNER JOIN
                                                    pro02 AS o02 ON d01.D01F03 = o02.O02F01
                                                        INNER JOIN
                                                    cus01 AS s01 ON d01.D01F02 = s01.S01F01
                                                WHERE
                                                    s01.S01F01 = {0};", id);

            MySqlCommand command = new MySqlCommand(query, _connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            try
            {
                _connection.Open();
                adapter.Fill(dtResult);
            }
            finally { _connection.Close(); }

            return dtResult;
        }

        #endregion
    }
}