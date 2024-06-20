using MySql.Data.MySqlClient;
using System.Data;

namespace PlacementCellManagementAPI.DL
{
    /// <summary>
    /// Provides methods to interact with the USR01 database table.
    /// </summary>
    public class DBUSR01Context
    {
        #region Private Fields

        /// <summary>
        /// MySQL database connection.
        /// </summary>
        private readonly MySqlConnection _connection;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the DBUSR01Context class with the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        public DBUSR01Context(string connectionString)
        {
            _connection = new(connectionString);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves user data from the USR01 table in the database.
        /// </summary>
        /// <returns>A DataTable containing the user data.</returns>
        public DataTable GetUserData()
        {
            string query = @"SELECT 
                                R01F02, 
                                R01F03, 
                                R01F04, 
                                R01F05 
                            FROM 
                                USR01;";

            DataTable dataTable = new();
            MySqlCommand command = new(query, _connection);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                dataTable.Load(reader);
            }

            return dataTable;
        }

        #endregion
    }
}
