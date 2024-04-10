using System.Text.Json.Serialization;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for ADM01 Model.
    /// </summary>
    public class DTOADM01
    {
        /// <summary>
        /// Admin Name
        /// </summary>
        [JsonPropertyName("M01102")]
        public string M01102 { get; set; }

        /// <summary>
        /// Admin Email
        /// </summary>
        [JsonPropertyName("M01F03")]
        public string M01103 { get; set; }

        /// <summary>
        /// Admin Password
        /// </summary>
        [JsonPropertyName("R01F03")]
        public string R01103 { get; set; }
    }
}