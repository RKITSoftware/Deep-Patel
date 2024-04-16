using ServiceStack.DataAnnotations;

namespace OnlineShoppingAPI.Models.POCO
{
    /// <summary>
    /// CUS01 model to store the customer's information.
    /// </summary>
    public class CUS01
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        [PrimaryKey]
        public int S01F01 { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        public string S01F02 { get; set; }

        /// <summary>
        /// Customer Email Address
        /// </summary>
        public string S01F03 { get; set; }

        /// <summary>
        /// Customer Password
        /// </summary>
        public string S01F04 { get; set; }

        /// <summary>
        /// Customer Mobile Number
        /// </summary>
        public string S01F05 { get; set; }

        /// <summary>
        /// Customer Address
        /// </summary>
        public string S01F06 { get; set; }
    }
}