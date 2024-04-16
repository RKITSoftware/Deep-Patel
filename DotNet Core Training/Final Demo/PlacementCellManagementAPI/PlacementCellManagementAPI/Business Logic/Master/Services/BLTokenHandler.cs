using Microsoft.IdentityModel.Tokens;
using PlacementCellManagementAPI.Business_Logic.Interface;
using PlacementCellManagementAPI.Extensions;
using PlacementCellManagementAPI.Models;
using PlacementCellManagementAPI.Models.Dtos;
using PlacementCellManagementAPI.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static PlacementCellManagementAPI.Business_Logic.Common.Helper;

namespace PlacementCellManagementAPI.Business_Logic.Services
{
    /// <summary>
    /// Business logic implementation for handling JWT token generation and user existence checks.
    /// </summary>
    public class BLTokenHandler : ITokenService
    {
        #region Private Fields

        /// <summary>
        /// User model to store user data.
        /// </summary>
        private USR01? _objUser;

        /// <summary>
        /// Database connection string.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Ormlite connection.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        #endregion

        #region Public Properties

        /// <summary>
        /// Project's config file access.
        /// </summary>
        public IConfiguration Configuration { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BLTokenHandler"/> class.
        /// </summary>
        /// <param name="configuration">The configuration object providing access to application settings.</param>
        public BLTokenHandler(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the user object before saving.
        /// </summary>
        /// <param name="objUserDto">The DTO object representing the user.</param>
        public void PreSave(DTOUSR01 objUserDto)
        {
            _objUser = objUserDto.Convert<USR01>();
        }

        /// <summary>
        /// Generates a JWT token for the authenticated user.
        /// </summary>
        /// <returns>A response object containing the generated token.</returns>
        public Response GenerateToken()
        {
            string issuer = Configuration["Jwt:Issuer"];
            string key = Configuration["Jwt:Key"];

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(key));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            if (_objUser != null)
            {
                List<Claim> lstClaim = new()
            {
                new Claim(ClaimTypes.Email, _objUser.R01F03 ?? string.Empty),
                new Claim(ClaimTypes.Role, _objUser.R01F05 ?? string.Empty),
                new Claim(ClaimTypes.Name, _objUser.R01F02 ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(_objUser.R01F01)),
            };

                JwtSecurityToken secretToken = new(issuer,
                    issuer,
                    lstClaim,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);

                string token = "Bearer " + new JwtSecurityTokenHandler().WriteToken(secretToken);

                return OkResponse("Token", token);
            }

            return NotFoundResponse();
        }

        /// <summary>
        /// Checks if the user exists in the database.
        /// </summary>
        /// <returns>A response object indicating whether the user exists or not.</returns>
        public Response IsExist()
        {
            using (var db = _dbFactory.OpenDbConnection())
            {
                if (_objUser == null)
                {
                    return NotFoundResponse();
                }

                string encryptedPassword = GetEncryptPassword(_objUser.R01F04);
                bool isExist = db.Exists<USR01>(u => u.R01F02 == _objUser.R01F02 &&
                    (u.R01F04 == _objUser.R01F04 || u.R01F04 == encryptedPassword));

                if (!isExist)
                {
                    return NotFoundResponse();
                }

                _objUser = db.SingleWhere<USR01>("R01F02", _objUser.R01F02);
            }

            return OkResponse();
        }

        #endregion
    }
}
