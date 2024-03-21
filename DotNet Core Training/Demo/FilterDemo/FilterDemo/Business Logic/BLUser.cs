using FilterDemo.Model;

namespace FilterDemo.Business_Logic
{
    /// <summary>
    /// Business logic class for managing user data.
    /// </summary>
    public class BLUser
    {
        private static List<USR01> _lstUser;

        static BLUser()
        {
            // Initialize the list of users with some sample data.
            _lstUser = new List<USR01>()
            {
                new USR01()
                {
                    R01F01 = 1,
                    R01F02 = "Deep2513",
                    R01F03 = "@1234",
                    R01F04 = "Admin"
                },
                new USR01()
                {
                    R01F01 = 2,
                    R01F02 = "Jeet1234",
                    R01F03 = "@1234",
                    R01F04 = "User"
                }
            };
        }

        /// <summary>
        /// Gets the list of users.
        /// </summary>
        /// <returns>A list of USR01 objects representing users.</returns>
        public static List<USR01> GetUsers()
        {
            return _lstUser;
        }
    }
}
