using MySql.Data.MySqlClient;
using OnlineShoppingAPI.Business_Logic;
using OnlineShoppingAPI.Extension;
using OnlineShoppingAPI.Models;
using System;
using System.Configuration;
using System.Data;
using System.Net;

namespace OnlineShoppingAPI.DL
{
    /// <summary>
    /// Data access layer for retrieving profit data from the database.
    /// </summary>
    public class DBADM01
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
        public DBADM01()
        {
            // Get connection string from configuration file
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            // Initialize MySqlConnection with the connection string
            _connection = new MySqlConnection(_connectionString);
        }

        /// <summary>
        /// Retrieves profit data from the database for a specified date.
        /// </summary>
        /// <param name="date">The date for which profit data is retrieved.</param>
        /// <param name="response">An out parameter containing the response object.</param>
        public void GetProfit(string date, out Response response)
        {
            try
            {
                DataTable dtProfit = new DataTable();

                // SQL query to retrieve profit data for the specified date
                string query = string.Format(@"SELECT 
                                                   IFNULL(T01F03, 0) AS 'Profit'
                                               FROM
                                                   pft01
                                                WHERE
                                                   t01f02 = '{0}';", date);

                // Initialize MySqlCommand with the SQL query and MySqlConnection
                MySqlCommand command = new MySqlCommand(query, _connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                // Open the database connection, fill DataTable with the data, and close the connection
                _connection.Open();
                adapter.Fill(dtProfit);
                _connection.Close();

                // If no profit data is found for the specified date
                if (dtProfit.Rows.Count == 0)
                {
                    response = new Response()
                    {
                        IsError = true,
                        StatusCode = HttpStatusCode.NotFound,
                        Message = $"That {date} profit is 0."
                    };
                    return;
                }

                response = BLHelper.OkResponse();
                response.Data = dtProfit;
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                ex.LogException();
                response = BLHelper.ISEResponse();
            }
        }
    }
}
