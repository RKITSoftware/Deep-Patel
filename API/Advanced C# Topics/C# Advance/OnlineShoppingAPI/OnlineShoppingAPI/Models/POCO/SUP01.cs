using Newtonsoft.Json;
using ServiceStack.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineShoppingAPI.Models.POCO
{
    public class SUP01
    {
        /// <summary>
        /// Suplier Id
        /// </summary>
        [AutoIncrement]
        [JsonPropertyName("P01101")]
        public int P01F01 { get; set; }

        /// <summary>
        /// Suplier name
        /// </summary>
        [Required]
        [JsonPropertyName("P01102")]
        public string P01F02 { get; set; }

        /// <summary>
        /// Suplier Email Id
        /// </summary>
        [Required]
        [JsonPropertyName("P01103")]
        public string P01F03 { get; set; }

        /// <summary>
        /// Suplier password
        /// </summary>
        [Required]
        [JsonPropertyName("P01104")]
        public string P01F04 { get; set; }

        /// <summary>
        /// Suplier Mobile Number
        /// </summary>
        [JsonPropertyName("P01105")]
        public string P01F05 { get; set; }

        /// <summary>
        /// Suplier GST Number
        /// </summary>
        [StringLength(maximumLength: 15, minimumLength: 15)]
        [JsonPropertyName("P01106")]
        public string P01F06 { get; set; }
    }
}