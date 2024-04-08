using Newtonsoft.Json;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for CAT01 POCO Model
    /// </summary>
    public class DTOCAT01
    {
        /// <summary>
        /// Category Id
        /// </summary>
        [JsonProperty("T01F01")]
        public int T01101 { get; set; }

        /// <summary>
        /// Category Name
        /// </summary>
        [JsonProperty("T01F02")]
        public string T01102 { get; set; }
    }
}