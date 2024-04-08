using Newtonsoft.Json;
using ServiceStack.DataAnnotations;

namespace OnlineShoppingAPI.Models.POCO
{
    public class SUP01
    {
        /// <summary>
        /// Suplier Id
        /// </summary>
        [AutoIncrement]
        [JsonProperty("P01101")]
        public int P01F01 { get; set; }

        /// <summary>
        /// Suplier name
        /// </summary>
        [Required]
        [JsonProperty("P01102")]
        public string P01F02 { get; set; }

        /// <summary>
        /// Suplier Email Id
        /// </summary>
        [Required]
        [JsonProperty("P01103")]
        public string P01F03 { get; set; }

        /// <summary>
        /// Suplier password
        /// </summary>
        [Required]
        [JsonProperty("P01104")]
        public string P01F04 { get; set; }

        /// <summary>
        /// Suplier Mobile Number
        /// </summary>
        [JsonProperty("P01105")]
        public string P01F05 { get; set; }

        /// <summary>
        /// Suplier GST Number
        /// </summary>
        [StringLength(maximumLength: 15, minimumLength: 15)]
        [JsonProperty("P01106")]
        public string P01F06 { get; set; }
    }
}