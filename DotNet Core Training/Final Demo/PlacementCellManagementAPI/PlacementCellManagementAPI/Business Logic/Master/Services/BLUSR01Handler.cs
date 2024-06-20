using PlacementCellManagementAPI.Business_Logic.Interface;
using PlacementCellManagementAPI.DL;
using PlacementCellManagementAPI.Models.POCO;
using System.Data;

namespace PlacementCellManagementAPI.Business_Logic.Services
{
    /// <summary>
    /// Business logic class for handling user-related operations.
    /// </summary>
    public class BLUSR01Handler : IUSR01Service
    {
        #region Private Fields

        /// <summary>
        /// Stores the default connection string to establish connection with the database.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Instance of a <see cref="ILoggerService"/> to log the exceptions handles by the developer.
        /// </summary>
        private readonly ILoggerService _exceptionLogger;

        /// <summary>
        /// SQL Query Context for USR01.
        /// </summary>
        private readonly DBUSR01Context _dbUSR01Context;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the BLUser class.
        /// </summary>
        /// <param name="configuration">Configuration object for retrieving connection string.</param>
        /// <param name="exceptionLogger">Exception logger for logging errors.</param>
        public BLUSR01Handler(IConfiguration configuration, ILoggerService exceptionLogger)
        {
            _connectionString = configuration.GetConnectionString("Default");
            _exceptionLogger = exceptionLogger;
            _dbUSR01Context = new DBUSR01Context(_connectionString);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Checks the provided username and password against the database and retrieves user data if valid.
        /// </summary>
        /// <param name="username">Username to be checked.</param>
        /// <param name="password">Password to be checked.</param>
        /// <param name="objUser">User object containing user data if user is found.</param>
        /// <returns>True if user is found and data is retrieved, false otherwise.</returns>
        public bool CheckUser(string username, string password, out USR01 objUser)
        {
            DataTable dtUser = _dbUSR01Context.GetUserData();

            if (dtUser.Rows.Count > 0)
            {
                foreach (DataRow dr in dtUser.Rows)
                {
                    if (dr["R01F02"].ToString() == username && dr["R01F04"].ToString() == password)
                    {
                        objUser = new USR01()
                        {
                            R01F02 = (string)dr["R01F02"],
                            R01F03 = (string)dr["R01F03"],
                            R01F05 = (string)dr["R01F05"]
                        };
                        return true;
                    }
                }
            }

            objUser = new USR01();
            return false;
        }

        #endregion
    }
}
