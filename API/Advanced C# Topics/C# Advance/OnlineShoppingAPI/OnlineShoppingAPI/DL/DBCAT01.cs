using MySql.Data.MySqlClient;
using OnlineShoppingAPI.Extension;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.POCO;
using System;
using System.Configuration;
using System.Data;
using System.Net;
using static OnlineShoppingAPI.BL.Common.BLHelper;

namespace OnlineShoppingAPI.DL
{
    /// <summary>
    /// DB Context for <see cref="CAT01"/>.
    /// </summary>
    public class DBCAT01
    {
        #region Private Fields

        /// <summary>
        /// Connection string for the database connection.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// MySqlConnection object for executing MySql Queries.
        /// </summary>
        private readonly MySqlConnection _connection;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize the instance of <see cref="DBCAT01"/>.
        /// </summary>
        public DBCAT01()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            _connection = new MySqlConnection(_connectionString);
        }

        #endregion

        #region Public Methods

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

                response = OkResponse();
                response.Data = dtCategories;
            }
            catch (Exception exception)
            {
                exception.LogException();
                response = ISEResponse();
            }
        }

        #endregion
    }
}