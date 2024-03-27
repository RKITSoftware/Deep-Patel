using BookMyShowAPI.Dto;
using BookMyShowAPI.Model;

namespace BookMyShowAPI.Interface
{
    public interface IAccountService
    {
        /// <summary>
        /// Gets the username and password from the encoded credentials
        /// </summary>
        /// <param name="credentials">Encoded credentials</param>
        /// <returns><see cref="Tuple{T1, T2}"/> where T1 is username and T2 is password</returns>
        bool IsCredentialsValid(string credentials, out USR01 objUser);

        /// <summary>
        /// Checks the user's credentials are valid or not.
        /// </summary>
        // <param name="credentials">Encoded Credentials</param>
        /// <param name="objUser">User information initialize if user exist else null is initilize.</param>
        /// <returns><see langword="true"/> is user is exist else <see langword="false"/></returns>
        Tuple<string, string> GetUsernameAndPassword(string credentials);

        /// <summary>
        /// Register the new user.
        /// </summary>
        /// <param name="objDtoUser">DTO for the getting user information</param>
        /// <returns><see langword="true"/> if user is successfully registered, else <see langword="false"/></returns>
        bool RegisterUser(DtoUSR01 objDtoUser);
    }
}
