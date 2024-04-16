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
    /// Business logic class for student-related operations.
    /// </summary>
    public class BLSTU01Handler : ISTU01Service
    {
        #region Private Fields

        /// <summary>
        /// Stores the connection string of the database.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// OrmLite Connection
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Student object to store the student's details.
        /// </summary>
        private STU01? _objSTU01;

        /// <summary>
        /// User model to store the student's credentials.
        /// </summary>
        private USR01? _objUSR01;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Operation to be performed.
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion Public Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BLSTU01Handler"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        public BLSTU01Handler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Deletes a student and associated user by ID.
        /// </summary>
        /// <param name="id">The ID of the student to delete.</param>
        /// <returns>A response indicating the outcome of the deletion.</returns>
        public Response Delete(int id)
        {
            using (IDbConnection? db = _dbFactory.OpenDbConnection())
            {
                if (_objSTU01 != null)
                {
                    _objUSR01 = db.SingleById<USR01>(_objSTU01.U01F07);
                }

                using IDbTransaction? transaction = db.BeginTransaction();
                try
                {
                    db.Delete(_objSTU01);
                    db.Delete(_objUSR01);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return OkResponse("Deleted successfully.");
        }

        /// <summary>
        /// Validates if the student exists before deletion.
        /// </summary>
        /// <param name="id">The ID of the student to validate.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response DeleteValidation(int id)
        {
            using (IDbConnection? db = _dbFactory.OpenDbConnection())
            {
                _objSTU01 = db.SingleById<STU01>(id);
            }

            return _objSTU01 != null ? OkResponse() : NotFoundResponse("Student not found.");
        }

        /// <summary>
        /// Prepares student and user objects before saving.
        /// </summary>
        /// <param name="objDto">DTO containing student and user data.</param>
        public void PreSave(DTOSTU01 objDto)
        {
            _objSTU01 = objDto.Convert<STU01>();
            _objUSR01 = objDto.Convert<USR01>();

            _objUSR01.R01F04 = GetEncryptPassword(objDto.R01F04);
            _objUSR01.R01F05 = Roles.Student;
        }

        /// <summary>
        /// Validates student data before saving.
        /// </summary>
        /// <param name="objDto">DTO containing student data.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response PreValidation(DTOSTU01 objDto)
        {
            return OkResponse();
        }

        /// <summary>
        /// Saves the student and associated user data.
        /// </summary>
        /// <returns>A response indicating the outcome of the save operation.</returns>
        public Response Save()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                using IDbTransaction transaction = db.BeginTransaction();
                try
                {
                    int r01F01 = (int)db.Insert(_objUSR01, selectIdentity: true);

                    if (_objSTU01 == null)
                    {
                        transaction.Rollback();
                        return NotFoundResponse();
                    }

                    _objSTU01.U01F07 = r01F01;
                    db.Insert(_objSTU01);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return OkResponse();
        }

        /// <summary>
        /// Validates student data.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
        public Response Validation()
        {
            return OkResponse();
        }

        #endregion Public Methods
    }
}