using MySql.Data.MySqlClient;
using System.Data;

namespace BookMyShowAPI.Interface
{
    public interface IDatabaseService
    {
        /// <summary>
        /// Execute statements that only performs adding, updating, deleting operations and don't
        /// return any tables, or any other aggregate functions result.
        /// </summary>
        /// <param name="command">Stores the NonQuery command</param>
        void ExecuteNonQuery(MySqlCommand command);

        /// <summary>
        /// Excecutes the select statements and any other join operations and return datatable.
        /// </summary>
        /// <param name="query">Select statement of MySQL</param>
        /// <returns><see cref="DataTable"/> contains the result of query</returns>
        DataTable? ExecuteQuery(string query);
    }
}
