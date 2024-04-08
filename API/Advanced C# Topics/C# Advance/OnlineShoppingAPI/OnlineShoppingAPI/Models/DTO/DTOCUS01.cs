using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for CUS01 POCO Model
    /// </summary>
    public class DTOCUS01
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        [AutoIncrement]
        [JsonProperty("S01F01")]
        public int S01101 { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        [Required]
        [StringLength(maximumLength: 50)]
        [JsonProperty("S01F02")]
        public string S01102 { get; set; }

        /// <summary>
        /// Customer Email Address
        /// </summary>
        [Required]
        [ValidateEmail]
        [JsonProperty("S01F03")]
        public string S01103 { get; set; }

        /// <summary>
        /// Customer Password
        /// </summary>
        [Required]
        [StringLength(minimumLength: 8, maximumLength: 16)]
        [JsonProperty("S01F04")]
        public string S01104 { get; set; }

        /// <summary>
        /// Customer Mobile Number
        /// </summary>
        [Required]
        [StringLength(minimumLength: 10, maximumLength: 10)]
        [JsonProperty("S01F05")]
        public string S01105 { get; set; }

        /// <summary>
        /// Customer Address
        /// </summary>
        [Required]
        [JsonProperty("S01F06")]
        public string S01106 { get; set; }
    }
}