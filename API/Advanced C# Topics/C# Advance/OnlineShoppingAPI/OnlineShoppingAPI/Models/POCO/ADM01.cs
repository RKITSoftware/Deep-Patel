using ServiceStack.DataAnnotations;

namespace OnlineShoppingAPI.Models.POCO
{
    /// <summary>
    /// ADM01 model to store the admin details.
    /// </summary>
    public class ADM01
    {
        /// <summary>
        /// Admin Id
        /// </summary>
        [PrimaryKey]
        public int M01F01 { get; set; }

        /// <summary>
        /// Admin Name
        /// </summary>
        public string M01F02 { get; set; }

        /// <summary>
        /// Admin Email Address
        /// </summary>
        public string M01F03 { get; set; }
    }
}