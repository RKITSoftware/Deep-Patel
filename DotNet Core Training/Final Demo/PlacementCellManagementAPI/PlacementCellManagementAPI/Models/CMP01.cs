using Newtonsoft.Json;

namespace PlacementCellManagementAPI.Models
{
    /// <summary>
    /// Company POCO model for handling database related queries.
    /// </summary>
    public class CMP01
    {
        /// <summary>
        /// Company Id
        /// </summary>
        public int? P01F01 { get; set; }

        /// <summary>
        /// Company Name
        /// </summary>
        [JsonProperty("P01102")]
        public string P01F02 { get; set; }

        /// <summary>
        /// Company Location
        /// </summary>
        [JsonProperty("P01103")]
        public string P01F03 { get; set; }
    }
}
