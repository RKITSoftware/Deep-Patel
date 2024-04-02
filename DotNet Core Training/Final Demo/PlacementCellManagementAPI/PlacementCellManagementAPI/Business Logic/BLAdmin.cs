using MySql.Data.MySqlClient;
using PlacementCellManagementAPI.Dtos;
using PlacementCellManagementAPI.Extensions;
using PlacementCellManagementAPI.Interface;
using PlacementCellManagementAPI.Models;
using System.Data;

namespace PlacementCellManagementAPI.Business_Logic
{
    /// <summary>
    /// Business logic class for handling admin-related operations.
    /// </summary>
    public class BLAdmin : IAdminService
    {
        /// <summary>
        /// Stores the connection string of the database.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Logs the exception to the file.
        /// </summary>
        private readonly IExceptionLogger _exceptionLogger;

        /// <summary>
        /// Admin object to store admin details.
        /// </summary>
        private ADM01 objAdmin;

        /// <summary>
        /// User object to store user's credentials.
        /// </summary>
        private USR01 objUser;

        /// <summary>
        /// Initializes a new instance of the BLAdmin class.
        /// </summary>
        /// <param name="configuration">Configuration object for retrieving connection string.</param>
        /// <param name="exceptionLogger">Logger for logging exceptions.</param>
        public BLAdmin(IConfiguration configuration, IExceptionLogger exceptionLogger)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _exceptionLogger = exceptionLogger;
        }

        /// <summary>
        /// Creates a new admin based on the provided DTO.
        /// </summary>
        /// <param name="ObjAdminDto">DTO containing admin information.</param>
        /// <returns>True if admin creation is successful, false otherwise.</returns>
        public bool CreateAdmin()
        {
            // Save
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    // Create MySqlCommand object and specify the stored procedure name and connection
                    MySqlCommand cmd = new MySqlCommand("InsertAdminWithUser", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters required by the stored procedure
                    cmd.Parameters.AddWithValue("@p_username", objUser.R01F02);
                    cmd.Parameters.AddWithValue("@p_email", objUser.R01F03);
                    cmd.Parameters.AddWithValue("@p_password", objUser.R01F04);
                    cmd.Parameters.AddWithValue("@p_role", "Admin");
                    cmd.Parameters.AddWithValue("@p_first_name", objAdmin.M01F02);
                    cmd.Parameters.AddWithValue("@p_last_name", objAdmin.M01F03);
                    cmd.Parameters.AddWithValue("@p_date_of_birth", objAdmin.M01F04.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@p_gender", objAdmin.M01F05);

                    // Execute the stored procedure
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    _exceptionLogger.Log(ex);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Deletes an admin with the specified ID.
        /// </summary>
        /// <param name="id">ID of the admin to be deleted.</param>
        /// <returns>True if admin deletion is successful, false otherwise.</returns>
        public bool DeleteAdmin(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    // Create MySqlCommand object and specify the stored procedure name and connection
                    MySqlCommand cmd = new MySqlCommand("DeleteAdmin", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters required by the stored procedure
                    cmd.Parameters.AddWithValue("@adminId", id);

                    // Execute the stored procedure
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    _exceptionLogger.Log(ex);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Retrieves all instances of ADM01 from the database.
        /// </summary>
        /// <remarks>
        /// This method fetches all records from the ADM01 table in the database.
        /// </remarks>
        /// <returns>A collection of ADM01 instances.</returns>
        public IEnumerable<ADM01> GetAll()
        {
            List<ADM01> lstAdmin = new List<ADM01>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT * FROM ADM01;";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lstAdmin.Add(new ADM01()
                            {
                                M01F01 = reader.GetInt32(0),
                                M01F02 = reader.GetString(1),
                                M01F03 = reader.GetString(2),
                                M01F04 = reader.GetDateTime(3),
                                M01F05 = reader.GetString(4),
                                M01F06 = reader.GetInt32(5)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptionLogger.Log(ex);
            }

            return lstAdmin;
        }

        public void PreSave(DtoADM01 objAdminDto)
        {
            objAdmin = objAdminDto.Convert<ADM01>();
            objUser = objAdminDto.Convert<USR01>();
        }

        public bool Validation()
        {
            return true;
        }
    }
}
