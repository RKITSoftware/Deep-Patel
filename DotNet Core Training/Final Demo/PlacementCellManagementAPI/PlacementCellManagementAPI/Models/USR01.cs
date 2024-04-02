using Newtonsoft.Json;

namespace PlacementCellManagementAPI.Models
{
    /// <summary>
    /// User POCO model for storing the user security related information.
    /// </summary>
    public class USR01
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int? R01F01 { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        [JsonProperty("R01102")]
        public string R01F02 { get; set; }

        /// <summary>
        /// User's Email
        /// </summary>
        [JsonProperty("R01103")]
        public string R01F03 { get; set; }

        /// <summary>
        /// User's Secured Password
        /// </summary>
        [JsonProperty("R01104")]
        public string R01F04 { get; set; }

        /// <summary>
        /// Roles
        /// </summary>
        public string R01F05 { get; set; }
    }
}
