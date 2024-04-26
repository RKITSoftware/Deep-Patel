using PlacementCellManagementAPI.Business_Logic.Interface;
using PlacementCellManagementAPI.DL;
using PlacementCellManagementAPI.Extensions;
using PlacementCellManagementAPI.Models;
using PlacementCellManagementAPI.Models.Dtos;
using PlacementCellManagementAPI.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;
using static PlacementCellManagementAPI.Business_Logic.Common.Helper;

namespace PlacementCellManagementAPI.Business_Logic.Services
{
    /// <summary>
    /// Business Logic implementation for job operations.
    /// </summary>
    public class BLJOB01Handler : IJOB01Service
    {
        #region Private Fields

        /// <summary>
        /// Object to hold job information
        /// </summary>
        private JOB01? _objJOB01;

        /// <summary>
        /// Connection string for the database
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Factory for database connections
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Database context for JOB01 model.
        /// </summary>
        private readonly DBJOB01Context _dbJOB01Context;

        #endregion

        #region Public Properties

        /// <summary>
        /// Specified operation to perform.
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for BLJob.
        /// </summary>
        /// <param name="configuration">Configuration for database connection.</param>
        public BLJOB01Handler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString);

            _dbJOB01Context = new(_connectionString);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves all job data.
        /// </summary>
        /// <returns>A response containing all job data.</returns>
        public Response GetAll()
        {
            DataTable dtData = _dbJOB01Context.GetAll();
            return dtData.Rows.Count == 0 ? NoContentResponse() : OkResponse("", dtData.ToJson());
        }

        /// <summary>
        /// Deletes a job by ID.
        /// </summary>
        /// <param name="id">The ID of the job to delete.</param>
        /// <returns>A response indicating the outcome of the deletion.</returns>
        public Response Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Validates if the job exists before deletion.
        /// </summary>
        /// <param name="id">The ID of the job to validate.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response DeleteValidation(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Prepares job object before saving.
        /// </summary>
        /// <param name="objDto">DTO containing job data.</param>
        public void PreSave(DTOJOB01 objDto)
        {
            _objJOB01 = objDto.Convert<JOB01>();
        }

        /// <summary>
        /// Validates job data before saving.
        /// </summary>
        /// <param name="objDto">DTO containing job data.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response PreValidation(DTOJOB01 objDto)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                if (!db.Exists<CMP01>(c => c.P01F01 == objDto.B01F06))
                {
                    return NotFoundResponse("Company not found.");
                }
            }

            return OkResponse();
        }

        /// <summary>
        /// Saves the job data.
        /// </summary>
        /// <returns>A response indicating the outcome of the save operation.</returns>
        public Response Save()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                if (Operation == EnmOperation.A)
                {
                    db.Insert(_objJOB01);
                }
            }

            return OkResponse();
        }

        /// <summary>
        /// Validates job data.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
        public Response Validation()
        {
            return OkResponse();
        }

        #endregion
    }
}
