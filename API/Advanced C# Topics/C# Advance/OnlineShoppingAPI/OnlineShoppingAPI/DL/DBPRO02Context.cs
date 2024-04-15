using MySql.Data.MySqlClient;
using OnlineShoppingAPI.Models.POCO;
using System.Configuration;
using System.Data;

namespace OnlineShoppingAPI.DL
{
    /// <summary>
    /// DB Context for the <see cref="PRO02"/>.
    /// </summary>
    public class DBPRO02Context
    {
        #region Private Fields

        /// <summary>
        /// <see cref="MySqlConnection"/> for execute MySql Queries.
        /// </summary>
        private readonly MySqlConnection _connection;

        /// <summary>
        /// Connection string for the database connection.
        /// </summary>
        private readonly string _connectionString;

        #endregion Private Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the DBPRO02 class with default connection settings.
        /// </summary>
        public DBPRO02Context()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            _connection = new MySqlConnection(_connectionString);
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Gets the full product information with category and supplier information.
        /// </summary>
        /// <returns>Datatable with the information data</returns>
        public DataTable GetInformation()
        {
            DataTable dtResult = new DataTable();

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

        #endregion Public Methods
    }
}