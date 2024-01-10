namespace BasicAuthAPI.Authentication
{
    /// <summary>
    /// ValidateUser class provides a simple method for user authentication.
    /// </summary>
    public class ValidateUser
    {
        /// <summary>
        /// LogIn method checks if the provided username and password are valid for authentication.
        /// For demonstration purposes, it currently allows access only for the username "admin" with the password "password".
        /// </summary>
        /// <param name="userName">username passed by API headers</param>
        /// <param name="password">password passed by API headers</param>
        /// <returns>True is user is valid else false</returns>
        public static bool LogIn(string userName, string password)
        {
            // Check if the provided username and password match the predefined values.
            if (userName == "admin" && password == "password")
                return true;

            // If not, return false indicating authentication failure.
            return false;
        }
    }
}
