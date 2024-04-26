using OnlineShoppingAPI.BL.Common;
using OnlineShoppingAPI.BL.Master.Interface;
using OnlineShoppingAPI.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;
using System.Web;

namespace OnlineShoppingAPI.BL.Master.Service
{
    /// <summary>
    /// Service implementation of <see cref="IUSR01Service"/> for <see cref="USR01"/>.
    /// </summary>
    public class BLUSR01Handler : IUSR01Service
    {
        #region Private Fields

        /// <summary>
        /// _dbFactory is used to store the reference of the database connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        #endregion Private Fields

        #region Constructor

        /// <summary>
        /// Constructor to initialize the <see cref="BLUSR01Handler"/>.
        /// </summary>
        public BLUSR01Handler()
        {
            _dbFactory = HttpContext.Current.Application["DbFactory"] as IDbConnectionFactory;
        }

        #endregion Constructor

        #region Public Methods

        /// <summary>
        /// Retrieves user details using user id.
        /// </summary>
        /// <param name="username">User id of the user.</param>
        /// <returns>User details. Null if the user is not found.</returns>
        public USR01 GetUser(int id)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.SingleById<USR01>(id);
            }
        }

        /// <summary>
        /// Retrieves user details using username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <returns>User details. Null if the user is not found.</returns>
        public USR01 GetUser(string username)
        {
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Single<USR01>(u => u.R01F02.Equals(username));
            }
        }

        /// <summary>
        /// Retrieves user details using username and password.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <returns>User details. Null if the user is not found.</returns>
        public USR01 GetUser(string username, string password)
        {
            string encryptedPassword = BLEncryption.GetEncryptPassword(password);

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Single<USR01>(u =>
                    u.R01F02.Equals(username) &&
                    u.R01F05.Equals(encryptedPassword));
            }
        }

        /// <summary>
        /// Checks if a user exists using username and password.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        public bool IsExist(string username, string password)
        {
            string encryptedPassword = BLEncryption.GetEncryptPassword(password);
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                return db.Exists<USR01>(u =>
                    u.R01F02.Equals(username) &&
                    u.R01F05.Equals(encryptedPassword));
            }
        }

        #endregion Public Methods
    }
}