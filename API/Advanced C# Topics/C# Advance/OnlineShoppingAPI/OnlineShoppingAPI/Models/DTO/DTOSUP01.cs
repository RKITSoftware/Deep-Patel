using Newtonsoft.Json;

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
        [JsonProperty("P01F01")]
        public string P01101 { get; set; }

        /// <summary>
        /// Suplier name
        /// </summary>
        [JsonProperty("P01F02")]
        public string P01102 { get; set; }

        /// <summary>
        /// Suplier Email Id
        /// </summary>
        [JsonProperty("P01F03")]
        public string P01103 { get; set; }

        /// <summary>
        /// Suplier password
        /// </summary>
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
        [JsonProperty("P01F06")]
        public string P01106 { get; set; }
    }
}