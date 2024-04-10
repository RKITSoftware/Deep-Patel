using MySql.Data.MySqlClient;
using OnlineShoppingAPI.BL.Common;
using OnlineShoppingAPI.Extension;
using OnlineShoppingAPI.Models;
using OnlineShoppingAPI.Models.POCO;
using System;
using System.Configuration;
using System.Data;

namespace OnlineShoppingAPI.DL
{
    /// <summary>
    /// DB Context for the <see cref="PRO02"/>.
    /// </summary>
    public class DBPRO02
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
        /// Initializes a new instance of the DBPRO02 class with default connection settings.
        /// </summary>
        public DBPRO02()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            _connection = new MySqlConnection(_connectionString);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the full product information with category and supplier information.
        /// </summary>
        /// <param name="response"><see cref="Response"/> containing the outcome of the operation.</param>
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

                response = BLHelper.OkResponse("Success.");
                response.Data = dtResult;
            }
            catch (Exception ex)
            {
                ex.LogException();
                response = BLHelper.ISEResponse();
            }
        }

        #endregion
    }
}