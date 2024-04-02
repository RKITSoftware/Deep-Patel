using PlacementCellManagementAPI.Dtos;
using PlacementCellManagementAPI.Interface;
using PlacementCellManagementAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace PlacementCellManagementAPI.Business_Logic
{
    /// <summary>
    /// Business Logic implementation for company operations.
    /// </summary>
    public class BLCompany : ICompanyService
    {
        #region Private Fields

        /// <summary>
        /// Private field to hold company object
        /// </summary>
        private CMP01 _objCompany;

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

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for BLCompany.
        /// </summary>
        /// <param name="configuration">Configuration for database connection.</param>
        /// <param name="exceptionLogger">Logger for exceptions.</param>
        public BLCompany(IConfiguration configuration, IExceptionLogger exceptionLogger)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
            _exceptionLogger = exceptionLogger;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Adds a new company to the database.
        /// </summary>
        /// <returns>True if addition succeeds, false otherwise.</returns>
        public bool Add()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.Insert(_objCompany);
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
        /// Deletes a company from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the company to delete.</param>
        /// <returns>True if deletion succeeds, false otherwise.</returns>
        public bool Delete(int id)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    db.DeleteById<CMP01>(id);
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
        /// Retrieves a company from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the company to retrieve.</param>
        /// <returns>The company object if found, null otherwise.</returns>
        public CMP01 Get(int id)
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    _objCompany = db.SingleById<CMP01>(id);
                }
            }
            catch (Exception ex)
            {
                _exceptionLogger.Log(ex);
            }

            return _objCompany;
        }

        /// <summary>
        /// Retrieves all companies from the database.
        /// </summary>
        /// <returns>A list of all companies.</returns>
        public IEnumerable<CMP01> GetAll()
        {
            List<CMP01> lstCompanies = new List<CMP01>();

            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    lstCompanies = db.Select<CMP01>();
                }
            }
            catch (Exception ex)
            {
                _exceptionLogger.Log(ex);
            }

            return lstCompanies;
        }

        /// <summary>
        /// Prepares the company object for saving.
        /// </summary>
        /// <param name="objCompanyDto">The DTO containing company information.</param>
        public void PreSave(DtoCMP01 objCompanyDto)
        {
            _objCompany = new CMP01()
            {
                P01F02 = objCompanyDto.P01101,
                P01F03 = objCompanyDto.P01102
            };
        }

        /// <summary>
        /// Validates the company data.
        /// </summary>
        /// <returns>True if data is valid, false otherwise.</returns>
        public bool Validation()
        {
            return true; // Placeholder for validation logic
        }

        #endregion
    }
}
