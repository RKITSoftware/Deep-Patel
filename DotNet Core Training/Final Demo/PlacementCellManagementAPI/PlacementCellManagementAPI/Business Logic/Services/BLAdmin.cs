using MySql.Data.MySqlClient;
using PlacementCellManagementAPI.Business_Logic.Interface;
using PlacementCellManagementAPI.Extensions;
using PlacementCellManagementAPI.Models;
using PlacementCellManagementAPI.Models.Dtos;
using PlacementCellManagementAPI.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

namespace PlacementCellManagementAPI.Business_Logic.Services
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
        private ADM01 _objAdmin;

        /// <summary>
        /// User object to store user's credentials.
        /// </summary>
        private USR01 _objUser;

        /// <summary>
        /// OrmLite Connection
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Initializes a new instance of the BLAdmin class.
        /// </summary>
        /// <param name="configuration">Configuration object for retrieving connection string.</param>
        /// <param name="exceptionLogger">Logger for logging exceptions.</param>
        public BLAdmin(IConfiguration configuration, IExceptionLogger exceptionLogger)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _exceptionLogger = exceptionLogger;
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
        }

        /// <summary>
        /// Creates a new admin based on the provided DTO.
        /// </summary>
        /// <returns>True if admin creation is successful, false otherwise.</returns>
        public bool CreateAdmin()
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    // Create MySqlCommand object and specify the stored procedure name and connection
                    MySqlCommand cmd = new MySqlCommand("InsertAdminWithUser", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters required by the stored procedure
                    cmd.Parameters.AddWithValue("@p_username", _objUser.R01F02);
                    cmd.Parameters.AddWithValue("@p_email", _objUser.R01F03);
                    cmd.Parameters.AddWithValue("@p_password", _objUser.R01F04);
                    cmd.Parameters.AddWithValue("@p_role", "Admin");
                    cmd.Parameters.AddWithValue("@p_first_name", _objAdmin.M01F02);
                    cmd.Parameters.AddWithValue("@p_last_name", _objAdmin.M01F03);
                    cmd.Parameters.AddWithValue("@p_date_of_birth", _objAdmin.M01F04.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@p_gender", _objAdmin.M01F05);

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
        /// <returns>A collection of ADM01 instances.</returns>
        public IEnumerable<ADM01> GetAll()
        {
            List<ADM01> lstAdmin = new List<ADM01>();

            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    lstAdmin = db.Select<ADM01>();
                }
            }
            catch (Exception ex)
            {
                _exceptionLogger.Log(ex);
            }

            return lstAdmin;
        }

        /// <summary>
        /// Sets the admin and user objects before saving.
        /// </summary>
        /// <param name="objAdminDto">DTO containing admin and user information.</param>
        public void PreSave(DtoADM01 objAdminDto)
        {
            _objAdmin = objAdminDto.Convert<ADM01>();
            _objUser = objAdminDto.Convert<USR01>();
        }

        /// <summary>
        /// Performs validation checks before saving.
        /// </summary>
        /// <returns>True if validation is successful, false otherwise.</returns>
        public bool Validation(out BaseResponse response)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    if (db.Exists<USR01>(u => u.R01F02 == _objUser.R01F02))
                    {
                        response = new BaseResponse
                        {
                            StatusCode = 412, // precondition failed
                            IsError = true,
                            Message = "Username already Exists."
                        };
                        return false;
                    }

                    if (db.Exists<USR01>(u => u.R01F03 == _objUser.R01F03))
                    {
                        response = new BaseResponse
                        {
                            StatusCode = 412, // precondition failed
                            IsError = true,
                            Message = "Email already Exists."
                        };
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptionLogger.Log(ex);
                response = new BaseResponse
                {
                    StatusCode = 500, // Internal Server Error
                    IsError = true,
                    Message = "Internal Server Error."
                };
                return false;
            }

            response = new BaseResponse
            {
                StatusCode = 200, // Ok
                Message = "Validation Successful."
            };
            return true;
        }
    }
}
