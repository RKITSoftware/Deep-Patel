using System.Text.Json.Serialization;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for CRT01 Model
    /// </summary>
    public class DTOCRT01
    {
        /// <summary>
        /// Customer id foreign key for cart
        /// </summary>
        [JsonPropertyName("T01F02")]
        public int T01102 { get; set; }

        /// <summary>
        /// Product id foreign key for cart
        /// </summary>
        [JsonPropertyName("T01F03")]
        public int T01103 { get; set; }

        /// <summary>
        /// Product Quantity that customer need
        /// </summary>
        [JsonPropertyName("T01F04")]
        public int T01104 { get; set; }
    }
}