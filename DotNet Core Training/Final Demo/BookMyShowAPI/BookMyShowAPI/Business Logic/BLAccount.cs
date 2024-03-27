using BookMyShowAPI.Dto;
using BookMyShowAPI.Interface;
using BookMyShowAPI.Model;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

namespace BookMyShowAPI.Business_Logic
{
    public class BLAccount : IAccountService
    {
        /// <summary>
        /// Instance of a <see cref="IDatabaseService"/> for handling database related operations.
        /// </summary>
        private readonly IDatabaseService _databaseService;

        /// <summary>
        /// Excepion logger for logging exception to the file.
        /// </summary>
        private readonly IExceptionLogger _exception;

        /// <summary>
        /// Initialize the <see cref="BLAccount"/> fields and properties.
        /// </summary>
        /// <param name="databaseService"><see cref="IDatabaseService"/> instance.</param>
        /// <param name="exception"><see cref="IExceptionLogger"/> instance for logging exceptions.</param>
        public BLAccount(IDatabaseService databaseService, IExceptionLogger exception)
        {
            _databaseService = databaseService;
            _exception = exception;
        }

        /// <summary>
        /// Gets the username and password from the encoded credentials
        /// </summary>
        /// <param name="credentials">Encoded credentials</param>
        /// <returns><see cref="Tuple{T1, T2}"/> where T1 is username and T2 is password</returns>
        public Tuple<string, string> GetUsernameAndPassword(string credentials)
        {
            string decodedAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(credentials));
            string[] usernamePassword = decodedAuthToken.Split(':');

            return new Tuple<string, string>(usernamePassword[0], usernamePassword[1]);
        }

        /// <summary>
        /// Checks the user's credentials are valid or not.
        /// </summary>
        // <param name="credentials">Encoded Credentials</param>
        /// <param name="objUser">User information initialize if user exist else null is initilize.</param>
        /// <returns><see langword="true"/> is user is exist else <see langword="false"/></returns>
        public bool IsCredentialsValid(string credentials, out USR01 objUser)
        {
            Tuple<string, string> usernameAndPassword = GetUsernameAndPassword(credentials);

            string username = usernameAndPassword.Item1;
            string password = usernameAndPassword.Item2;

            DataTable dtUser = _databaseService.ExecuteQuery("SELECT * FROM USR01;");

            foreach (DataRow row in dtUser.Rows)
            {
                if (row["R01F03"].Equals(username) && row["R01F04"].Equals(password))
                {
                    objUser = new USR01()
                    {
                        R01F01 = (int)row["R01F01"],
                        R01F02 = (string)row["R01F02"],
                        R01F03 = (string)row["R01F03"],
                        R01F04 = (string)row["R01F04"]
                    };

                    return true;
                }
            }

            objUser = new USR01();
            return false;
        }

        /// <summary>
        /// Register the new user.
        /// </summary>
        /// <param name="objDtoUser">DTO for the getting user information</param>
        /// <returns><see langword="true"/> if user is successfully registered, else <see langword="false"/></returns>
        public bool RegisterUser(DtoUSR01 objDtoUser)
        {
            MySqlCommand command =
                new MySqlCommand(@"INSERT INTO 
	                                    USR01 (R01F02, R01F03, R01F04)
                                    VALUES 
	                                    (@Name, @Username, @Password);");

            MySqlParameter[] parameters =
            {
                new MySqlParameter("@Name", objDtoUser.R01101),
                new MySqlParameter("@Username", objDtoUser.R01102),
                new MySqlParameter("@Password", objDtoUser.R01103)
            };

            command.Parameters.AddRange(parameters);

            try
            {
                _databaseService.ExecuteNonQuery(command);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
