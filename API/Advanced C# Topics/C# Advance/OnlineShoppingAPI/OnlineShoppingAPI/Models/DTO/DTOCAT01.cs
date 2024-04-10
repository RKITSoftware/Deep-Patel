using System.Text.Json.Serialization;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for CAT01 Model
    /// </summary>
    public class DTOCAT01
    {
        /// <summary>
        /// Category Id
        /// </summary>
        [JsonPropertyName("T01F01")]
        public int T01101 { get; set; }

        /// <summary>
        /// Category Name
        /// </summary>
        [JsonPropertyName("T01F02")]
        public string T01102 { get; set; }
    }
}