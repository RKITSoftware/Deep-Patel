using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MySql.Data.MySqlClient;
using PlacementCellManagementAPI.Dtos;
using PlacementCellManagementAPI.Interface;
using PlacementCellManagementAPI.Mapper;
using PlacementCellManagementAPI.Models;
using System.Data;

namespace PlacementCellManagementAPI.Business_Logic
{
    /// <summary>
    /// Business logic class for student-related operations.
    /// </summary>
    public class BLStudent : IStudentService
    {
        private readonly string _connectionString;
        private readonly IExceptionLogger _exceptionLogger;

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
            ModelState = actionContext.ActionContext.ModelState;
        }

        /// <summary>
        /// Gets the model state dictionary for validation errors.
        /// </summary>
        public ModelStateDictionary ModelState { get; }

        /// <summary>
        /// Adds a new student.
        /// </summary>
        /// <param name="objStudentDto">The DTO representing the student to add.</param>
        /// <returns>True if the student was added successfully, otherwise false.</returns>
        public bool Add(DtoSTU01 objStudentDto)
        {
            // PreSave
            STUUSR objStudentUser = StudentMapper.ToPoco(objStudentDto);

            // Validation

            // Save
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    MySqlCommand command = new MySqlCommand("Insert_Student_Data", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("first_name", objStudentUser.U01.U01F02);
                    command.Parameters.AddWithValue("last_name", objStudentUser.U01.U01F03);
                    command.Parameters.AddWithValue("dob", objStudentUser.U01.U01F04);
                    command.Parameters.AddWithValue("gender", objStudentUser.U01.U01F05);
                    command.Parameters.AddWithValue("aadhar_card_details", objStudentUser.U01.U01F06);
                    command.Parameters.AddWithValue("username", objStudentUser.R01.R01F02);
                    command.Parameters.AddWithValue("email", objStudentUser.R01.R01F03);
                    command.Parameters.AddWithValue("password", objStudentUser.R01.R01F04);
                    command.Parameters.AddWithValue("role", objStudentUser.R01.R01F05);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    _exceptionLogger.Log(ex);
                    ModelState.AddModelError("Create Request Error", ex.Message);
                    return false;
                }
            }

            return true;
        }
    }
}
