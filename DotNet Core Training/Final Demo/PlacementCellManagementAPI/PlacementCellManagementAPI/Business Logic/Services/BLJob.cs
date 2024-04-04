using MySql.Data.MySqlClient;
using PlacementCellManagementAPI.Business_Logic.Interface;
using PlacementCellManagementAPI.Models.Dtos;
using PlacementCellManagementAPI.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

namespace PlacementCellManagementAPI.Business_Logic.Services
{
    /// <summary>
    /// Business Logic implementation for job operations.
    /// </summary>
    public class BLJob : IJobService
    {
        /// <summary>
        /// Object to hold job information
        /// </summary>
        private JOB01? objJob;

        /// <summary>
        /// Connection string for the database
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Factory for database connections
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Logger for exceptions
        /// </summary>
        private readonly IExceptionLogger _exceptionLogger;

        /// <summary>
        /// Constructor for BLJob.
        /// </summary>
        /// <param name="configuration">Configuration for database connection.</param>
        /// <param name="exceptionLogger">Logger for exceptions.</param>
        public BLJob(IConfiguration configuration, IExceptionLogger exceptionLogger)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
            _exceptionLogger = exceptionLogger;
        }

        /// <summary>
        /// Adds a new job to the database.
        /// </summary>
        /// <returns>True if addition succeeds, false otherwise.</returns>
        public bool Add()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.Insert(objJob);
                }
            }
            catch (Exception ex)
            {
                _exceptionLogger.Log(ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Deletes a job by its ID.
        /// </summary>
        /// <param name="id">The ID of the job to delete.</param>
        /// <returns>True if deletion succeeds, false otherwise.</returns>
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves all jobs.
        /// </summary>
        /// <returns>The DataTable containing all jobs.</returns>
        public DataTable GetAll()
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string query = string.Format(@"SELECT 
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
                                                    cmp01 ON JOB01.B01F06 = cmp01.p01f01;");

                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                DataTable dataTable = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                adapter.Fill(dataTable);

                return dataTable;
            }
        }

        /// <summary>
        /// Prepares the job object for saving.
        /// </summary>
        /// <param name="objJobDto">The DTO containing job information.</param>
        public void PreSave(DtoJOB01 objJobDto)
        {
            objJob = new JOB01()
            {
                B01F02 = objJobDto.B01101,
                B01F03 = objJobDto.B01102,
                B01F04 = objJobDto.B01103,
                B01F05 = objJobDto.B01104,
                B01F06 = objJobDto.B01107,
                B01F07 = objJobDto.B01105,
                B01F08 = objJobDto.B01106
            };
        }

        /// <summary>
        /// Validates the job data.
        /// </summary>
        /// <returns>True if data is valid, false otherwise.</returns>
        public bool Validation()
        {
            return true;
        }
    }
}
