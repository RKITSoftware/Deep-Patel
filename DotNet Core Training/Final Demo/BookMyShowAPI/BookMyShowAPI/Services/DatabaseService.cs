using BookMyShowAPI.Interface;
using MySql.Data.MySqlClient;
using System.Data;

namespace BookMyShowAPI.Services
{
    /// <summary>
    /// For handling database related Queries.
    /// </summary>
    public class DatabaseService : IDatabaseService
    {
        /// <summary>
        /// Configuration of the project.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Excepion logger for logging exception to the file.
        /// </summary>
        private readonly IExceptionLogger _exception;

        /// <summary>
        /// Connection string to establish connection with the database.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Intialize private fields and properties of DatabaseService Class
        /// </summary>
        /// <param name="configuration">Project's confihuration</param>
        /// <param name="exception"><see cref="IExceptionLogger"/> instance for logging exception</param>
        public DatabaseService(IConfiguration configuration, IExceptionLogger exception)
        {
            _configuration = configuration;
            _exception = exception;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Execute statements that only performs adding, updating, deleting operations and don't
        /// return any tables, or any other aggregate functions result.
        /// </summary>
        /// <param name="command">Stores the NonQuery command</param>
        public void ExecuteNonQuery(MySqlCommand command)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    command.Connection = connection;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                _exception.Log(ex);
            }
        }

        /// <summary>
        /// Excecutes the select statements and any other join operations and return datatable.
        /// </summary>
        /// <param name="query">Select statement of MySQL</param>
        /// <returns><see cref="DataTable"/> contains the result of query</returns>
        public DataTable? ExecuteQuery(string query)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        connection.Open();

                        DataTable dataTable = new DataTable();
                        MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);

                        dataAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                _exception.Log(ex);
            }

            return null;
        }
    }
}
