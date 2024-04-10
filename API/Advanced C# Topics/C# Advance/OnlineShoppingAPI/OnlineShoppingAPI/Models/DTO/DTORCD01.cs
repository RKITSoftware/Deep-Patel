using Newtonsoft.Json;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for RCD01 Model.
    /// </summary>
    public class DTORCD01
    {
        /// <summary>
        /// Foreign key of Customer
        /// </summary>
        [JsonProperty("D01F02")]
        public int D01F02 { get; set; }

        /// <summary>
        /// Foreign key of Product
        /// </summary>
        [JsonProperty("D01F03")]
        public int D01F03 { get; set; }

        /// <summary>
        /// Quantity of product that customer want to by it.
        /// </summary>
        [JsonProperty("D01F04")]
        public int D01F04 { get; set; }
    }
}