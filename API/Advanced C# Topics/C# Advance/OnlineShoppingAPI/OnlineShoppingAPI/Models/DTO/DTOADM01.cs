using Newtonsoft.Json;

namespace OnlineShoppingAPI.Models.DTO
{
    /// <summary>
    /// DTO for ADM01
    /// </summary>
    public class DTOADM01
    {
        /// <summary>
        /// Admin Name
        /// </summary>
        public string M01102 { get; set; }

        /// <summary>
        /// Admin Email
        /// </summary>
        [JsonProperty("M01F03")]
        public string M01103 { get; set; }

        /// <summary>
        /// Admin Password
        /// </summary>
        [JsonProperty("R01F03")]
        public string R01103 { get; set; }
    }
}