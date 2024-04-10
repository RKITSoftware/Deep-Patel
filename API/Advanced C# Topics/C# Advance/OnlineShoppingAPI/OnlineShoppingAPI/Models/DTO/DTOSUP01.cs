using System.Text.Json.Serialization;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for SUP01 model.
    /// </summary>
    public class DTOSUP01
    {
        /// <summary>
        /// Suplier Id
        /// </summary>
        [JsonPropertyName("P01F01")]
        public string P01101 { get; set; }

        /// <summary>
        /// Suplier name
        /// </summary>
        [JsonPropertyName("P01F02")]
        public string P01102 { get; set; }

        /// <summary>
        /// Suplier Email Id
        /// </summary>
        [JsonPropertyName("P01F03")]
        public string P01103 { get; set; }

        /// <summary>
        /// Suplier password
        /// </summary>
        [JsonPropertyName("P01F04")]
        public string P01104 { get; set; }

        /// <summary>
        /// Suplier Mobile Number
        /// </summary>
        [JsonPropertyName("P01F05")]
        public string P01105 { get; set; }

        /// <summary>
        /// Suplier GST Number
        /// </summary>
        [JsonPropertyName("P01F06")]
        public string P01106 { get; set; }
    }
}