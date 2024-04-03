using Microsoft.IdentityModel.Tokens;
using PlacementCellManagementAPI.Dtos;
using PlacementCellManagementAPI.Extensions;
using PlacementCellManagementAPI.Interface;
using PlacementCellManagementAPI.Models;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PlacementCellManagementAPI.Business_Logic
{
    /// <summary>
    /// Business logic implementation for handling JWT token generation and user existence checks.
    /// </summary>
    public class BLToken : ITokenService
    {
        /// <summary>
        /// User model to store user data.
        /// </summary>
        private USR01 _objUser;

        /// <summary>
        /// Connection string of the database.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Ormlite connection
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// Logs the exception.
        /// </summary>
        private readonly IExceptionLogger _exceptionLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BLToken"/> class.
        /// </summary>
        /// <param name="configuration">The configuration object providing access to application settings.</param>
        /// <param name="exceptionLogger">The exception logger service.</param>
        public BLToken(IConfiguration configuration, IExceptionLogger exceptionLogger)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _exceptionLogger = exceptionLogger ?? throw new ArgumentNullException(nameof(exceptionLogger));
            _connectionString = configuration.GetConnectionString("Default");
            _dbFactory = new OrmLiteConnectionFactory(_connectionString, MySqlDialect.Provider);
        }

        /// <summary>
        /// Gets the configuration object providing access to application settings.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Generates a JWT token based on user information.
        /// </summary>
        /// <returns>The generated JWT token.</returns>
        public string GenerateToken()
        {
            string issuer = Configuration["Jwt:Issuer"];
            string key = Configuration["Jwt:Key"];

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> lstClaim = new List<Claim>
            {
                new Claim(ClaimTypes.Email, _objUser.R01F03),
                new Claim(ClaimTypes.Role, _objUser.R01F05),
                new Claim(ClaimTypes.Name, _objUser.R01F02),
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(_objUser.R01F01)),
            };

            JwtSecurityToken secretToken = new JwtSecurityToken(issuer,
                issuer,
                lstClaim,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            string token = "Bearer " + new JwtSecurityTokenHandler().WriteToken(secretToken);

            return token;
        }

        /// <summary>
        /// Checks if a user exists in the database.
        /// </summary>
        /// <returns><c>true</c> if the user exists; otherwise, <c>false</c>.</returns>
        public bool IsExist()
        {
            try
            {
                using (var db = _dbFactory.OpenDbConnection())
                {
                    bool isExist = db.Exists<USR01>(u => u.R01F02 == _objUser.R01F02 && u.R01F04 == _objUser.R01F04);
                    return isExist;
                }
            }
            catch (Exception ex)
            {
                _exceptionLogger.Log(ex);
                return false;
            }
        }

        /// <summary>
        /// Sets the user object before saving.
        /// </summary>
        /// <param name="objUserDto">The DTO object representing the user.</param>
        public void PreSave(DtoUSR01 objUserDto)
        {
            _objUser = objUserDto.Convert<USR01>();
        }
    }
}