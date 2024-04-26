using PlacementCellManagementAPI.Business_Logic.Interface;
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
    /// Business Logic implementation for company operations.
    /// </summary>
    public class BLCMP01Handler : ICMP01Service
    {
        #region Private Fields

        /// <summary>
        /// Private field to hold company object
        /// </summary>
        private CMP01? _objCMP01;

        /// <summary>
        /// Connection string for the database
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Factory for database connections
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        #endregion

        #region Public Properties

        /// <summary>
        /// Specifies the operation to perform.
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for BLCompany.
        /// </summary>
        /// <param name="configuration">Configuration for database connection.</param>
        public BLCMP01Handler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves a company by ID.
        /// </summary>
        /// <param name="id">The ID of the company to retrieve.</param>
        /// <returns>A response containing the company data.</returns>
        public Response Get(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                _objCMP01 = db.SingleById<CMP01>(id);
            }

            return _objCMP01 != null ? OkResponse("Success", _objCMP01) : NotFoundResponse("Company not found.");
        }

        /// <summary>
        /// Retrieves all companies.
        /// </summary>
        /// <returns>A response containing all company data.</returns>
        public Response GetAll()
        {
            List<CMP01> lstCMP01;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                lstCMP01 = db.Select<CMP01>();
            }

            return (lstCMP01 == null || lstCMP01.Count == 0) ? NoContentResponse() : OkResponse("Success", lstCMP01);
        }

        /// <summary>
        /// Deletes a company by ID.
        /// </summary>
        /// <param name="id">The ID of the company to delete.</param>
        /// <returns>A response indicating the outcome of the deletion.</returns>
        public Response Delete(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.Delete(_objCMP01);
            }

            return OkResponse("Success", _objCMP01);
        }

        /// <summary>
        /// Validates if the company exists before deletion.
        /// </summary>
        /// <param name="id">The ID of the company to validate.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response DeleteValidation(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                _objCMP01 = db.SingleById<CMP01>(id);
            }

            return _objCMP01 != null ? OkResponse() : NotFoundResponse("Customer not found.");
        }

        /// <summary>
        /// Prepares company object before saving.
        /// </summary>
        /// <param name="objDto">DTO containing company data.</param>
        public void PreSave(DTOCMP01 objDto)
        {
            _objCMP01 = objDto.Convert<CMP01>();
        }

        /// <summary>
        /// Validates company data before saving.
        /// </summary>
        /// <param name="objDto">DTO containing company data.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response PreValidation(DTOCMP01 objDto)
        {
            return OkResponse();
        }

        /// <summary>
        /// Saves the company data.
        /// </summary>
        /// <returns>A response indicating the outcome of the save operation.</returns>
        public Response Save()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                if (Operation == EnmOperation.A)
                {
                    db.Insert(_objCMP01);
                }
            }

            return OkResponse();
        }

        /// <summary>
        /// Validates company data.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
        public Response Validation()
        {
            return OkResponse();
        }

        #endregion
    }
}
