using MySql.Data.MySqlClient;
using OnlineShoppingAPI.BL.Common;
using OnlineShoppingAPI.Extension;
using OnlineShoppingAPI.Models;
using System;
using System.Configuration;
using System.Data;

namespace OnlineShoppingAPI.DL
{
    public class DBPRO02
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
        public DBPRO02()
        {
            // Get connection string from configuration file
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            // Initialize MySqlConnection with the connection string
            _connection = new MySqlConnection(_connectionString);
        }

        public void GetInformation(out Response response)
        {
            try
            {
                string query = string.Format(@"SELECT 
	                                               pro02.O02F01 AS 'Id',
                                                   pro02.O02F02 AS 'Name',
                                                   pro02.O02F03 AS 'Buy Price',
                                                   pro02.O02F04 AS 'Sell Price',
                                                   pro02.O02F05 AS 'Quantity',
                                                   pro02.O02F06 AS 'Image Link',
                                                   cat01.T01F02 AS 'Category Name',
                                                   sup01.P01F02 AS 'Suplier Name'
                                               FROM
                                                   pro02
                                                       INNER JOIN
                                                   cat01 ON pro02.O02F09 = cat01.T01F01
                                                       INNER JOIN
                                                   sup01 ON pro02.O02F10 = sup01.P01F01;");

                DataTable dtResult = new DataTable();

                using (MySqlCommand command = new MySqlCommand(query, _connection))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                    _connection.Open();
                    adapter.Fill(dtResult);
                    _connection.Close();
                }

                response = BLHelper.OkResponse();
                response.Data = dtResult;
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
            }
        }
    }
}