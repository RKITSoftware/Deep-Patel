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
    /// Data access class for managing category-related database operations.
    /// </summary>
    public class DBCAT01
    {
        /// <summary>
        /// Connection string for the database connection.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// MySqlConnection object for executing MySql Queries.
        /// </summary>
        private readonly MySqlConnection _connection;

        /// <summary>
        /// Initializes a new instance of the DBCAT01 class with default connection settings.
        /// </summary>
        public DBCAT01()
        {
            // Get connection string from configuration file
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

            // Initialize MySqlConnection with the connection string
            _connection = new MySqlConnection(_connectionString);
        }

        /// <summary>
        /// Retrieves all categories from the database.
        /// </summary>
        /// <param name="response">Out parameter containing the response with the retrieved categories.</param>
        public void GetAll(out Response response)
        {
            try
            {
                DataTable dtCategories = new DataTable();

                string query = string.Format(@"SELECT 
                                                   T01F02
                                               FROM
                                                   CAT01;");

                MySqlCommand command = new MySqlCommand(query, _connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                _connection.Open();
                adapter.Fill(dtCategories);
                _connection.Close();

                if (dtCategories.Rows.Count == 0)
                {
                    response = new Response()
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Message = "There are no categories."
                    };
                    return;
                }

                response = new Response()
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Success",
                    Data = dtCategories
                };
            }
            catch (Exception exception)
            {
                exception.LogException();
                response = BLHelper.ISEResponse();
            }
        }
    }
}