using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace VerificationDemo.DL
{
    /// <summary>
    /// DB Class for handling mysql related queries execution.
    /// </summary>
    public class DBUSR01
    {
        #region Private Fields

        /// <summary>
        /// Database connection using MySqlConnection
        /// </summary>
        private readonly MySqlConnection _connection;

        #endregion Private Fields

        #region Constructor

        /// <summary>
        /// Constructor to initialize the <see cref="DBUSR01"/>.
        /// </summary>
        /// <param name="connectionString"></param>
        public DBUSR01(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Gets the all user details from the database.
        /// </summary>
        /// <returns>Datatable containing user records.</returns>
        public DataTable GetAllData()
        {
            DataTable dtResult = new DataTable();

            try
            {
                string query = string.Format(@"SELECT
                                                    r01f02 AS 'Name', r01f03 AS 'Age'
                                                FROM
                                                    usr01;");

                MySqlCommand command = new MySqlCommand(query, _connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                _connection.Open();
                adapter.Fill(dtResult);
            }
            catch (Exception)
            {
                return dtResult;
            }
            finally
            {
                _connection.Close();
            }

            return dtResult;
        }

        #endregion Public Methods
    }
}