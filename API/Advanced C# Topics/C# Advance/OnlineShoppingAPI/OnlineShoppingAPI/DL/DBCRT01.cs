using MySql.Data.MySqlClient;
using OnlineShoppingAPI.BL.Common;
using OnlineShoppingAPI.Extension;
using OnlineShoppingAPI.Models;
using System;
using System.Configuration;
using System.Data;

namespace OnlineShoppingAPI.DL
{
    public class DBCRT01
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
        public DBCRT01()
        {
            // Get connection string from configuration file
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            // Initialize MySqlConnection with the connection string
            _connection = new MySqlConnection(_connectionString);
        }

        internal void GetFullDetailsOfCart(int id, out Response response)
        {
            try
            {
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

                MySqlCommand command = new MySqlCommand(query, _connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                DataTable dtResult = new DataTable();

                _connection.Open();
                adapter.Fill(dtResult);

                response = BLHelper.OkResponse();
                response.Data = dtResult;
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}