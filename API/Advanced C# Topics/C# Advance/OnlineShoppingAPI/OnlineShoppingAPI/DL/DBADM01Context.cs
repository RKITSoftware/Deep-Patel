using MySql.Data.MySqlClient;
using OnlineShoppingAPI.Models.POCO;
using System.Configuration;

namespace OnlineShoppingAPI.DL
{
    /// <summary>
    /// DB Context for <see cref="ADM01"/>.
    /// </summary>
    public class DBADM01Context
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
        /// Initialize the <see cref="DBADM01Context"/> class instance.
        /// </summary>
        public DBADM01Context()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            _connection = new MySqlConnection(_connectionString);
        }

        #endregion Constructor
    }
}