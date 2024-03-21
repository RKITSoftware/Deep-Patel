using System.Text;

namespace FilterDemo.Helper
{
    /// <summary>
    /// Helper class for authentication-related operations.
    /// </summary>
    public class AuthenticationHelper
    {
        /// <summary>
        /// Extracts the username and password from the encoded authorization parameter.
        /// </summary>
        /// <param name="parameter">The encoded authorization parameter.</param>
        /// <returns>A tuple containing the username and password.</returns>
        public static Tuple<string, string> ExtractUserNameAndPassword(string parameter)
        {
            // Decode the base64-encoded authorization parameter.
            string decodedAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(parameter));

            // Split the decoded string to separate username and password.
            string[] usernamePassword = decodedAuthToken.Split(':');

            // Return the username and password as a tuple.
            return new Tuple<string, string>(usernamePassword[0], usernamePassword[1]);
        }
    }
}
