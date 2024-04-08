using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace OnlineShoppingAPI.Models.POCO
{
    [Alias("CUS01")]
    public class CUS01
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        [AutoIncrement]
        [JsonProperty("S01101")]
        public int S01F01 { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        [Required]
        [StringLength(maximumLength: 50)]
        [JsonProperty("S01102")]
        public string S01F02 { get; set; }

        /// <summary>
        /// Customer Email Address
        /// </summary>
        [Required]
        [ValidateEmail]
        [JsonProperty("S01103")]
        public string S01F03 { get; set; }

        /// <summary>
        /// Customer Password
        /// </summary>
        [Required]
        [StringLength(minimumLength: 8, maximumLength: 16)]
        [JsonProperty("S01104")]
        public string S01F04 { get; set; }

        /// <summary>
        /// Customer Mobile Number
        /// </summary>
        [Required]
        [StringLength(minimumLength: 10, maximumLength: 10)]
        [JsonProperty("S01105")]
        public string S01F05 { get; set; }

        /// <summary>
        /// Customer Address
        /// </summary>
        [Required]
        [JsonProperty("S01106")]
        public string S01F06 { get; set; }
    }
}