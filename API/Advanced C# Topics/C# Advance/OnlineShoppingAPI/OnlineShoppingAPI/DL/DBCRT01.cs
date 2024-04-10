using MySql.Data.MySqlClient;
using OnlineShoppingAPI.Extension;
using OnlineShoppingAPI.Models;
using System;
using System.Configuration;
using System.Data;
using static OnlineShoppingAPI.BL.Common.BLHelper;

namespace OnlineShoppingAPI.DL
{
    public class DBCRT01
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
        /// Initialize the <see cref="DBCRT01"/>.
        /// </summary>
        public DBCRT01()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            _connection = new MySqlConnection(_connectionString);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the full cart information with product name details also.
        /// </summary>
        /// <param name="id">Customer id.</param>
        /// <param name="response"><see cref="Response"/> containing the outcome of the operation.</param>
        public void GetFullDetailsOfCart(int id, out Response response)
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

                response = OkResponse("Success.");
                response.Data = dtResult;
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = ISEResponse();
            }
            finally
            {
                _connection.Close();
            }
        }

        #endregion
    }
}