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
    /// Business logic class for handling admin-related operations.
    /// </summary>
    public class BLADM01Handler : IADM01Service
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
        /// Admin object to store admin details.
        /// </summary>
        private ADM01? _objADM01;

        /// <summary>
        /// User object to store user's credentials.
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
        /// Initializes a new instance of the BLAdmin class.
        /// </summary>
        /// <param name="configuration">Configuration object for retrieving connection string.</param>
        public BLADM01Handler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString,
                                                      MySqlDialect.Provider);
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Deletes an admin and associated user by ID.
        /// </summary>
        /// <param name="id">The ID of the admin to delete.</param>
        /// <returns>A response indicating the outcome of the deletion.</returns>
        public Response Delete(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                using IDbTransaction transaction = db.BeginTransaction();
                try
                {
                    db.Delete(_objADM01);
                    db.DeleteById<USR01>(_objADM01?.M01F06);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return OkResponse("Admin deleted successfully.");
        }

        /// <summary>
        /// Validates if the admin exists before deletion.
        /// </summary>
        /// <param name="id">The ID of the admin to validate.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response DeleteValidation(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                _objADM01 = db.SingleById<ADM01>(id);
            }

            return _objADM01 != null ? OkResponse() : NotFoundResponse();
        }

        /// <summary>
        /// Retrieves all admin data.
        /// </summary>
        /// <returns>A response containing all admin data.</returns>
        public Response GetAll()
        {
            List<ADM01> lstADM01;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                lstADM01 = db.Select<ADM01>();
            }

            return lstADM01 == null || lstADM01.Count == 0 ? NoContentResponse() : OkResponse("Admin Data", lstADM01);
        }

        /// <summary>
        /// Prepares admin and user objects before saving.
        /// </summary>
        /// <param name="objDto">DTO containing admin and user data.</param>
        public void PreSave(DTOADM01 objDto)
        {
            _objADM01 = objDto.Convert<ADM01>();
            _objUSR01 = objDto.Convert<USR01>();

            _objUSR01.R01F04 = GetEncryptPassword(_objUSR01.R01F04);
            _objUSR01.R01F05 = Roles.Admin;
        }

        /// <summary>
        /// Validates admin data before saving.
        /// </summary>
        /// <param name="objDto">DTO containing admin data.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response PreValidation(DTOADM01 objDto)
        {
            return OkResponse();
        }

        /// <summary>
        /// Saves the admin and associated user data.
        /// </summary>
        /// <returns>A response indicating the outcome of the save operation.</returns>
        public Response Save()
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                using IDbTransaction transaction = db.BeginTransaction();
                if (Operation == EnmOperation.A)
                {
                    try
                    {
                        int userId = (int)db.Insert(_objUSR01, selectIdentity: true);

                        if (_objADM01 == null)
                        {
                            transaction.Rollback();
                            return NotFoundResponse();
                        }

                        _objADM01.M01F06 = userId;
                        db.Insert(_objADM01);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return OkResponse();
        }

        /// <summary>
        /// Validates admin data.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
        public Response Validation()
        {
            return OkResponse();
        }

        #endregion Public Methods
    }
}