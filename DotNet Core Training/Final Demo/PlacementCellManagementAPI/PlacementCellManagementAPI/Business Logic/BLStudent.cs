using Microsoft.AspNetCore.Mvc.Infrastructure;
using MySql.Data.MySqlClient;
using PlacementCellManagementAPI.Dtos;
using PlacementCellManagementAPI.Extensions;
using PlacementCellManagementAPI.Interface;
using PlacementCellManagementAPI.Models;
using System.Data;

namespace PlacementCellManagementAPI.Business_Logic
{
    /// <summary>
    /// Business logic class for student-related operations.
    /// </summary>
    public class BLStudent : IStudentService
    {
        /// <summary>
        /// Stores the connection string of the database.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Exception logger to log the exceptions.
        /// </summary>
        private readonly IExceptionLogger _exceptionLogger;

        /// <summary>
        /// Student object to store the student's details.
        /// </summary>
        private STU01 objStudent;

        /// <summary>
        /// User model to store the student's credentials.
        /// </summary>
        private USR01 objUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="BLStudent"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="exceptionLogger">The exception logger service.</param>
        /// <param name="actionContext">The action context accessor.</param>
        public BLStudent(IConfiguration configuration, IExceptionLogger exceptionLogger,
            IActionContextAccessor actionContext)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _exceptionLogger = exceptionLogger;
        }

        /// <summary>
        /// Adds a new student.
        /// </summary>
        /// <param name="objStudentDto">The DTO representing the student to add.</param>
        /// <returns>True if the student was added successfully, otherwise false.</returns>
        public bool Add()
        {
            // Save
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    MySqlCommand command = new MySqlCommand("Insert_Student_Data", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("first_name", objStudent.U01F02);
                    command.Parameters.AddWithValue("last_name", objStudent.U01F03);
                    command.Parameters.AddWithValue("dob", objStudent.U01F04);
                    command.Parameters.AddWithValue("gender", objStudent.U01F05);
                    command.Parameters.AddWithValue("aadhar_card_details", objStudent.U01F06);
                    command.Parameters.AddWithValue("username", objUser.R01F02);
                    command.Parameters.AddWithValue("email", objUser.R01F03);
                    command.Parameters.AddWithValue("password", objUser.R01F04);
                    command.Parameters.AddWithValue("role", "Student");

                    command.ExecuteNonQuery();
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
        /// Deletes a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student to delete.</param>
        /// <returns>True if the student was deleted successfully, otherwise false.</returns>
        public bool Delete(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    // Create MySqlCommand object and specify the stored procedure name and connection
                    MySqlCommand cmd = new MySqlCommand("DeleteStudent", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters required by the stored procedure
                    cmd.Parameters.AddWithValue("@studentId", id);

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
        /// Sets the internal student and user objects before saving.
        /// </summary>
        /// <param name="objStudentDto">The DTO object representing the student.</param>
        public void PreSave(DtoSTU01 objStudentDto)
        {
            objStudent = objStudentDto.Convert<STU01>();
            objUser = objStudentDto.Convert<USR01>();
        }

        /// <summary>
        /// Performs validation checks on the student data.
        /// </summary>
        /// <returns>True if the student data is valid, otherwise false.</returns>
        public bool Validation()
        {
            return true;
        }
    }
}
