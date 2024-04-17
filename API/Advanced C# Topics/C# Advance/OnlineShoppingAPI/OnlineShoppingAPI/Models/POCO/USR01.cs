using ServiceStack.DataAnnotations;

namespace OnlineShoppingAPI.Models.POCO
{
    /// <summary>
    /// USR01 model to store user's credentials
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// User Id
        /// </summary>
        [PrimaryKey]
        public int R01F01 { get; set; }

        /// <summary>
        /// User username
        /// </summary>`
        public string R01F02 { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        public string R01F03 { get; set; }

        /// <summary>
        /// User Role
        /// </summary>
        public string R01F04 { get; set; }

        /// <summary>
        /// Encrypt Password
        /// </summary>
        public string R01F05 { get; set; }
    }
}