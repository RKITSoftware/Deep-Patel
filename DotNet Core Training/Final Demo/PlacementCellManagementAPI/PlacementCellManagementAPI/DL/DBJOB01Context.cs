using MySql.Data.MySqlClient;
using System.Data;

namespace PlacementCellManagementAPI.DL
{
    /// <summary>
    /// Database context for the JOB01 model.
    /// </summary>
    public class DBJOB01Context
    {
        #region Private Fields

        /// <summary>
        /// Instance of <see cref="MySqlConnection"/>
        /// </summary>
        private readonly MySqlConnection _connection;

        #endregion Private Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DBJOB01Context"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        public DBJOB01Context(string connectionString)
        {
            _connection = new(connectionString);
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Retrieves all job data from the database.
        /// </summary>
        /// <returns>A DataTable containing all job data.</returns>
        public DataTable GetAll()
        {
            string query = @"SELECT
                                B01F01,
                                B01F02,
                                B01F03,
                                B01F04,
                                B01F05,
                                P01F02,
                                P01F03,
                                B01F07,
                                B01F08
                            FROM
                                JOB01
                                    INNER JOIN
                                cmp01 ON JOB01.B01F06 = cmp01.p01f01;";

            MySqlCommand command = new(query, _connection);

            DataTable dataTable = new();
            MySqlDataAdapter adapter = new(command);

            try
            {
                _connection.Open();
                adapter.Fill(dataTable);
            }
            finally
            {
                _connection.Close();
            }

            return dataTable;
        }

        #endregion Public Methods
    }
}