using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingAPI.Models.DTO
{
    public class DTOSUP01
    {
        /// <summary>
        /// Suplier Id
        /// </summary>
        [Required]
        [JsonProperty("P01F01")]
        public string P01101 { get; set; }

        /// <summary>
        /// Suplier name
        /// </summary>
        [Required]
        [JsonProperty("P01F02")]
        public string P01102 { get; set; }

        /// <summary>
        /// Suplier Email Id
        /// </summary>
        [Required]
        [JsonProperty("P01F03")]
        public string P01103 { get; set; }

        /// <summary>
        /// Suplier password
        /// </summary>
        [Required]
        [JsonProperty("P01F04")]
        public string P01104 { get; set; }

        /// <summary>
        /// Suplier Mobile Number
        /// </summary>
        [JsonProperty("P01F05")]
        public string P01105 { get; set; }

        /// <summary>
        /// Suplier GST Number
        /// </summary>
        [StringLength(maximumLength: 15, MinimumLength = 15)]
        [JsonProperty("P01F06")]
        public string P01106 { get; set; }
    }
}