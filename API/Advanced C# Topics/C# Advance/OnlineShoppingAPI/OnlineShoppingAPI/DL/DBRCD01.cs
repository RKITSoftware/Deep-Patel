using MySql.Data.MySqlClient;
using OnlineShoppingAPI.Extension;
using System;
using System.Configuration;
using System.Data;

namespace OnlineShoppingAPI.DL
{
    public class DBRCD01
    {
        /// <summary>
        /// Connection string for the database connection.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// <see cref="MySqlConnection"/> for execute MySql Queries.
        /// </summary>
        private readonly MySqlConnection _connection;

        /// <summary>
        /// Initializes a new instance of the DBADM01 class with default connection settings.
        /// </summary>
        public DBRCD01()
        {
            // Get connection string from configuration file
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            // Initialize MySqlConnection with the connection string
            _connection = new MySqlConnection(_connectionString);
        }

        public DataTable GetCUS01Data(int id)
        {
            DataTable dtResult = new DataTable();

            try
            {
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

                _connection.Open();
                adapter.Fill(dtResult);
                _connection.Close();
            }
            catch (Exception ex)
            {
                ex.LogException();
            }

            return dtResult;
        }
    }
}