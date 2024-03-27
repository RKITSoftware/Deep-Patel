using MySql.Data.MySqlClient;
using PlacementCellManagementAPI.Dtos;
using PlacementCellManagementAPI.Interface;
using PlacementCellManagementAPI.Mapper;
using PlacementCellManagementAPI.Models;
using System.Data;

namespace PlacementCellManagementAPI.Business_Logic
{
    /// <summary>
    /// Business logic class for handling admin-related operations.
    /// </summary>
    public class BLAdmin : IAdminService
    {
        private readonly string _connectionString;
        private readonly IExceptionLogger _exceptionLogger;

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
        public bool CreateAdmin(DtoADM01 ObjAdminDto)
        {
            // PreSave
            ADMUSR objAdminUser = AdminMapper.ToPOCO(ObjAdminDto);

            // Validation

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
                    cmd.Parameters.AddWithValue("@p_username", objAdminUser.R01.R01F02);
                    cmd.Parameters.AddWithValue("@p_email", objAdminUser.R01.R01F03);
                    cmd.Parameters.AddWithValue("@p_password", objAdminUser.R01.R01F04);
                    cmd.Parameters.AddWithValue("@p_role", "Admin");
                    cmd.Parameters.AddWithValue("@p_first_name", objAdminUser.M01.M01F02);
                    cmd.Parameters.AddWithValue("@p_last_name", objAdminUser.M01.M01F03);
                    cmd.Parameters.AddWithValue("@p_date_of_birth", objAdminUser.M01.M01F04.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@p_gender", objAdminUser.M01.M01F05);

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
    }
}
